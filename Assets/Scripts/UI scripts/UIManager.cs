using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameObject optionPanel;
    GameObject targetPlayerUI;
    GameObject arcShotUI;
    GameObject spiralShotUI;
    GameObject singleDirectionUI;

    Text[] fireRateInput;
    Text turretName;
    Dropdown aimType;
    Dropdown numOfStreams;
    Dropdown streamToEdit;
    Button saveTurretSettings;

    // target player variables
    Toggle smoothTargetToggle;
    Text[] smoothTargetSpeed;
    Text[] targetingOffset;

    //Arc shot variables
    Text[] arcSize;
    Text[] arcDirection;
    Text[] rotationSpeed;

    //spiral shot variables
    Text[] spiralRotationSpeed;
    Toggle spiralDirection;

    //single direction variables
    Text[] singleDirectionAim;

    //turret info
    Turret turret;
    GameObject mainTurret;
    GameObject currentSelectedTurret;
    GameObject[] turretChildren = new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
        //get main panel and individual shot panels
        optionPanel = GameObject.Find("turret options");
        targetPlayerUI = GameObject.Find("Target player UI");
        arcShotUI = GameObject.Find("Arc shot UI");
        spiralShotUI = GameObject.Find("Spiral shot UI");
        singleDirectionUI = GameObject.Find("Single direction UI");

        //get generic turret options
        fireRateInput = GameObject.Find("turret firerate input").GetComponentsInChildren<Text>();
        turretName = GameObject.Find("SelectedTurret").GetComponent<Text>();
        aimType = GameObject.Find("Turret aim types").GetComponent<Dropdown>();
        numOfStreams = GameObject.Find("number of streams input").GetComponent<Dropdown>();
        streamToEdit = GameObject.Find("stream to edit input").GetComponent<Dropdown>();
        saveTurretSettings = GameObject.Find("Save turret settings").GetComponent<Button>();

        //get targeted turret options
        smoothTargetToggle = GameObject.Find("Smooth targeting Toggle").GetComponent<Toggle>();
        smoothTargetSpeed = GameObject.Find("Smooth targeting speed input").GetComponentsInChildren<Text>();
        targetingOffset = GameObject.Find("Target offset input").GetComponentsInChildren<Text>();

        //get arc shot options
        arcSize = GameObject.Find("Arc Size input").GetComponentsInChildren<Text>();
        arcDirection = GameObject.Find("Arc Direction input").GetComponentsInChildren<Text>();
        rotationSpeed = GameObject.Find("Rotation Speed input").GetComponentsInChildren<Text>();

        //get spiral shot options
        spiralRotationSpeed = GameObject.Find("Spiral Rotation Speed input").GetComponentsInChildren<Text>();
        spiralDirection = GameObject.Find("Spiral spin direction toggle").GetComponent<Toggle>();

        //get single direction options
        singleDirectionAim = GameObject.Find("Single direciton input").GetComponentsInChildren<Text>();


        //set listeners for the dropdown boxes
        aimType.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(aimType);
        }
        );

        numOfStreams.onValueChanged.AddListener(delegate
        {
            numOfStreamsChanged(numOfStreams);
        }
        );

        streamToEdit.onValueChanged.AddListener(delegate
            {
                streamToEditChanged(streamToEdit);
            }
        );

        saveTurretSettings.onClick.AddListener(delegate
        {
            saveTurretPressed();
        });

        targetPlayerUI.SetActive(false);
        arcShotUI.SetActive(false);
        spiralShotUI.SetActive(false);
        singleDirectionUI.SetActive(false);
        optionPanel.SetActive(false);
        
    }

    public void turretSelected(GameObject currentTurret) 
    {
        currentSelectedTurret = currentTurret;
        turret = currentTurret.GetComponent<Turret>();
        mainTurret = currentTurret.transform.parent.gameObject;

        turretChildren[0] = mainTurret.transform.GetChild(0).gameObject;
        turretChildren[1] = mainTurret.transform.GetChild(1).gameObject;
        turretChildren[2] = mainTurret.transform.GetChild(2).gameObject;
        turretChildren[3] = mainTurret.transform.GetChild(3).gameObject;

        turretName.text = "Selected turret \n" + currentSelectedTurret.name;

        fireOnOrOffOnTurret(currentTurret, true);
        optionPanel.SetActive(true);
        aimType.value = turret.targetingType - 1;
        SetActiveUI();
    }
    void fireOnOrOffOnTurret(GameObject turr, bool b)
    {
        turr.GetComponent<Turret_Fire>().enabled = b;
        turr.GetComponent<Turret_Targeting>().enabled = b;
        turr.GetComponent<Turret_BulletSetup>().enabled = b;
    }
    void numOfStreamsChanged(Dropdown change)
    {
        List<string> streamToEditOptions = new List<string> { };
        //loops through children activating them up to the number selected
        for (int i = 1; i < change.value +1; i++)
        {
            turretChildren[i].SetActive(true);
            turretChildren[i].GetComponent<Turret>().streamEnabled = true;
        }

        for (int i = change.value+1; i < 4; i++)
        {
            turretChildren[i].SetActive(false);
            turretChildren[i].GetComponent<Turret>().streamEnabled = false;
        }


        //creates a list of strings and adds them to the stream to edit dropdown
        streamToEdit.ClearOptions();
        for (int i = 1; i < change.value+2; i++)
        {
            streamToEditOptions.Add(i.ToString());
        }
        streamToEdit.AddOptions(streamToEditOptions);
    }

    void streamToEditChanged(Dropdown change) 
    {
        fireOnOrOffOnTurret(currentSelectedTurret, false);
        Debug.Log(change.value);
        turret = turretChildren[change.value].GetComponent<Turret>();
        currentSelectedTurret = turretChildren[change.value];
        turretName.text = "Selected turret \n" + currentSelectedTurret.name;
        fireOnOrOffOnTurret(currentSelectedTurret, true);
    }

    void SetActiveUI()
    {
        switch(turret.targetingType)
        {
            case 1:

                targetPlayerUI.SetActive(true);
                arcShotUI.SetActive(false);
                spiralShotUI.SetActive(false);
                singleDirectionUI.SetActive(false);
                break;
            case 2:

                targetPlayerUI.SetActive(false);
                arcShotUI.SetActive(true);
                spiralShotUI.SetActive(false);
                singleDirectionUI.SetActive(false);
                break;
            case 3:
                targetPlayerUI.SetActive(false);
                arcShotUI.SetActive(false);
                spiralShotUI.SetActive(true);
                singleDirectionUI.SetActive(false);
                break;
            case 4:
                targetPlayerUI.SetActive(false);
                arcShotUI.SetActive(false);
                spiralShotUI.SetActive(false);
                singleDirectionUI.SetActive(true);
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
                targetPlayerUI.SetActive(false);
                arcShotUI.SetActive(true);
                spiralShotUI.SetActive(false);
                singleDirectionUI.SetActive(false);
                break;
            case 2:
                targetPlayerUI.SetActive(false);
                arcShotUI.SetActive(true);
                spiralShotUI.SetActive(true);
                singleDirectionUI.SetActive(false);
                break;
            case 3:
                targetPlayerUI.SetActive(false);
                arcShotUI.SetActive(false);
                spiralShotUI.SetActive(true);
                singleDirectionUI.SetActive(false);
                break;
            case 4:
                targetPlayerUI.SetActive(false);
                arcShotUI.SetActive(false);
                spiralShotUI.SetActive(false);
                singleDirectionUI.SetActive(true);
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

            case 3:
                saveSpiralShotSettings();
                break;
            case 4:
                saveSingleDirSettings();
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

    void saveSpiralShotSettings()
    {
        if (spiralRotationSpeed[1].text != null)
        {
            turret.rotateSpeed = float.Parse(spiralRotationSpeed[1].text);
        }

        if (spiralDirection.isOn)
        {
            turret.spiralDirection = true;
        }
        else
        {
            turret.spiralDirection = false;
        }   
    }

    void saveSingleDirSettings()
    {
        if (singleDirectionAim[1].text != null)
        {
            turret.singleDirDirection = float.Parse(singleDirectionAim[1].text);
        }
    }
}
