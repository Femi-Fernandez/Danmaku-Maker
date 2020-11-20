using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class turretSelect : MonoBehaviour
{
    public GameObject uiManager;
    public GameObject[] turrets;
    private void Start()
    {
        uiManager = GameObject.Find("UI Manager");
    }

    private void OnMouseDown()
    {
        turrets = GameObject.FindGameObjectsWithTag("Turret");
        for (int i = 0; i < turrets.Length; i++)
        {
            turrets[i].GetComponent<Turret_Fire>().enabled = false;
            turrets[i].GetComponent<Turret_Targeting>().enabled = false;
            turrets[i].GetComponent<Turret_BulletSetup>().enabled = false;
            turrets[i].transform.rotation = Quaternion.Euler(0, 0, -90);
        }

       uiManager.GetComponent<UIManager>().turretSelected(transform.GetChild(0).gameObject);       
    }   
}
