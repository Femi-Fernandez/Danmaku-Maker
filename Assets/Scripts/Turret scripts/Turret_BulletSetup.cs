using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Turret_BulletSetup : MonoBehaviour
{
    Turret turret;
    GameObject _player;

    bool readytoShoot;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        turret = GetComponent<Turret>();
    }

    public void fireShot()
    {
        if (_player != null)
        {
            //GameObject bullet = transform.GetComponentInParent<bullet_pool_manager>().GetBullet(turret.bulletType);
            switch (turret.bulletFormation)
            {
                //single shot downwards
                case 1:
                    StartCoroutine(Bul_Shot());
                    break;

                //string of shots, then wait to fire again.
                case 2:
                    StartCoroutine(Bul_LineShot());    
                    break;

                //shotgun burst
                case 3:
                    StartCoroutine(Bul_Shotgun());
                    break;

                //random burst;
                case 4:
                    StartCoroutine(bul_randomBurst());
                    break;

                default:
                    break;
            }
        }

    }

    IEnumerator Bul_Shot()
    {
        GameObject bullet = transform.GetComponentInParent<bullet_pool_manager>().GetBullet(turret.bulletType);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.GetComponent<Bullet>().speed = turret.bulletBaseSpeed;

        setBulletValues(bullet);
        bullet.SetActive(true);
        yield return new WaitForSeconds(turret.firerate);
        GetComponent<Turret_Fire>().readyToFire = true;
    }

    IEnumerator Bul_LineShot()
    {   
        for (int i = 0; i < turret.numOfBullets; i++)
        {
            GameObject bullet = transform.GetComponentInParent<bullet_pool_manager>().GetBullet(turret.bulletType);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.GetComponent<Bullet>().speed = turret.bulletBaseSpeed;
            if (turret.bulletSpeedIncreaseCheck == true)
            {
                    bullet.GetComponent<Bullet>().speed = turret.bulletBaseSpeed + (turret.bulletSpeedIncreaseAmmount * i);
            }
            setBulletValues(bullet);
            bullet.SetActive(true);
            yield return new WaitForSeconds(turret.bulletDelay);
        }
        GetComponent<Turret_Fire>().fireTimer = 0;
        GetComponent<Turret_Fire>().readyToFire = true;
    }

    IEnumerator Bul_Shotgun()
    {
        int anglemanip = 0;
        if (turret.numOfBullets % 2 != 0)
        {
            for (int i = 0; i < turret.numOfBullets; i++)
            {
                GameObject bullet = transform.GetComponentInParent<bullet_pool_manager>().GetBullet(turret.bulletType);
                bullet.transform.position = transform.position;
                bullet.GetComponent<Bullet>().speed = turret.bulletBaseSpeed;
                if (i % 2 == 0)
                {
                    bullet.transform.rotation = transform.rotation * Quaternion.AngleAxis(-(anglemanip * turret.angleBetweenBullets), transform.forward);

                    if (turret.shotgunStraight == true)
                    {
                        bullet.GetComponent<Bullet>().speed = turret.bulletBaseSpeed / Mathf.Cos(((turret.angleBetweenBullets * anglemanip) * Mathf.PI) / 180);
                    }
                    anglemanip++;
                }
                else
                {
                    bullet.transform.rotation = transform.rotation * Quaternion.AngleAxis((anglemanip * turret.angleBetweenBullets), transform.forward);
                     

                    if (turret.shotgunStraight == true)
                    {
                        bullet.GetComponent<Bullet>().speed = turret.bulletBaseSpeed / Mathf.Cos(((turret.angleBetweenBullets * anglemanip) * Mathf.PI) / 180);
                    }
                }
                setBulletValues(bullet);
                bullet.SetActive(true);
            } 
        }
        else
        {
            for (int i = 0; i < turret.numOfBullets; i++)
            {
                GameObject bullet = transform.GetComponentInParent<bullet_pool_manager>().GetBullet(turret.bulletType);
                bullet.transform.position = transform.position;
                bullet.GetComponent<Bullet>().speed = turret.bulletBaseSpeed;
                if (i % 2 == 0)
                {
                    bullet.transform.rotation = transform.rotation * Quaternion.AngleAxis(-((anglemanip * turret.angleBetweenBullets) + (turret.angleBetweenBullets / 2)), transform.forward);
                     
                     
                    if (turret.shotgunStraight == true)
                    {
                        bullet.GetComponent<Bullet>().speed = turret.bulletBaseSpeed / Mathf.Cos(((turret.angleBetweenBullets * anglemanip + (turret.angleBetweenBullets / 2)) * Mathf.PI) / 180);
                    }
                }
                else
                {
                    bullet.transform.rotation = transform.rotation * Quaternion.AngleAxis((anglemanip * turret.angleBetweenBullets) + (turret.angleBetweenBullets / 2), transform.forward);
                     
                     
                    if (turret.shotgunStraight == true)
                    {
                        bullet.GetComponent<Bullet>().speed = turret.bulletBaseSpeed / Mathf.Cos(((turret.angleBetweenBullets * anglemanip + (turret.angleBetweenBullets / 2)) * Mathf.PI) / 180);
                    }
                    anglemanip++;
                }
                setBulletValues(bullet);
                bullet.SetActive(true);
            }

        }
        yield return new WaitForSeconds(turret.firerate);
        GetComponent<Turret_Fire>().readyToFire = true;
    }

    IEnumerator bul_randomBurst()
    {
        
        for (int i = 0; i < turret.numOfBullets; i++)
        {
            GameObject bullet = transform.GetComponentInParent<bullet_pool_manager>().GetBullet(turret.bulletType);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation * Quaternion.AngleAxis(UnityEngine.Random.Range(-turret.bulletRandomRange / 2, turret.bulletRandomRange / 2), transform.forward);
            bullet.GetComponent<Bullet>().speed = turret.bulletBaseSpeed;
            setBulletValues(bullet);
            bullet.SetActive(true);
        }
        yield return new WaitForSeconds(turret.firerate);
        GetComponent<Turret_Fire>().readyToFire = true;
    }


    void setBulletValues(GameObject bullet)
    {
        //bullet.GetComponent<Bullet>().speed = turret.bulletBaseSpeed;
        bullet.GetComponent<Bullet>().movementType = turret.bulletMovementType;
        switch (turret.bulletMovementType)
        {
            case 0:
                bullet.GetComponent<Bullet>().movementType = 0;
                break;

            case 1:
                bullet.GetComponent<Bullet>().movementType = 1;
                bullet.GetComponent<Bullet>().amplitude = turret.bulletAmplitude;
                bullet.GetComponent<Bullet>().frequency = turret.bulletFrequency;
                break;
            case 2:
                bullet.GetComponent<Bullet>().movementType = 2;
                bullet.GetComponent<Bullet>().maxSpeed = turret.bulletMaxSpeed;
                bullet.GetComponent<Bullet>().minSpeed = turret.bulletMinSpeed;
                bullet.GetComponent<Bullet>().speedChangeFrequency = turret.bulletSpeedChangeFrequency;
                break;
            case 3:
                bullet.GetComponent<Bullet>().movementType = 3;
                bullet.GetComponent<Bullet>().timeUntilChange = turret.bulletTimeUntilChange;
                bullet.GetComponent<Bullet>().newTargetingType = turret.bulletNewTargetingType;
                bullet.GetComponent<Bullet>().speedAfterTarget = turret.bulletSpeedAfterTarget;
                break;
            default:
                break;
        }
    }
}
