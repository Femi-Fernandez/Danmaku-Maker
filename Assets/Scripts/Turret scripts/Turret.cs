using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    /// <summary>
    ///check if turret is active
    /// </summary>
    [SerializeField]
    public int streamNumber;
    [SerializeField]
    public bool streamEnabled;

    /// <summary>
    /// generic turret settings (applies to all)
    /// </summary>
    [SerializeField]
    public int fireType;
    [SerializeField]
    public float firerate;
    [SerializeField]
    public int targetingType;

    /// <summary>
    /// Target player settings
    /// </summary>
    [SerializeField]
    public bool smoothTarget;
    [SerializeField]
    public float smoothTargetSpeed;
    [SerializeField]
    public float targetPlayerOffsetAmmount;

    /// <summary>
    /// arc targetting settings
    /// </summary>
    [SerializeField]
    public float rotateAngleDirection;
    [SerializeField]
    public float rotateAngleWidth;
    [SerializeField]
    public float rotateSpeed;

    /// <summary>
    /// Bullet fire variables 
    /// </summary>
    [SerializeField]
    public int bulletFormation;
    [SerializeField]
    public int numOfBullets;
    [SerializeField]
    public float bulletDelay;

}
