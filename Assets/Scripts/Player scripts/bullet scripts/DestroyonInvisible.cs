using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyonInvisible : MonoBehaviour
{

    public GameObject destroyTarget = null;

    private void OnBecameInvisible()
    {
        if (destroyTarget == null)
        {
            Debug.Log("im dead");
            Destroy(gameObject);
        }

        else
        {
            Debug.Log("i SHOULD BE DEAD");
            Destroy(destroyTarget);
        }
    }






}