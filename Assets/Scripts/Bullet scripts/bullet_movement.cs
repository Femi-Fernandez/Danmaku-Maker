using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_movement : MonoBehaviour
{
    Bullet bullet;

    private void OnEnable()
    {
        bullet = GetComponent<Bullet>();

        Invoke("Destroy", bullet.lifeTime);
    }


    private void FixedUpdate()
    {
        switch (bullet.movementType)
        {
            case 1:
                moveStraightDown();
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

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
