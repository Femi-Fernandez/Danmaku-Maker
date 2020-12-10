using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Turret : MonoBehaviour
{
    public Vector3 turretLocation;

    //turret settings
    public int TotalNumberOfTurrets;
    public string parentTurret;
    public int numberActiveStreams;
    public int streamNumber;
    public bool streamEnabled;
    public int turretHealth;

    // generic turret settings (applies to all)
    public int fireType;
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
    //firerate determines the gap between each burst of shots
    public float firerate;
    //bullet delay determins the gap between each individual bullet in a burst (if they are staggered)
    public float bulletDelay;
    public float bulletSpeedIncreaseAmmount;
    public float angleBetweenBullets;
    public float bulletRandomRange;

    public bool shotgunStraight;
    public bool bulletSpeedIncreaseCheck;

    //individual bullet settings (what movement type they use, their speed etc)
    public int bulletMovementType;
    public float bulletBaseSpeed;

}
