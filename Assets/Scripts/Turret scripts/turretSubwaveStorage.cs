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


    public int TotalNumberOfTurrets;
    public bool[] activeInWave = new bool[4];
    public int[] numberActiveStreams = new int[16];
  //  public int[] streamNumber = new int[16];
    public bool[] streamEnabled = new bool[16];

    // generic turret settings (applies to all)
    public int[] turretHealth = new int[4];
    public int[] fireType = new int[16];
    public int[] targetingType = new int[16];
    public float[] rotateSpeed = new float[16];


    // Target player settings
    public bool[] smoothTarget = new bool[16];
    public float[] smoothTargetSpeed =new float[16];
    public float[] targetPlayerOffsetAmmount = new float[16];


    // arc targetting settings
    public float[] rotateAngleDirection = new float[16];
    public float[] rotateAngleWidth = new float[16];


    // spiral targetting settings
    public bool[] spiralDirection = new bool[16];

    //single direction settings
    public float[] singleDirDirection = new float[16];


    //Bullet fire variables  
    public int[] bulletFormation = new int[16];
    public int[] numOfBullets = new int[16];
    //firerate determines the gap between each burst of shots
    public float[] firerate = new float[16];
    //bullet delay determins the gap between each individual bullet in a burst (if they are staggered)
    public float[] bulletDelay = new float[16];
    public float[] bulletSpeedIncreaseAmmount = new float[16];
    public float[] angleBetweenBullets = new float[16];
    public float[] bulletRandomRange = new float[16];

    public bool[] shotgunStraight = new bool[16];
    public bool[] bulletSpeedIncreaseCheck = new bool[16];

    //individual bullet settings (what movement type they use, their speed etc)
    public int[] bulletMovementType = new int[16];
    public float[] bulletBaseSpeed = new float[16];

}
