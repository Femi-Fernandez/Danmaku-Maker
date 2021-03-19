using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretSpriteManager : MonoBehaviour
{

    public int rotateState;
    Transform rotateDir;
    //public Transform parentRot;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        //rotateDir = transform.parent.GetChild(0).transform;
        //parentRot = transform.parent.transform;
    }

    // Update is called once per frame
    void Update()
    {

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, gameObject.transform.parent.rotation.z * -1.0f);
        rotateState = Convert.ToInt32(gameObject.transform.parent.rotation.eulerAngles.z / 45.0f);

        anim.SetInteger("direction", rotateState);
    }
}
