using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class saveTurretSubwave : MonoBehaviour
{
    /// <summary>
    /// each array slot is for a single potential subwave. 
    /// </summary>
    public int totalWaveCount;
    public int[] SubwaveCount = new int[4];
    public Vector3 turretLocation = new Vector3(0, 0, 0);

    public int TotalNumberOfTurrets;
    public bool[] activeInWave = new bool[4];
    public int[] numberActiveStreams = new int[16];
    public float[] subwaveDuration = new float[16];
    public bool[] streamEnabled = new bool[64];

    // generic turret settings (applies to all)
    public int[] turretHealth = new int[4];
    public bool[] turretSavedOnce = new bool[64];
    public int[] fireType = new int[64];
    public int[] targetingType = new int[64];
    public float[] rotateSpeed = new float[64];

    //public Vector4[] temp = new Vector4[16];

    // Target player settings
    public bool[] smoothTarget = new bool[64];
    public float[] smoothTargetSpeed = new float[64];
    public float[] targetPlayerOffsetAmmount = new float[64];


    // arc targetting settings
    public float[] rotateAngleDirection = new float[64];
    public float[] rotateAngleWidth = new float[64];


    // spiral targetting settings
    public bool[] spiralDirection = new bool[64];

    //single direction settings
    public float[] singleDirDirection = new float[64];


    //Bullet fire variables  
    public int[] bulletFormation = new int[64];
    public int[] numOfBullets = new int[64];
    //firerate determines the gap between each burst of shots
    public float[] firerate = new float[64];
    //bullet delay determins the gap between each individual bullet in a burst (if they are staggered)
    public float[] bulletDelay = new float[64];
    public float[] bulletSpeedIncreaseAmmount = new float[64];
    public float[] angleBetweenBullets = new float[64];
    public float[] bulletRandomRange = new float[64];

    public bool[] shotgunStraight = new bool[64];
    public bool[] bulletSpeedIncreaseCheck = new bool[64];

    //individual bullet settings (what movement type they use  their speed etc)
    public int[] bulletMovementType = new int[64];
    public float[] bulletBaseSpeed = new float[64];

    //sin wave storage
    public float[] bulletAmplitude = new float[64];
    public float[] bulletFrequency = new float[64];

    //variable speed storage
    public float[] bulletMaxSpeed = new float[64];
    public float[] bulletMinSpeed = new float[64];
    public float[] bulletSpeedChangeFrequency = new float[64];


    //travel then target storage
    public float[] timeUntilChange = new float[64];
    public int[] newTargetingType = new int[64];
    public float[] speedAfterTarget = new float[64];
}
