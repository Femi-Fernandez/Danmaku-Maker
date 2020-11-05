using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_BulletSetup : MonoBehaviour
{
    Turret bulletConfig;
    GameObject _player;

    bool readytoShoot;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        bulletConfig = GetComponent<Turret>();
    }

    public void fireShot()
    {
        if (_player != null)
        {
            GameObject bullet = transform.GetComponentInParent<bullet_pool>().GetBullet();
            switch (bulletConfig.bulletFormation)
            {
                //single shot downwards
                case 1:
                    bullet.transform.position = transform.position;
                    bullet.transform.rotation = transform.rotation;
                    bullet.SetActive(true);
                    bullet.GetComponent<bullet_move_down>().setDirection();
                    break;

                //string of shots, then wait to fire again.
                case 2:
                    StartCoroutine(Bul_LineShot());
                    
                    break;

                default:
                    break;
            }
        }

    }

    IEnumerator Bul_LineShot()
    {
        
        for (int i = 0; i < bulletConfig.numOfBullets; i++)
        {
            GameObject bullet = transform.GetComponentInParent<bullet_pool>().GetBullet();
            //Debug.Log("i = " + i);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<bullet_move_down>().setDirection();
            yield return new WaitForSeconds(bulletConfig.bulletDelay);
        }
        GetComponent<Turret_Fire>().readyToFire = true;
       // Debug.Log("here once plss");
    }

}
