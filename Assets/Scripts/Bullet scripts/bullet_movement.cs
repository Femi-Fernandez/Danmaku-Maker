using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_movement : MonoBehaviour
{
    Bullet bullet;
    public float timer;
    bool increasing;
    public bool newTargetSet;

    GameObject _player;
    private void OnEnable()
    {
        bullet = GetComponent<Bullet>();
        
        Invoke("Destroy", bullet.lifeTime);
        timer = 0;
         newTargetSet = false;
        _player = GameObject.FindWithTag("Player");
    }


    private void FixedUpdate()
    {
        switch (bullet.movementType)
        {
            case 0:
                moveStraightDown();
                break;
            case 1:
                moveSineWave();
                break;
            case 2:
                moveVariableSpeed();
                break;
            case 3:
                moveTravelThenTarget();
                break;
            default:
                break;
        }
    }

    void moveStraightDown()
    {
        Vector3 position = transform.position;
        position += transform.right * bullet.speed * Time.deltaTime;
        transform.position = position;
    }

    void moveSineWave()
    {
        Vector3 position = transform.position;
        position += transform.right * bullet.speed * Time.deltaTime;
        timer += Time.deltaTime;
        transform.position = position + transform.up * Mathf.Cos(timer * bullet.frequency) * bullet.amplitude;
    }

    void moveVariableSpeed()
    {
        float t;

        Vector3 position = transform.position;
        if (increasing)
        {
            timer += Time.deltaTime;
            t = (bullet.speedChangeFrequency - timer) / bullet.speedChangeFrequency;
            bullet.speed = Mathf.SmoothStep(bullet.maxSpeed, bullet.minSpeed, t);
            if (timer > bullet.speedChangeFrequency)
            {
                increasing = false;
            }
        }

        if (!increasing)
        {
            timer -= Time.deltaTime;
            t = (bullet.speedChangeFrequency - timer) / bullet.speedChangeFrequency;
            bullet.speed = Mathf.SmoothStep(bullet.maxSpeed, bullet.minSpeed, t);
            if (timer < 0)
            {
                increasing = true;
            }
        }
        position += transform.right * bullet.speed * Time.deltaTime;

        transform.position = position;
    }

    void moveTravelThenTarget()
    {        
        Vector3 position = transform.position;


        timer += Time.deltaTime;
        if (timer > bullet.timeUntilChange && (newTargetSet == false))
        {
            switch (bullet.newTargetingType)
            {
                case 0:
                    //Vector2 direction = _player.transform.position - transform.position;
                    //Quaternion rotation = Quaternion.LookRotation(direction);
                    //rotation.x = transform.rotation.x;
                    //rotation.y = transform.rotation.y;
                    //transform.rotation = rotation;

                    var dir = _player.transform.position - transform.position;
                    var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                    newTargetSet = true;
                    break;
                default:
                    break;
            }

        }

        position += transform.right * bullet.speed * Time.deltaTime;
        transform.position = position;

    }

    private void Destroy()
    {
       
        gameObject.SetActive(false);    
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
