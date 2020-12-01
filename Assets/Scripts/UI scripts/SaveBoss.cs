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

    public GameObject[] turrets;
    public string[] lines;

    public string[] Filepath;

    private void Start()
    {
        Filepath = StaticFilePath.filePath;
        if (Filepath != null)
        {
            LoadBoss(Filepath);
        }
    }

    public void saveBoss()
    {
        //path = Application.dataPath + "/SAVES/testSave.txt";
        turrets = GameObject.FindGameObjectsWithTag("Turret");
        for (int i = 0; i < turrets.Length; i++)
        {

            SaveText += JsonUtility.ToJson(turrets[i].GetComponent<Turret>()) + "\n";
            

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


    public void LoadBoss()
    {
#if UNITY_STANDALONE
        string[] readFileBoxBuild = StandaloneFileBrowser.OpenFilePanel("Load a boss", "", "txt", false);
        if (readFileBoxBuild.Length != 0)
        {
            lines = File.ReadAllLines(readFileBoxBuild[0]);

        }
#endif

        //Debug.Log(lines[0]);
        //Debug.Log(lines[1]);
        //Debug.Log(lines[2]);


        for (int i = 1; i < boss.transform.childCount; i++)
        {
            Destroy(boss.transform.GetChild(i).gameObject);
        }
        


        Debug.Log("lines read");


        int x = 0;
        int ActiveStreams = 1;
        int NumOfTurrets = 1;
        for (int i = 0; i < NumOfTurrets; i++)
        {
            GameObject spawnedTurret = Instantiate(turret, new Vector3(0, 0, 0), transform.rotation) as GameObject;
            
            for (int j = 0; j < ActiveStreams; j++)
            {
                JsonUtility.FromJsonOverwrite(lines[x], spawnedTurret.transform.GetChild(j).GetComponent<Turret>());

                if (!spawnedTurret.transform.GetChild(j).GetComponent<Turret>().streamEnabled)
                    spawnedTurret.transform.GetChild(j).gameObject.SetActive(false);
                ActiveStreams = spawnedTurret.transform.GetChild(j).GetComponent<Turret>().numberActiveStreams;
                x++;
            }
            Debug.Log(spawnedTurret.transform.GetChild(0).GetComponent<Turret>().turretLocation);
            NumOfTurrets = spawnedTurret.transform.GetChild(0).GetComponent<Turret>().TotalNumberOfTurrets;

            spawnedTurret.transform.parent = boss.transform;
            spawnedTurret.transform.localPosition = spawnedTurret.transform.GetChild(0).GetComponent<Turret>().turretLocation;
        }
    }

    public void LoadBoss(string[] filePath)
    {
        lines = File.ReadAllLines(filePath[0]);

        Debug.Log(lines[0]);
        Debug.Log(lines[1]);
        Debug.Log(lines[2]);


        for (int i = 1; i < boss.transform.childCount; i++)
        {
            Destroy(boss.transform.GetChild(i).gameObject);
        }



        Debug.Log("lines read");


        int x = 0;
        int ActiveStreams = 1;
        int NumOfTurrets = 1;
        for (int i = 0; i < NumOfTurrets; i++)
        {
            GameObject spawnedTurret = Instantiate(turret, new Vector3(0, 0, 0), transform.rotation) as GameObject;

            for (int j = 0; j < ActiveStreams; j++)
            {
                JsonUtility.FromJsonOverwrite(lines[x], spawnedTurret.transform.GetChild(j).GetComponent<Turret>());

                if (!spawnedTurret.transform.GetChild(j).GetComponent<Turret>().streamEnabled)
                    spawnedTurret.transform.GetChild(j).gameObject.SetActive(false);
                ActiveStreams = spawnedTurret.transform.GetChild(j).GetComponent<Turret>().numberActiveStreams;
                x++;
            }
            Debug.Log(spawnedTurret.transform.GetChild(0).GetComponent<Turret>().turretLocation);
            NumOfTurrets = spawnedTurret.transform.GetChild(0).GetComponent<Turret>().TotalNumberOfTurrets;

            spawnedTurret.transform.parent = boss.transform;
            spawnedTurret.transform.localPosition = spawnedTurret.transform.GetChild(0).GetComponent<Turret>().turretLocation;
        }
    }
}
