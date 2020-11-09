using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
//using System.Numerics;
using UnityEngine;

public class Turret_Targeting : MonoBehaviour
{

    Turret targetType;
    GameObject _player;
    private void Start()
    {
        targetType = GetComponent<Turret>();
        _player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        switch (targetType.targetingType)
        {
            case 1:
                if (_player != null)
                {
                    Vector2 direction = _player.transform.position - transform.position;
                    Quaternion rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);
                    transform.rotation = rotation;
                }
                break;

            case 2:
                // float angle = Mathf.Sin(Time.time) * 70;
                float angle = Mathf.PingPong(Time.time * 50, targetType.rotateAngleWidth) - (targetType.rotateAngleDirection + targetType.rotateAngleWidth/2);
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                break;
            default:

                break;
        }
    }
}
