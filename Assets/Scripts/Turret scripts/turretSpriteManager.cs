using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretSpriteManager : MonoBehaviour
{

    public int rotateState;
    Transform rotateDir;
    public Transform parentRot;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rotateDir = transform.parent.GetChild(0).transform;
        //parentRot = transform.parent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = parentRot.rotation;
        rotateState = Convert.ToInt32(rotateDir.eulerAngles.z / 45.0f);

        anim.SetInteger("direction", rotateState);
    }
}
