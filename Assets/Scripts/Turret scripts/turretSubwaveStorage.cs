using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class turretSubwaveStorage : MonoBehaviour
{
    /// <summary>
    /// each array slot is for a single potential subwave. 
    /// </summary>
    public int totalWaveCount;
    public int[] SubwaveCount = new int[4];
    public Vector3 turretLocation = new Vector3(0,0,0);


    public int TotalNumberOfTurrets;
    public bool[] activeInWave = new bool[4];
    public int[] numberActiveStreams = new int[16];
    public float[] subwaveDuration = new float[16];
    public bool[,] streamEnabled = new bool[16, 4];

    // generic turret settings (applies to all)
    public int[] turretHealth = new int[4];
    public bool[,] turretSavedOnce = new bool[16, 4];
    public int[,] fireType = new int[16, 4];
    public int[,] targetingType = new int[16, 4];
    public float[,] rotateSpeed = new float[16, 4];

    //public Vector4[] temp = new Vector4[16];

    // Target player settings
    public bool[,] smoothTarget = new bool[16, 4];
    public float[,] smoothTargetSpeed =new float[16, 4];
    public float[,] targetPlayerOffsetAmmount = new float[16, 4];


    // arc targetting settings
    public float[,] rotateAngleDirection = new float[16, 4];
    public float[,] rotateAngleWidth = new float[16, 4];


    // spiral targetting settings
    public bool[,] spiralDirection = new bool[16, 4];

    //single direction settings
    public float[,] singleDirDirection = new float[16, 4];


    //Bullet fire variables  
    public int[,] bulletFormation = new int[16, 4];
    public int[,] numOfBullets = new int[16, 4];
    //firerate determines the gap between each burst of shots
    public float[,] firerate = new float[16, 4];
    //bullet delay determins the gap between each individual bullet in a burst (if they are staggered)
    public float[,] bulletDelay = new float[16, 4];
    public float[,] bulletSpeedIncreaseAmmount = new float[16, 4];
    public float[,] angleBetweenBullets = new float[16, 4];
    public float[,] bulletRandomRange = new float[16, 4];

    public bool[,] shotgunStraight = new bool[16, 4];
    public bool[,] bulletSpeedIncreaseCheck = new bool[16, 4];

    //individual bullet settings (what movement type they use, their speed etc)
    public int[,] bulletMovementType = new int[16, 4];
    public float[,] bulletBaseSpeed = new float[16, 4];

    //sin wave storage
    public float[,] bulletAmplitude = new float[16, 4];
    public float[,] bulletFrequency = new float[16, 4];

    //variable speed storage
    public float[,] bulletMaxSpeed = new float[16, 4];
    public float[,] bulletMinSpeed = new float[16, 4];
    public float[,] bulletSpeedChangeFrequency = new float[16, 4];


    //travel then target storage
    public float[,] timeUntilChange = new float[16, 4];
    public int[,] newTargetingType = new int[16, 4];
    public float[,] speedAfterTarget = new float[16, 4];
}
