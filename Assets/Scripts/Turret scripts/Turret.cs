using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Turret : MonoBehaviour
{
    public Vector3 turretLocation;

    //turret settings
    [Header("Core Turret Settings")]
    public int TotalNumberOfTurrets;
    public string parentTurret;
    public int numberActiveStreams;
    public int streamNumber;
    public bool streamEnabled;
    public int turretHealth;
    public bool isDestroyable;

    // generic turret settings (applies to all)
    [Header("Turret Settings")]
    public int fireType;
    public int targetingType;
    public float rotateSpeed;


    // Target player settings
    [Header("Target Player Settings")]
    public bool smoothTarget;
    public float smoothTargetSpeed;
    public float targetPlayerOffsetAmmount;


    // arc targetting settings
    [Header("Arc shot Settings")]
    public float rotateAngleDirection;
    public float rotateAngleWidth;


    // spiral targetting settings
    [Header("spiral shot Settings")]
    public bool spiralDirection;

    //single direction settings
    [Header("single direction Settings")]
    public float singleDirDirection;


    //Bullet fire variables  
    [Header("bullet Settings")]
    public int bulletFormation; 
    public int numOfBullets;
    //firerate determines the gap between each burst of shots
    public float firerate;
    //bullet delay determins the gap between each individual bullet in a burst (if they are staggered)

    [Header("bullet stream shot Settings")]
    public float bulletDelay;
    public bool bulletSpeedIncreaseCheck;
    public float bulletSpeedIncreaseAmmount;

    [Header("bullet shotgun Settings")]
    public float angleBetweenBullets;
    public float bulletRandomRange;

    public bool shotgunStraight;


    //individual bullet settings (what movement type they use, their speed etc)
    [Header("individual bullet Settings")]
    public int bulletMovementType;
    public float bulletBaseSpeed;

    //sin wave settings
    [Header("bullet wave Settings")]
    public float bulletAmplitude;
    public float bulletFrequency;

    //variable speed settings
    [Header("bullet variable speed Settings")]
    public float bulletMaxSpeed;
    public float bulletMinSpeed;
    public float bulletSpeedChangeFrequency;

    //travel then target settings
    [Header("bullet travel then target Settings")]
    public float bulletTimeUntilChange;
    public int bulletNewTargetingType;
    public float bulletSpeedAfterTarget;

    [Header("bullet visual Settings")]
    public int bulletType;
}
