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

    private void Start()
    {
        uiManager = GameObject.Find("UI Manager");
    }

    private void OnMouseDown()
    {
        uiManager.GetComponent<UIManager>().turretSelected(transform.gameObject);
    }   
}
