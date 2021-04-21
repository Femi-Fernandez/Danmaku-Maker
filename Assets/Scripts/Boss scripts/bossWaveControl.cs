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
            turret = GameObject.FindGameObjectsWithTag("Turret");
            TurretSubwaveStorage = mainTurrets[0].GetComponent<turretSubwaveStorage>();
            Array.Resize(ref turretStreams, turret.Length);
            for (int i = 0; i < turret.Length; i++)
            {
                turretStreams[i] = turret[i].GetComponent<Turret>();
            }

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
    }


    public float timeToWait;
    public float currTime;
    //   if (currentSubwave < numOfSubwaves[currentWave])
    IEnumerator runSubwave(int subwaveNum)
    {
        //Debug.Log("subwaveNum = " + subwaveNum);
        if ((currentSubwave < numOfSubwaves[currentWave]) || (numOfSubwaves[currentWave] == 0))
            setValues(subwaveNum);

        timeToWait = mainTurrets[0].GetComponent<turretSubwaveStorage>().subwaveDuration[currentSubwave];
        if (timeToWait == 0)
        {
            timeToWait = 5;
        }
        currTime = 0;
        while (currTime < timeToWait)
        {
            currTime += Time.deltaTime;
            yield return null;
        }
        //yield return new WaitForSeconds(timeToWait);
        currentSubwave++;
        
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
    /// <summary>
    /// for every turret,
    /// set each child active or not
    /// </summary>
    /// <param name="subwaveNum"></param>
    public void setValues(int subwaveNum)
    {

        for (int i = 0; i < mainTurrets.Length; i++)
        {
            mainTurrets[i].SetActive(mainTurrets[i].GetComponent<turretSubwaveStorage>().activeInWave[currentWave]);
            mainTurrets[i].GetComponent<BoxCollider2D>().enabled = mainTurrets[i].GetComponent<turretSubwaveStorage>().isDestroyable[currentWave];
            mainTurrets[i].SetActive(!TurretSubwaveStorage.hasBeenDestroyed[currentWave]);
            // Debug.Log("should hitbox be off?" + mainTurrets[i].GetComponent<turretSubwaveStorage>().isDestroyable[currentWave]);     
        }


        for (int i = 0; i < mainTurrets.Length; i++)
        {

            if (mainTurrets[i].GetComponent<turretSubwaveStorage>() != null)
            {
                TurretSubwaveStorage = mainTurrets[i].GetComponent<turretSubwaveStorage>();
               // Debug.Log("has the turret been destroyed?" + mainTurrets[i].GetComponent<turretSubwaveStorage>().hasBeenDestroyed[currentWave]);
                if (TurretSubwaveStorage.hasBeenDestroyed[currentWave] == false)
                {
                    //Debug.Log("only here if its not dead");
                    for (int j = 0; j < 4; j++)
                    {
                        // Debug.Log(": " + i);
                        mainTurrets[i].transform.GetChild(j).GetComponent<Turret>().streamEnabled = TurretSubwaveStorage.streamEnabled[subwaveNum, j];
                        //Debug.Log("subwaveNum: " + subwaveNum);
                        if (TurretSubwaveStorage.streamEnabled[subwaveNum, j])
                        {
                            // Debug.Log("enable stream: " + j);
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
