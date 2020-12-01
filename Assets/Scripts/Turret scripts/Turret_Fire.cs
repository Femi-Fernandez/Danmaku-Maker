using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Fire : MonoBehaviour
{

    Turret turret;
    GameObject _player;

    public float fireTimer;
    public bool readyToFire;
    private void Start()
    {
        turret = GetComponent<Turret>();
        fireTimer = 0f;
        readyToFire = true;
    }

    
    void FixedUpdate()
    {
        if ((fireTimer > turret.firerate) && readyToFire)
        {
            GetComponent<Turret_BulletSetup>().fireShot();
           // fireTimer = 0f;
            readyToFire = false;
        }
        else
        {
            fireTimer += Time.deltaTime;
        }
    }
}
