using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class turretSelect : MonoBehaviour
{
    Turret turret;
    GameObject optionPanel;
    GameObject targetPlayerUI;

    public Text[] fireRateInput;
    Text turretName;
    Dropdown aimType;
    Button saveTurretSettings;

    Toggle smoothTargetToggle;
    public Text[] smoothTargetSpeed;
    Text[] targetingOffset;

    // Start is called before the first frame update
    void Start()
    {
        turret = GetComponent<Turret>();
        //get main panel
        optionPanel = GameObject.Find("turret options");
        targetPlayerUI = GameObject.Find("Target player UI");

        //get generic turret options
        fireRateInput = GameObject.Find("turret firerate input").GetComponentsInChildren<Text>();
        turretName = GameObject.Find("SelectedTurret").GetComponent<Text>();
        aimType = GameObject.Find("Turret aim types").GetComponent<Dropdown>();
        saveTurretSettings = GameObject.Find("Save settings").GetComponent<Button>();

        //get targeted turret options
        smoothTargetToggle = GameObject.Find("Smooth targeting Toggle").GetComponent<Toggle>();
        smoothTargetSpeed = GameObject.Find("Smooth targeting speed input").GetComponentsInChildren<Text>();
        targetingOffset = GameObject.Find("Target offset input").GetComponentsInChildren<Text>();

        turretName.text = "Selected turret " + this.name;
        aimType.value = GetComponent<Turret>().targetingType - 1;

        aimType.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(aimType);
        }
        );

        saveTurretSettings.onClick.AddListener(delegate
        {
            saveTurretPressed();
        });

        targetPlayerUI.SetActive(false);
        optionPanel.SetActive(false);    
      }

    private void OnMouseDown()
    {
        GetComponent<Turret_Fire>().enabled = true;
        GetComponent<Turret_Targeting>().enabled = true;
        GetComponent<Turret_BulletSetup>().enabled = true;
        optionPanel.SetActive(true);

        switch (turret.targetingType)
        {
            case 1:
                targetPlayerUI.SetActive(true);
                break;
            case 2:
                break;

            default:
                break;
        }
    }

    //save button pressed
    void saveTurretPressed()
    {
        switch (turret.targetingType)
        {
            case 1:
                saveTargetPlayerSettings();
                break;
            case 2:
                break;

            default:
                break;
        }
    }
    void DropdownValueChanged(Dropdown change)
    {
        turret.targetingType = change.value + 1;
    }

    void saveTargetPlayerSettings()
    {
        if (fireRateInput[1].text != "")
        {
            Debug.Log("firerate set");
            turret.firerate = float.Parse(fireRateInput[1].text);     
        }

        if (smoothTargetToggle.isOn == true)
        {
            turret.smoothTarget = true;      
            if (smoothTargetSpeed[1].text != "")
            {
                turret.smoothTargetSpeed = float.Parse(smoothTargetSpeed[1].text);
                Debug.Log("slerp set");
            }
        }
        else
        {
            turret.smoothTarget = false;
        }

        if (targetingOffset[1].text != "")
        {
            Debug.Log("offset set");
            turret.targetPlayerOffsetAmmount = float.Parse(targetingOffset[1].text);
        }
    }
}
