using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    public int fireType;
    [SerializeField]
    public float firerate;
    [SerializeField]
    public int targetingType;
    [SerializeField]
    public int rotateAngleDirection;
    [SerializeField]
    public int rotateAngleWidth;

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
