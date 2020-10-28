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
            Destroy(gameObject);
        }

        else
        {
            Destroy(destroyTarget);
        }
    }






}