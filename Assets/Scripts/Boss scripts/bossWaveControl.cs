using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class bossWaveControl : MonoBehaviour
{

    int numOfWaves;
    int[] numOfSubwaves;

    int currentWave;
    int currentSubwave;

    bossHealth Health;

    GameObject[] mainTurrets;
    GameObject[] turret;
    Turret[] turretStreams;
    float percentCutoff;


    public bool fightingBoss;

    bool arraysSet;

    bool wavesStarted;
    // Start is called before the first frame update
    void Start()
    {  
        currentWave = 0;
        currentSubwave = 0;
        Health = GetComponent<bossHealth>();
        wavesStarted = false;
    }

    // Update is called once per frame
    void setArrays()
    {
        if (!arraysSet)
        {
            mainTurrets = GameObject.FindGameObjectsWithTag("turret Main");
            turret = GameObject.FindGameObjectsWithTag("Turret");

            Array.Resize(ref turretStreams, turret.Length);
            for (int i = 0; i < turret.Length; i++)
            {
                turretStreams[i] = turret[i].GetComponent<Turret>();
            }

            numOfSubwaves = mainTurrets[0].GetComponent<turretSubwaveStorage>().SubwaveCount;
            numOfWaves = mainTurrets[0].GetComponent<turretSubwaveStorage>().totalWaveCount;

            for (int i = 0; i < mainTurrets.Length; i++)
            {
                if (mainTurrets[i].GetComponent<turretSubwaveStorage>().activeInWave[currentWave])
                {
                    mainTurrets[i].SetActive(true);

                }
                else
                {
                    mainTurrets[i].SetActive(false);
                }
            }
        }
        percentCutoff = Health.health / numOfWaves;

    }
    void Update()
    {
        if (fightingBoss)
        {
            setArrays();
            arraysSet = true;

            if (!wavesStarted)
                StartCoroutine(runSubwave(((currentWave) * 4) + currentSubwave));

            //Debug.Log(((currentWave) * 4) + currentSubwave);
            wavesStarted = true;

           // Debug.Log("Current wave: " + currentWave);
           // Debug.Log("percentCutoff: " + percentCutoff);
           // Debug.Log("health check: " + ((numOfWaves-1 - currentWave) * percentCutoff));
            if (Health.health < ((numOfWaves-1 - currentWave) * percentCutoff))
            {
                currentWave++;
            }
            
            if (currentWave > numOfWaves)
            {
                Debug.Log("thats it... you won");
            }
        }
        else
        {
            currentWave = 0;
            currentSubwave = 0;
        }
        
    }



    //   if (currentSubwave < numOfSubwaves[currentWave])
    IEnumerator runSubwave(int subwaveNum)
    {
        Debug.Log("subwaveNum = " + subwaveNum);
        if (currentSubwave < numOfSubwaves[currentWave])
            setValues(subwaveNum);
        currentSubwave++;
        yield return new WaitForSeconds(2);

        if (currentSubwave >= numOfSubwaves[currentWave])
            currentSubwave = 0;

        if (fightingBoss)
        {
            StartCoroutine(runSubwave(((currentWave) * 4) + currentSubwave));
        }
        else
        {
            yield break;
        }
        

    }

    void setValues(int subwaveNum)
    {
        for (int i = 0; i < turret.Length; i++)
        {
            turretStreams[i].streamEnabled = mainTurrets[0].GetComponent<turretSubwaveStorage>().streamEnabled[subwaveNum];
            Debug.Log("Is stream enabled?" + mainTurrets[0].GetComponent<turretSubwaveStorage>().streamEnabled[subwaveNum]);
            if (turretStreams[i].streamEnabled == true)
            {
                turretStreams[i].GetComponent<Turret_Fire>().enabled = true;
                turretStreams[i].GetComponent<Turret_Targeting>().enabled = true;
                turretStreams[i].GetComponent<Turret_BulletSetup>().enabled = true;
            }
            else
            {
                turretStreams[i].GetComponent<Turret_Fire>().enabled = false;
                turretStreams[i].GetComponent<Turret_Targeting>().enabled = false;
                turretStreams[i].GetComponent<Turret_BulletSetup>().enabled = false;
            }


            turretStreams[i].turretHealth = mainTurrets[0].GetComponent<turretSubwaveStorage>().turretHealth[subwaveNum];
            turretStreams[i].fireType = mainTurrets[0].GetComponent<turretSubwaveStorage>().fireType[subwaveNum];
            turretStreams[i].targetingType = mainTurrets[0].GetComponent<turretSubwaveStorage>().targetingType[subwaveNum];
            turretStreams[i].rotateSpeed = mainTurrets[0].GetComponent<turretSubwaveStorage>().rotateSpeed[subwaveNum];
            turretStreams[i].smoothTarget = mainTurrets[0].GetComponent<turretSubwaveStorage>().smoothTarget[subwaveNum];
            turretStreams[i].smoothTargetSpeed = mainTurrets[0].GetComponent<turretSubwaveStorage>().smoothTargetSpeed[subwaveNum];
            turretStreams[i].targetPlayerOffsetAmmount = mainTurrets[0].GetComponent<turretSubwaveStorage>().targetPlayerOffsetAmmount[subwaveNum];
            turretStreams[i].rotateAngleDirection = mainTurrets[0].GetComponent<turretSubwaveStorage>().rotateAngleDirection[subwaveNum];
            turretStreams[i].rotateAngleWidth = mainTurrets[0].GetComponent<turretSubwaveStorage>().rotateAngleWidth[subwaveNum];
            turretStreams[i].spiralDirection = mainTurrets[0].GetComponent<turretSubwaveStorage>().spiralDirection[subwaveNum];
            turretStreams[i].singleDirDirection = mainTurrets[0].GetComponent<turretSubwaveStorage>().singleDirDirection[subwaveNum];
            turretStreams[i].bulletFormation = mainTurrets[0].GetComponent<turretSubwaveStorage>().bulletFormation[subwaveNum];
            turretStreams[i].numOfBullets = mainTurrets[0].GetComponent<turretSubwaveStorage>().numOfBullets[subwaveNum];
            turretStreams[i].firerate = mainTurrets[0].GetComponent<turretSubwaveStorage>().firerate[subwaveNum];
            turretStreams[i].bulletDelay = mainTurrets[0].GetComponent<turretSubwaveStorage>().bulletDelay[subwaveNum];
            turretStreams[i].bulletSpeedIncreaseAmmount = mainTurrets[0].GetComponent<turretSubwaveStorage>().bulletSpeedIncreaseAmmount[subwaveNum];
            turretStreams[i].angleBetweenBullets = mainTurrets[0].GetComponent<turretSubwaveStorage>().angleBetweenBullets[subwaveNum];
            turretStreams[i].bulletRandomRange = mainTurrets[0].GetComponent<turretSubwaveStorage>().bulletRandomRange[subwaveNum];
            turretStreams[i].shotgunStraight = mainTurrets[0].GetComponent<turretSubwaveStorage>().shotgunStraight[subwaveNum];
            turretStreams[i].bulletSpeedIncreaseCheck = mainTurrets[0].GetComponent<turretSubwaveStorage>().bulletSpeedIncreaseCheck[subwaveNum];
            turretStreams[i].bulletMovementType = mainTurrets[0].GetComponent<turretSubwaveStorage>().bulletMovementType[subwaveNum];
            turretStreams[i].bulletBaseSpeed = mainTurrets[0].GetComponent<turretSubwaveStorage>().bulletBaseSpeed[subwaveNum];

        }
        
    }
}
