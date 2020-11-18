using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_move_down : MonoBehaviour
{

    public float speed = 3f;

    private void OnEnable()
    {
        Invoke("Destroy", 3f);
    }


    private void FixedUpdate()
    {
            Vector3 position = transform.position;
            position += transform.right * speed * Time.deltaTime;
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
