using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_move_down : MonoBehaviour
{

    public float speed = 3f;
    Vector2 _direction;

    bool isReady = false;

    private void OnEnable()
    {
        Invoke("Destroy", 3f);
    }

    public void setDirection()
    {
        //_direction = direction.normalized;
        isReady = true;
    }

    private void FixedUpdate()
    {
        // transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (isReady)
        {
            Vector3 position = transform.position;

            position += transform.right * speed * Time.deltaTime;
            transform.position = position;
        }
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
