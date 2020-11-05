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
        switch (fireRate.fireType)
        {
            //shoot down
            case 1:
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

                break;

            default:
                break;
        }
    }

    //move thefire script to a separate script which will handle how many bullets are fired/the angle between them
   // void shootDown()
   // {
   //     if (_player != null)
   //     {
   //         GameObject bullet = transform.GetComponentInParent<bullet_pool>().GetBullet();
   //         //bullet_pool.bulletPoolInstanse.GetBullet();
   //         bullet.transform.position = transform.position;
   //         bullet.transform.rotation = transform.rotation;
   //         bullet.SetActive(true);
   //         //Vector2 direction = _player.transform.position - transform.position;
   //         bullet.GetComponent<bullet_move_down>().setDirection();
   //     }
   // }
}
