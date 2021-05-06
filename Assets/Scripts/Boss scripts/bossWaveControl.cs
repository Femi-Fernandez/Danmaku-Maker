using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class bossWaveControl : MonoBehaviour
{

    int numOfWaves;
    int[] numOfSubwaves;

    public int currentWave;
    public int currentSubwave;

    bossHealth Health;

    public GameObject[] mainTurrets;
    public GameObject[] turret;
    public Turret[] turretStreams;
    public turretSubwaveStorage TurretSubwaveStorage;
   // GameObject[] turretChildren;
    float percentCutoff;


    public bool fightingBoss;

    bool arraysSet;

    public bool wavesStarted;

    public AnalyticsCommands AC;
    // Start is called before the first frame update
    void Start()
    {  
        currentWave = 0;
        currentSubwave = 0;
        Health = GetComponent<bossHealth>();
        wavesStarted = false;
    }

    // Update is called once per frame
    public void setArrays()
    {
        if (!arraysSet)
        {
            mainTurrets = GameObject.FindGameObjectsWithTag("turret Main");
            //turret = GameObject.FindGameObjectsWithTag("Turret");
            TurretSubwaveStorage = mainTurrets[0].GetComponent<turretSubwaveStorage>();
           // Array.Resize(ref turretStreams, turret.Length);
           // for (int i = 0; i < turret.Length; i++)
           // {
           //     turretStreams[i] = turret[i].GetComponent<Turret>();
           // }

            numOfSubwaves = TurretSubwaveStorage.SubwaveCount;
            numOfWaves = TurretSubwaveStorage.totalWaveCount;
        }
        

    }
    void Update()
    {
        if (fightingBoss)
        {
            setArrays();
            arraysSet = true;

            if (!wavesStarted)
            {
                StartCoroutine(runSubwave(((currentWave) * 4) + currentSubwave));
                percentCutoff = Health.health / numOfWaves;
            }

            //Debug.Log(((currentWave) * 4) + currentSubwave);
            wavesStarted = true;

            // Debug.Log("Current wave: " + currentWave);
            // Debug.Log("percentCutoff: " + percentCutoff);
            // Debug.Log("health check: " + ((numOfWaves-1 - currentWave) * percentCutoff));
            if (Health.health < ((numOfWaves-1 - currentWave) * percentCutoff))
            {
                Debug.Log("Curent wave increase!");
                currentWave++;
            }
            
            if (currentWave > numOfWaves)
            {
                AC.bossDefeated();
                Debug.Log("thats it... you won");
            }
        }
        else
        {
            arraysSet = false;

        }      
    }


    public float timeToWait;
    public float currTime;
    //sub-Phase control
    IEnumerator runSubwave(int subwaveNum)
    {
        //if current sub-phase exists, set values of all turrets
        if ((currentSubwave < numOfSubwaves[currentWave]) || (numOfSubwaves[currentWave] == 0))
            setValues(subwaveNum);

        //how long is the sub-phase? if no value present, set it to default of 5 seconds
        timeToWait = mainTurrets[0].GetComponent<turretSubwaveStorage>().subwaveDuration[currentSubwave];
        if (timeToWait == 0)
        {
            timeToWait = 5;
        }
        currTime = 0;
        //loop until it has been 5 seconds. 
        while (currTime < timeToWait)
        {
            currTime += Time.deltaTime;
            yield return null;
        }
        //once 5 seconds are up, increase sub-phase count
        currentSubwave++;

        //if sub-phase count goes over the number present in the current phase, reset subphase counter.
        if (currentSubwave >= numOfSubwaves[currentWave])
            currentSubwave = 0;

        //if still fighting boss, reset coroutine, otherwise cancel.
        if (fightingBoss)
        {
            StartCoroutine(runSubwave(((currentWave) * 4) + currentSubwave));
        }
        else
        {
            yield break;
        }
        

    }

    public void setValues(int subwaveNum)
    {

        //for each turret, checks if it is active/destroyable in current wave and sets its values to ones stored. 
        for (int i = 0; i < mainTurrets.Length; i++)
        {
            mainTurrets[i].SetActive(mainTurrets[i].GetComponent<turretSubwaveStorage>().activeInWave[currentWave]);
            mainTurrets[i].GetComponent<BoxCollider2D>().enabled = mainTurrets[i].GetComponent<turretSubwaveStorage>().isDestroyable[currentWave];
            mainTurrets[i].SetActive(!TurretSubwaveStorage.hasBeenDestroyed[currentWave]);
            // Debug.Log("should hitbox be off?" + mainTurrets[i].GetComponent<turretSubwaveStorage>().isDestroyable[currentWave]);     
        }

        //for each turret
        for (int i = 0; i < mainTurrets.Length; i++)
        {
            //safety check
            if (mainTurrets[i].GetComponent<turretSubwaveStorage>() != null)
            {
                TurretSubwaveStorage = mainTurrets[i].GetComponent<turretSubwaveStorage>();
                //if the turret hasnt been destroyed, 
                if (TurretSubwaveStorage.hasBeenDestroyed[currentWave] == false)
                {
                    //for each stream 
                    for (int j = 0; j < 4; j++)
                    {
                        //enable the stream (if stream was enabled)
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().streamEnabled = TurretSubwaveStorage.streamEnabled[subwaveNum, j];
                        
                        if (TurretSubwaveStorage.streamEnabled[subwaveNum, j])
                        {
                            //if stream is enabled, set it to be ready to fire. 
                            mainTurrets[i].transform.GetChild(j).gameObject.SetActive(true);
                            mainTurrets[i].transform.GetChild(j).GetComponent<Turret_Fire>().enabled = true;
                            mainTurrets[i].transform.GetChild(j).GetComponent<Turret_Fire>().fireTimer = 1000f;
                            // mainTurrets[i].transform.GetChild(j).GetComponent<Turret_Fire>().readyToFire = true;
                            mainTurrets[i].transform.GetChild(j).GetComponent<Turret_Targeting>().enabled = true;
                            mainTurrets[i].transform.GetChild(j).GetComponent<Turret_BulletSetup>().enabled = true;
                        }
                        else
                        {
                            mainTurrets[i].transform.GetChild(j).gameObject.SetActive(false);
                            mainTurrets[i].transform.GetChild(j).GetComponent<Turret_Fire>().enabled = false;
                            mainTurrets[i].transform.GetChild(j).GetComponent<Turret_Targeting>().enabled = false;
                            mainTurrets[i].transform.GetChild(j).GetComponent<Turret_BulletSetup>().enabled = false;
                        }

                        //set all values for the stream
                        //turret firing style settings
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().turretHealth = TurretSubwaveStorage.turretHealth[currentWave];
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().fireType = TurretSubwaveStorage.fireType[subwaveNum, j];
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().targetingType = TurretSubwaveStorage.targetingType[subwaveNum, j];
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().rotateSpeed = TurretSubwaveStorage.rotateSpeed[subwaveNum, j];
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().smoothTarget = TurretSubwaveStorage.smoothTarget[subwaveNum, j];
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().smoothTargetSpeed = TurretSubwaveStorage.smoothTargetSpeed[subwaveNum, j];
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().targetPlayerOffsetAmmount = TurretSubwaveStorage.targetPlayerOffsetAmmount[subwaveNum, j];
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().rotateAngleDirection = TurretSubwaveStorage.rotateAngleDirection[subwaveNum, j];
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().rotateAngleWidth = TurretSubwaveStorage.rotateAngleWidth[subwaveNum, j];
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().spiralDirection = TurretSubwaveStorage.spiralDirection[subwaveNum, j];
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().singleDirDirection = TurretSubwaveStorage.singleDirDirection[subwaveNum, j];

                        //bullet formation settings
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().bulletFormation = TurretSubwaveStorage.bulletFormation[subwaveNum, j];
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().numOfBullets = TurretSubwaveStorage.numOfBullets[subwaveNum, j];
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().firerate = TurretSubwaveStorage.firerate[subwaveNum, j];
                     // mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().bulletDelay = TurretSubwaveStorage.bulletDelay[subwaveNum, j];
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().bulletSpeedIncreaseAmmount = TurretSubwaveStorage.bulletSpeedIncreaseAmmount[subwaveNum, j];
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().angleBetweenBullets = TurretSubwaveStorage.angleBetweenBullets[subwaveNum, j];
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().bulletRandomRange = TurretSubwaveStorage.bulletRandomRange[subwaveNum, j];
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().shotgunStraight = TurretSubwaveStorage.shotgunStraight[subwaveNum, j];
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().bulletSpeedIncreaseCheck = TurretSubwaveStorage.bulletSpeedIncreaseCheck[subwaveNum, j];


                        //individual bullet movement settings
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().bulletMovementType = TurretSubwaveStorage.bulletMovementType[subwaveNum, j];
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().bulletBaseSpeed = TurretSubwaveStorage.bulletBaseSpeed[subwaveNum, j];

                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().bulletAmplitude = TurretSubwaveStorage.bulletAmplitude[subwaveNum, j];
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().bulletFrequency = TurretSubwaveStorage.bulletFrequency[subwaveNum, j];

                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().bulletMaxSpeed = TurretSubwaveStorage.bulletMaxSpeed[subwaveNum, j];
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().bulletMinSpeed = TurretSubwaveStorage.bulletMinSpeed[subwaveNum, j];
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().bulletSpeedChangeFrequency = TurretSubwaveStorage.bulletSpeedChangeFrequency[subwaveNum, j];

                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().bulletTimeUntilChange = TurretSubwaveStorage.timeUntilChange[subwaveNum, j];
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().bulletNewTargetingType = TurretSubwaveStorage.newTargetingType[subwaveNum, j];
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().bulletSpeedAfterTarget = TurretSubwaveStorage.speedAfterTarget[subwaveNum, j];

                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().bulletType = TurretSubwaveStorage.bulletType[subwaveNum, j];

                    }
                }
            }
        }             
    }
}
