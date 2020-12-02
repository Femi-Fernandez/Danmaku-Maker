﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameStart : MonoBehaviour
{

    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject turretPanel;
    [SerializeField]
    private GameObject optionsPanel;

    [SerializeField]
    private GameObject UIManager;

    GameObject[] turretMain;

    GameObject[] bullets;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UIManager.GetComponent<SceneControl>().EnterScene());
        
        TestMode();
    }


    public void TestMode()
    {
        player.SetActive(true);
        boss.SetActive(true);
        turretPanel.SetActive(true);
        boss.transform.GetChild(0).GetComponent<bossHealth>().health = 1000;
        player.GetComponent<playerHealth>().health = 5;
        player.GetComponent<BoxCollider2D>().enabled = false;
        boss.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;

        GameObject[] turrets = GameObject.FindGameObjectsWithTag("turret Main");
        for (int i = 0; i < turrets.Length; i++)
        {
            turrets[i].SetActive(true);
            if (turrets[i].GetComponent<BoxCollider2D>())
            {
                turrets[i].GetComponent<TurretHealth>().health = 100000000000;
            }  
        }
    }

    public void PlayMode()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        for (int i = 0; i < turrets.Length; i++)
        {
            turrets[i].transform.rotation = Quaternion.Euler(0, 0, -90);
        }

         turretMain = GameObject.FindGameObjectsWithTag("turret Main");
        for (int i = 0; i < turretMain.Length; i++)
        {
            turretMain[i].GetComponent<TurretHealth>().health = turretMain[i].transform.GetChild(0).GetComponent<Turret>().turretHealth;
           //Debug.Log(turretMain[i].transform.GetChild(1));
            turretMain[i].GetComponent<BoxCollider2D>().enabled = true;
        }

        optionsPanel.SetActive(false);
        turretPanel.SetActive(false);
        player.GetComponent<BoxCollider2D>().enabled = true;
        boss.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;

    }
}
