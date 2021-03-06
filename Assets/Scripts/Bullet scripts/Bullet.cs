﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float movementType;
    public float lifeTime;
    public float speed;

    //sine wave movement
    public float amplitude;
    public float frequency;
    public float waveLength;

    //varaible speed movement
    public float maxSpeed;
    public float minSpeed;
    public float speedChangeFrequency;

    public float timeUntilChange;
    public int newTargetingType;
    public float speedAfterTarget;
}
