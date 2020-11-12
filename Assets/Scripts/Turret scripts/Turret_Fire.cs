using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Fire : MonoBehaviour
{

    Turret fireRate;
    GameObject _player;

    private float fireTimer;
    public bool readyToFire;
    private void Start()
    {
        fireRate = GetComponent<Turret>();

        fireTimer = 0f;
        readyToFire = true;
    }

    
    void Update()
    {
        if ((fireTimer > fireRate.firerate) && readyToFire)
        {
            GetComponent<Turret_BulletSetup>().fireShot();
            fireTimer = 0f;
            readyToFire = false;
        }
        else
        {
            fireTimer += Time.deltaTime;
        }
    }
}
