using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMinMaxVal : MonoBehaviour
{
    // generic turret settings (applies to all)
    [Header("Turret Settings")]
    public float[] rotateSpeed = new float[2];
    public int[] turretHealth = new int[2];
    // Target player settings
    [Header("Target Player Settings")]
    public float[] smoothTargetSpeed = new float[2];
    public float[] targetPlayerOffsetAmmount = new float[2];

    // arc targetting settings
    [Header("Arc shot Settings")]
    public float[] rotateAngleDirection = new float[2];
    public float[] rotateAngleWidth = new float[2];


    [Header("single direction Settings")]
    public float[] singleDirDirection = new float[2];


    //Bullet fire variables  
    [Header("bullet Settings")]
    public int[] numOfBullets = new int[2];
    public float[] firerate = new float[2];


    [Header("bullet stream shot Settings")]
    public float[] bulletDelay = new float[2];
    public float[] bulletSpeedIncreaseAmmount = new float[2];

    [Header("bullet shotgun Settings")]
    public float[] angleBetweenBullets = new float[2];
    public float[] bulletRandomRange = new float[2];

    //individual bullet settings (what movement type they use, their speed etc)
    [Header("individual bullet Settings")]
    public float[] bulletBaseSpeed = new float[2];

    [Header("bullet wave Settings")]
    public float[] bulletAmplitude = new float[2];
    public float[] bulletFrequency = new float[2];


    //variable speed settings
    [Header("bullet variable speed Settings")]
    public float[] bulletMaxSpeed = new float[2];
    public float[] bulletMinSpeed = new float[2];
    public float[] bulletSpeedChangeFrequency = new float[2];




    //travel then target settings
    [Header("bullet travel then target Settings")]
    public float[] bulletTimeUntilChange = new float[2];
    public float[] bulletSpeedAfterTarget = new float[2];

}
