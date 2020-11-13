using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameObject optionPanel;
    GameObject targetPlayerUI;
    GameObject arcShotUI;
    GameObject[] UIPanels = new GameObject[3];

    Text[] fireRateInput;
    Text turretName;
    Dropdown aimType;
    Button saveTurretSettings;

    // target player variables
    Toggle smoothTargetToggle;
    Text[] smoothTargetSpeed;
    Text[] targetingOffset;

    //Arc shot variables
    Text[] arcSize;
    Text[] arcDirection;
    Text[] rotationSpeed;

    Turret turret;
    // Start is called before the first frame update
    void Start()
    {
        //get main panel and individual shot panels
        optionPanel = GameObject.Find("turret options");
        targetPlayerUI = GameObject.Find("Target player UI");
        arcShotUI = GameObject.Find("Arc shot UI");

        //get generic turret options
        fireRateInput = GameObject.Find("turret firerate input").GetComponentsInChildren<Text>();
        turretName = GameObject.Find("SelectedTurret").GetComponent<Text>();
        aimType = GameObject.Find("Turret aim types").GetComponent<Dropdown>();
        saveTurretSettings = GameObject.Find("Save settings").GetComponent<Button>();

        //get targeted turret options
        smoothTargetToggle = GameObject.Find("Smooth targeting Toggle").GetComponent<Toggle>();
        smoothTargetSpeed = GameObject.Find("Smooth targeting speed input").GetComponentsInChildren<Text>();
        targetingOffset = GameObject.Find("Target offset input").GetComponentsInChildren<Text>();

        //get arc shot options
        arcSize = GameObject.Find("Arc Size input").GetComponentsInChildren<Text>();
        arcDirection = GameObject.Find("Arc Direction input").GetComponentsInChildren<Text>();
        rotationSpeed = GameObject.Find("Rotation Speed input").GetComponentsInChildren<Text>();

        
        //aimType.value = GetComponent<Turret>().targetingType - 1;

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
        arcShotUI.SetActive(false);
        optionPanel.SetActive(false);
    }

    public void turretSelected(GameObject currentTurret) 
    {
        turret = currentTurret.GetComponent<Turret>();
        turretName.text = "Selected turret " + currentTurret.name;

        currentTurret.GetComponent<Turret_Fire>().enabled = true;
        currentTurret.GetComponent<Turret_Targeting>().enabled = true;
        currentTurret. GetComponent<Turret_BulletSetup>().enabled = true;
        optionPanel.SetActive(true);
        aimType.value = turret.targetingType - 1;
        SetActiveUI();
    }




    void SetActiveUI()
    {
        switch(turret.targetingType)
        {
            case 1:
                arcShotUI.SetActive(false);
                targetPlayerUI.SetActive(true);
                break;
            case 2:
                targetPlayerUI.SetActive(false);
                arcShotUI.SetActive(true);
                break;

            default:
                break;
        }
    }

    //save button pressed
    void saveTurretPressed()
    {
        if (fireRateInput[1].text != "")
        {
            Debug.Log("firerate set");
            turret.firerate = float.Parse(fireRateInput[1].text);
        }

        switch (turret.targetingType)
        {
            case 1:
                saveTargetPlayerSettings();
                break;
            case 2:
                saveArcShotSettings();
                break;

            default:
                break;
        }
    }
    void DropdownValueChanged(Dropdown change)
    {
        turret.targetingType = change.value + 1;

        switch (turret.targetingType)
        {
            case 1:
                arcShotUI.SetActive(false);
                targetPlayerUI.SetActive(true);
                break;
            case 2:
                targetPlayerUI.SetActive(false);
                arcShotUI.SetActive(true);
                break;

            default:
                break;
        }
    }

    void saveTargetPlayerSettings()
    {

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
    void saveArcShotSettings()
    {
        if (arcSize[1].text != "")
        {
            turret.rotateAngleWidth = float.Parse(arcSize[1].text);
        }
        if (arcDirection[1].text != "")
        {
            turret.rotateAngleDirection = float.Parse(arcDirection[1].text) + 90;
        }
        if (rotationSpeed[1].text != "")
        {
            turret.rotateSpeed = float.Parse(rotationSpeed[1].text);
        }

    }
}
