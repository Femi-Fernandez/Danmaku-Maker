using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    //check if turret is active
    public int streamNumber;
    public bool streamEnabled;


    // generic turret settings (applies to all)
    public int fireType;
    public float firerate;
    public int targetingType;
    public float rotateSpeed;


    // Target player settings
    public bool smoothTarget;
    public float smoothTargetSpeed;
    public float targetPlayerOffsetAmmount;


    // arc targetting settings
    public float rotateAngleDirection;
    public float rotateAngleWidth;


    // spiral targetting settings
    public bool spiralDirection;

    //single direction settings
    public float singleDirDirection;


    //Bullet fire variables  
    public int bulletFormation; 
    public int numOfBullets;
    public float bulletDelay;

}
