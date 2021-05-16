using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

using SFB;


public class SaveBoss : MonoBehaviour
{

    public string SaveText;
    string path;
    public GameObject turret;
    public GameObject boss;
    public UIManager uIManager;
    public UICustomisationManager uICustomisationManager;


    private saveTurretSubwave save;
    private turretSubwaveStorage load;

    public GameObject[] turrets;
    GameObject spawnedTurret;
    public string[] lines;

    public string[] Filepath;

    public OnGameStart ongameStart;

    private void Start()
    {
        Filepath = StaticFilePath.filePath;
        if (Filepath != null)
        {
            LoadBoss(Filepath);
        }
       //uIManager = GetComponent<UIManager>();
    }

    public void saveBoss()
    {
        //path = Application.dataPath + "/SAVES/testSave.txt";
        turrets = GameObject.FindGameObjectsWithTag("turret Main");
        for (int i = 0; i < turrets.Length; i++)
        {
            saveTurretSubwave temp = SaveSubwaveStorage(turrets[i].GetComponent<turretSubwaveStorage>());
            SaveText += JsonUtility.ToJson(temp) + "\n";
        }

#if UNITY_STANDALONE
        var SaveWindowBuild = StandaloneFileBrowser.SaveFilePanel(
                    "Save texture as PNG",
                    "",
                    "boss name" + ".txt",
                    "txt");
        if (SaveWindowBuild.Length != 0)
        {
            File.WriteAllText(SaveWindowBuild, SaveText);
        }
#endif
    }
    
    //sets all the values in the saveTurretSubwave script with the values in temp
    public saveTurretSubwave SaveSubwaveStorage(turretSubwaveStorage temp)
    {
        save = new saveTurretSubwave();

        save.totalWaveCount = temp.totalWaveCount;
        save.SubwaveCount = temp.SubwaveCount;
        save.TotalNumberOfTurrets = temp.TotalNumberOfTurrets;
        save.turretLocation = temp.turretLocation;
        save.activeInWave =temp.activeInWave;

        save.isDestroyable = temp.isDestroyable;

        save.numberActiveStreams =temp.numberActiveStreams;
        save.subwaveDuration =temp.subwaveDuration;

        save.turretHealth = temp.turretHealth;

        //Loops
        int count = 0;
        //Debug.Log("");
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 16; j++)
            {

                //Debug.Log(count + " ," + i+ " ,"+j);
                save.streamEnabled[count] = temp.streamEnabled[j, i];


                save.turretSavedOnce[count] = temp.turretSavedOnce[j, i];
            save.fireType[count] = temp.fireType[j, i];
            save.targetingType[count] = temp.targetingType[j, i];
            save.rotateSpeed[count] =temp.rotateSpeed[j, i];

            //public Vector4[] temp = new Vector4[16];

            // Target player settings
            save.smoothTarget[count] = temp.smoothTarget[j, i];
                save.smoothTargetSpeed[count] = temp.smoothTargetSpeed[j, i];
            save.targetPlayerOffsetAmmount[count] =temp.targetPlayerOffsetAmmount[j, i];


                // arc targetting settings
                save.rotateAngleDirection[count] = temp.rotateAngleDirection[j, i];
            save.rotateAngleWidth[count] =temp.rotateAngleWidth[j, i];


            // spiral targetting settings
            save.spiralDirection[count] = temp.spiralDirection[j, i];

            //single direction settings
            save.singleDirDirection[count] =temp.singleDirDirection[j, i];


            //Bullet fire variables  
            save.bulletFormation[count] = temp.bulletFormation[j, i];
            save.numOfBullets[count] = temp.numOfBullets[j, i];
            //firerate determines the gap between each burst of shots
            save.firerate[count] =temp.firerate[j, i];
            //bullet delay determins the gap between each individual bullet in a burst (if they are staggered)
            save.bulletDelay[count] =temp.bulletDelay[j, i];
            save.bulletSpeedIncreaseAmmount[count] =temp.bulletSpeedIncreaseAmmount[j, i];
            save.angleBetweenBullets[count] =temp.angleBetweenBullets[j, i];
            save.bulletRandomRange[count] = temp.bulletRandomRange[j, i];

            save.shotgunStraight[count] = temp.shotgunStraight[j, i];
            save.bulletSpeedIncreaseCheck[count] = temp.bulletSpeedIncreaseCheck[j, i];

            //individual bullet settings (what movement type they use  their speed etc)
            save.bulletMovementType[count] = temp.bulletMovementType[j, i];
            save.bulletBaseSpeed[count] =temp.bulletBaseSpeed[j, i];

            //sin wave storage
            save.bulletAmplitude[count] =temp.bulletAmplitude[j, i];
            save.bulletFrequency[count] =temp.bulletFrequency[j, i];

            //variable speed storage
            save.bulletMaxSpeed[count] =temp.bulletMaxSpeed[j, i];
            save.bulletMinSpeed[count] =temp.bulletMinSpeed[j, i];
            save.bulletSpeedChangeFrequency[count] =temp.bulletSpeedChangeFrequency[j, i];


            //travel then target storage
            save.timeUntilChange[count] =temp.timeUntilChange[j, i];
            save.newTargetingType[count] = temp.newTargetingType[j, i];
            save.speedAfterTarget[count] =temp.speedAfterTarget[j, i];

                save.bulletType[count] = temp.bulletType[j, i];

            count++;
            }
            //count++;
        }


        return save;
    }

    public turretSubwaveStorage LoadSubwaveStorage(saveTurretSubwave temp)
    {
        load = new turretSubwaveStorage();

        load.totalWaveCount = temp.totalWaveCount;
        load.SubwaveCount = temp.SubwaveCount;
        load.TotalNumberOfTurrets = temp.TotalNumberOfTurrets;
        load.turretLocation = temp.turretLocation;
        load.activeInWave = temp.activeInWave;


        load.isDestroyable = temp.isDestroyable;

        load.numberActiveStreams = temp.numberActiveStreams;
        load.subwaveDuration = temp.subwaveDuration;

        load.turretHealth = temp.turretHealth;

        //Loops
        int count = 0;
        //Debug.Log("");
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 16; j++)
            {

                //Debug.Log(count + " ," + i+ " ,"+j);
                load.streamEnabled[j, i] = temp.streamEnabled[count];


                load.turretSavedOnce[j, i] = temp.turretSavedOnce[count];
                load.fireType[j, i] = temp.fireType[count];
                load.targetingType[j, i] = temp.targetingType[count];
                load.rotateSpeed[j, i] = temp.rotateSpeed[count];

                //public Vector4[] temp = new Vector4[16];

                // Target player settings
                load.smoothTarget[j, i] = temp.smoothTarget[count];
                load.smoothTargetSpeed[j, i] = temp.smoothTargetSpeed[count];
                load.targetPlayerOffsetAmmount[j, i] = temp.targetPlayerOffsetAmmount[count];


                // arc targetting settings
                load.rotateAngleDirection[j, i] = temp.rotateAngleDirection[count];
                load.rotateAngleWidth[j, i] = temp.rotateAngleWidth[count];


                // spiral targetting settings
                load.spiralDirection[j, i] = temp.spiralDirection[count];

                //single direction settings
                load.singleDirDirection[j, i] = temp.singleDirDirection[count];


                //Bullet fire variables  
                load.bulletFormation[j, i] = temp.bulletFormation[count];
                load.numOfBullets[j, i] = temp.numOfBullets[count];
                //firerate determines the gap between each burst of shots
                load.firerate[j, i] = temp.firerate[count];
                //bullet delay determins the gap between each individual bullet in a burst (if they are staggered)
                load.bulletDelay[j, i] = temp.bulletDelay[count];
                load.bulletSpeedIncreaseAmmount[j, i] = temp.bulletSpeedIncreaseAmmount[count];
                load.angleBetweenBullets[j, i] = temp.angleBetweenBullets[count];
                load.bulletRandomRange[j, i] = temp.bulletRandomRange[count];

                load.shotgunStraight[j, i] = temp.shotgunStraight[count];
                load.bulletSpeedIncreaseCheck[j, i] = temp.bulletSpeedIncreaseCheck[count];

                //individual bullet settings (what movement type they use  their speed etc)
                load.bulletMovementType[j, i] = temp.bulletMovementType[count];
                load.bulletBaseSpeed[j, i] = temp.bulletBaseSpeed[count];

                //sin wave storage
                load.bulletAmplitude[j, i] = temp.bulletAmplitude[count];
                load.bulletFrequency[j, i] = temp.bulletFrequency[count];

                //variable speed storage
                load.bulletMaxSpeed[j, i] = temp.bulletMaxSpeed[count];
                load.bulletMinSpeed[j, i] = temp.bulletMinSpeed[count];
                load.bulletSpeedChangeFrequency[j, i] = temp.bulletSpeedChangeFrequency[count];


                //travel then target storage
                load.timeUntilChange[j, i] = temp.timeUntilChange[count];
                load.newTargetingType[j, i] = temp.newTargetingType[count];
                load.speedAfterTarget[j, i] = temp.speedAfterTarget[count];

                load.bulletType[j, i] = temp.bulletType[count];

                count++;
            }
            //count++;
        }


        return load;
    }

    public void setAll(turretSubwaveStorage temp, turretSubwaveStorage turretToSet, GameObject turretBody)
    {

        turretToSet.totalWaveCount = temp.totalWaveCount;
        turretToSet.SubwaveCount = temp.SubwaveCount;
        turretToSet.TotalNumberOfTurrets = temp.TotalNumberOfTurrets;
        turretToSet.turretLocation = temp.turretLocation;
        turretToSet.activeInWave = temp.activeInWave;
        turretToSet.numberActiveStreams = temp.numberActiveStreams;
        turretToSet.subwaveDuration = temp.subwaveDuration;

        turretToSet.isDestroyable = temp.isDestroyable;

        turretToSet.turretHealth = temp.turretHealth;

        //Loops
        int count = 0;
        //Debug.Log("");
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 16; j++)
            {

                //Debug.Log(count + " ," + i+ " ,"+j);
                turretToSet.streamEnabled[j, i] = temp.streamEnabled[j, i];


                turretToSet.turretSavedOnce[j, i] = temp.turretSavedOnce[j, i];
                turretToSet.fireType[j, i] = temp.fireType[j, i];
                turretToSet.targetingType[j, i] = temp.targetingType[j, i];
                turretToSet.rotateSpeed[j, i] = temp.rotateSpeed[j, i];

                //public Vector4[] temp = new Vector4[16];

                // Target player settings
                turretToSet.smoothTarget[j, i] = temp.smoothTarget[j, i];
                turretToSet.smoothTargetSpeed[j, i] = temp.smoothTargetSpeed[j, i];
                turretToSet.targetPlayerOffsetAmmount[j, i] = temp.targetPlayerOffsetAmmount[j, i];


                // arc targetting settings
                turretToSet.rotateAngleDirection[j, i] = temp.rotateAngleDirection[j, i];
                turretToSet.rotateAngleWidth[j, i] = temp.rotateAngleWidth[j, i];


                // spiral targetting settings
                turretToSet.spiralDirection[j, i] = temp.spiralDirection[j, i];

                //single direction settings
                turretToSet.singleDirDirection[j, i] = temp.singleDirDirection[j, i];


                //Bullet fire variables  
                turretToSet.bulletFormation[j, i] = temp.bulletFormation[j, i];
                turretToSet.numOfBullets[j, i] = temp.numOfBullets[j, i];
                //firerate determines the gap between each burst of shots
                turretToSet.firerate[j, i] = temp.firerate[j, i];
                //bullet delay determins the gap between each individual bullet in a burst (if they are staggered)
                turretToSet.bulletDelay[j, i] = temp.bulletDelay[j, i];
                turretToSet.bulletSpeedIncreaseAmmount[j, i] = temp.bulletSpeedIncreaseAmmount[j, i];
                turretToSet.angleBetweenBullets[j, i] = temp.angleBetweenBullets[j, i];
                turretToSet.bulletRandomRange[j, i] = temp.bulletRandomRange[j, i];

                turretToSet.shotgunStraight[j, i] = temp.shotgunStraight[j, i];
                turretToSet.bulletSpeedIncreaseCheck[j, i] = temp.bulletSpeedIncreaseCheck[j, i];

                //individual bullet settings (what movement type they use  their speed etc)
                turretToSet.bulletMovementType[j, i] = temp.bulletMovementType[j, i];
                turretToSet.bulletBaseSpeed[j, i] = temp.bulletBaseSpeed[j, i];

                //sin wave storage
                turretToSet.bulletAmplitude[j, i] = temp.bulletAmplitude[j, i];
                turretToSet.bulletFrequency[j, i] = temp.bulletFrequency[j, i];

                //variable speed storage
                turretToSet.bulletMaxSpeed[j, i] = temp.bulletMaxSpeed[j, i];
                turretToSet.bulletMinSpeed[j, i] = temp.bulletMinSpeed[j, i];
                turretToSet.bulletSpeedChangeFrequency[j, i] = temp.bulletSpeedChangeFrequency[j, i];


                //travel then target storage
                turretToSet.timeUntilChange[j, i] = temp.timeUntilChange[j, i];
                turretToSet.newTargetingType[j, i] = temp.newTargetingType[j, i];
                turretToSet.speedAfterTarget[j, i] = temp.speedAfterTarget[j, i];

                turretToSet.bulletType[j, i] = temp.bulletType[j, i];

                count++;
            }
            //count++;
        }


        Turret[] turrets = new Turret[4];

        turrets[0] = turretBody.transform.GetChild(0).GetComponent<Turret>();
        turrets[1] = turretBody.transform.GetChild(1).GetComponent<Turret>();
        turrets[2] = turretBody.transform.GetChild(2).GetComponent<Turret>();
        turrets[3] = turretBody.transform.GetChild(3).GetComponent<Turret>();
    }


    public void LoadBoss()
    {
#if UNITY_STANDALONE
        string[] readFileBoxBuild = StandaloneFileBrowser.OpenFilePanel("Load a boss", "", "txt", false);
        if (readFileBoxBuild.Length != 0)
        {
            lines = File.ReadAllLines(readFileBoxBuild[0]);
            NumOfTurrets = 1;

            //Debug.Log(lines[0]);
            //Debug.Log(lines[1]);
            //Debug.Log(lines[2]);


            for (int i = 1; i < boss.transform.childCount; i++)
            {
                Destroy(boss.transform.GetChild(i).gameObject);
            }

            NumOfTurretsLeft = 0;
            StartCoroutine(loadBossTile());

            Debug.Log("boss loaded");



            uIManager.turretSelected(spawnedTurret.transform.GetChild(0).gameObject);
            uIManager.TestBoss.interactable = true;
            int wavenum = spawnedTurret.GetComponent<turretSubwaveStorage>().totalWaveCount;
            int subwavenum = spawnedTurret.GetComponent<turretSubwaveStorage>().SubwaveCount[0];
            uICustomisationManager.setWaveAndSubwaveValues(wavenum, subwavenum);

            //StartCoroutine(waitaFrame());
            uIManager.deselect();
            StartCoroutine(noFire());
        }
#endif
    }
    IEnumerator noFire()
    {
        for (int i = 0; i < 2; i++)
        {
            //yield return null;
            yield return new WaitForEndOfFrame();
        }
        
        ongameStart.TestMode();

        // gameManager.GetComponent<OnGameStart>().TestMode();


        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        Debug.Log(turrets.Length);
        for (int i = 0; i < turrets.Length; i++)
        {
            turrets[i].GetComponent<Turret_Fire>().enabled = false;
            turrets[i].GetComponent<Turret_Targeting>().enabled = false;
            turrets[i].GetComponent<Turret_BulletSetup>().enabled = false;
            turrets[i].GetComponentInParent<BoxCollider2D>().enabled = false;
            turrets[i].GetComponentInParent<BoxCollider2D>().enabled = true;
            turrets[i].transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        yield return new WaitForEndOfFrame();
    }

    int NumOfTurretsLeft = 0;
    int NumOfTurrets = 1;
    public void LoadBoss(string[] filePath)
    {
         lines = File.ReadAllLines(filePath[0]);
            NumOfTurrets = 1;

            //Debug.Log(lines[0]);
            //Debug.Log(lines[1]);
            //Debug.Log(lines[2]);


            for (int i = 1; i < boss.transform.childCount; i++)
            {
                Destroy(boss.transform.GetChild(i).gameObject);
            }

            NumOfTurretsLeft = 0;
            StartCoroutine(loadBossTile());

            Debug.Log("boss loaded");



            uIManager.turretSelected(spawnedTurret.transform.GetChild(0).gameObject);
            uIManager.TestBoss.interactable = true;
            int wavenum = spawnedTurret.GetComponent<turretSubwaveStorage>().totalWaveCount;
            int subwavenum = spawnedTurret.GetComponent<turretSubwaveStorage>().SubwaveCount[0];
            uICustomisationManager.setWaveAndSubwaveValues(wavenum, subwavenum);

            //StartCoroutine(waitaFrame());
            uIManager.deselect();
            StartCoroutine(noFire());

}
IEnumerator loadBossTile()
    {

            spawnedTurret = Instantiate(turret, new Vector3(0, 0, 0), transform.rotation) as GameObject;

            save = new saveTurretSubwave();
            JsonUtility.FromJsonOverwrite(lines[NumOfTurretsLeft], save);

            load = LoadSubwaveStorage(save);

            setAll(load, spawnedTurret.GetComponent<turretSubwaveStorage>(), spawnedTurret);

            spawnedTurret.transform.parent = boss.transform;
            spawnedTurret.transform.localPosition = spawnedTurret.GetComponent<turretSubwaveStorage>().turretLocation;

            NumOfTurrets = spawnedTurret.GetComponent<turretSubwaveStorage>().TotalNumberOfTurrets;




            //Debug.Log(spawnedTurret.GetComponent<turretSubwaveStorage>().spiralDirection[0, 0]);
            boss.transform.GetChild(0).GetComponent<bossWaveControl>().setArrays();
            boss.transform.GetChild(0).GetComponent<bossWaveControl>().setValues(0);
            NumOfTurretsLeft++;

            if (NumOfTurretsLeft >= NumOfTurrets)
            {
                yield return new WaitForEndOfFrame();
                yield return new WaitForEndOfFrame();
            }
            else
            {
                yield return new WaitForEndOfFrame();
                yield return new WaitForEndOfFrame();
            StartCoroutine(loadBossTile());
            }
        

    }


}
