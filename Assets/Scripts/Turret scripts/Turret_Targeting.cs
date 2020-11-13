﻿using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
//using System.Numerics;
using UnityEngine;

public class Turret_Targeting : MonoBehaviour
{

    Turret turret;
    GameObject _player;

    private void Start()
    {
        turret = GetComponent<Turret>();
        _player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        if (_player != null)
        {
            switch (turret.targetingType)
            {
                case 1:
                    targetPlayer();
                    break;

                case 2:
                    arcShot();
                    break;
                default:

                    break;
            }
        }
    }
    
    void arcShot()
    {
        // float angle = Mathf.Sin(Time.time) * 70;
        float angle = Mathf.PingPong(Time.time * turret.rotateSpeed * 5, turret.rotateAngleWidth) - (turret.rotateAngleDirection + turret.rotateAngleWidth / 2);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void targetPlayer()
    {
        if (turret.smoothTarget == true)
        {
            Vector2 direction = _player.transform.position - transform.position;
            Quaternion rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward) * Quaternion.AngleAxis(turret.targetPlayerOffsetAmmount, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turret.smoothTargetSpeed/1000);
        }
        else
        {
            Vector2 direction = _player.transform.position - transform.position;
            Quaternion rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward) * Quaternion.AngleAxis(turret.targetPlayerOffsetAmmount, Vector3.forward);
            transform.rotation = rotation;

        }

        
        
    }
}
