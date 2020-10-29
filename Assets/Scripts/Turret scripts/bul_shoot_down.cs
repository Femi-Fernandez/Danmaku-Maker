using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bul_shoot_down : MonoBehaviour
{
    public float fireRate = 1f;
    private float fireTimer;
    //this is what you use to spawn the bullet
    void start()
    {
        fireTimer = 0f;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (fireTimer > fireRate)
        {
            shootBullet();
            fireTimer = 0f;
        }
        else
        {
            fireTimer += Time.deltaTime;
        }

    }
    void shootBullet()
    {
        GameObject _player = GameObject.FindWithTag("Player");

        if (_player != null)
        {
            GameObject bullet = transform.GetComponentInParent<bullet_pool>().GetBullet();
            //bullet_pool.bulletPoolInstanse.GetBullet();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
            //Vector2 direction = _player.transform.position - transform.position;
            bullet.GetComponent<bullet_move_down>().setDirection();
        }
    }
}
