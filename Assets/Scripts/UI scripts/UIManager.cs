using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //main UI panels
    GameObject optionPanel;
    GameObject turretPanel;
    GameObject bulletPanel;

    //turret setting panels
    GameObject targetPlayerUI;
    GameObject arcShotUI;
    GameObject spiralShotUI;
    GameObject singleDirectionUI;

    //change settings buttons
    Button toTurretSettings;
    Button toBulletSettings;

    //general turret movement settings
    Text[] fireRateInput;
    Text turretName;
    Dropdown aimType;
    Dropdown numOfStreams;
    Dropdown streamToEdit;
    Button saveTurretSettings;
    Text[] setTurretHealth;

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



    //bullet setting panels
    GameObject streamshotUI;
    GameObject shotgunUI;
    GameObject randomBurstUI;

    //general bullet settings
    Dropdown bulletStreamToEdit;
    Dropdown fireType;
    Dropdown moveType;
    Button saveBulletSettings;
    //Text[] bulletSpeed;

    //stream bullet settings
    Text[] streamNumberOfBul;
    Toggle bulletSpeedIncreaseCheck;
    Text[] bulletSpeedIncreaseAmmount;

    //shotgun bullet settings
    Text[] shotgunNumberOfBul;
    Text[] angleBetweenBul;
    Toggle straightShotgunShot;

    //random burst bullet settings
    Text[] randNumberOfBul;
    Text[] randRange;

    //turret info
    Turret turret;
    GameObject mainTurret;
    GameObject currentSelectedTurret;
    GameObject[] turretChildren = new GameObject[4];

    //save button
    Button saveBoss;
    Button TEMPLOAD;
    Button TestBoss;
    Button clearAll;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject boss;

    [SerializeField]
    private GameObject gameManager;


    GameObject[] turrets;

    // Start is called before the first frame update
    void Start()
    {
        //get main panels
        optionPanel = GameObject.Find("Options panel");
        turretPanel = GameObject.Find("turret options");
        bulletPanel = GameObject.Find("bullet options");

        //get turret settings panels
        targetPlayerUI = GameObject.Find("Target player UI");
        arcShotUI = GameObject.Find("Arc shot UI");
        spiralShotUI = GameObject.Find("Spiral shot UI");
        singleDirectionUI = GameObject.Find("Single direction UI");



        //get change settings buttons
        toTurretSettings = GameObject.Find("Turret settings").GetComponent<Button>();
        toBulletSettings = GameObject.Find("Bullet settings").GetComponent<Button>();

        saveBoss = GameObject.Find("Save Boss").GetComponent<Button>();
        TEMPLOAD = GameObject.Find("TEMP load").GetComponent<Button>();
        TestBoss = GameObject.Find("TEST").GetComponent<Button>();
        clearAll = GameObject.Find("clear boss").GetComponent<Button>();
        setupTurretPanelsInputs();
        setupBulletPanelsInputs();
        setupDropdowns();
        setupButtons();

        //disable all turret setting pannels
        targetPlayerUI.SetActive(false);
        arcShotUI.SetActive(false);
        spiralShotUI.SetActive(false);
        singleDirectionUI.SetActive(false);

        //disable all bullet setting pannels
        streamshotUI.SetActive(false);
        shotgunUI.SetActive(false);
        randomBurstUI.SetActive(false);

        //disable turret, bullet and the main option panel
        bulletPanel.SetActive(false);
        turretPanel.SetActive(false);
        optionPanel.SetActive(false);

    }

    void setupTurretPanelsInputs()
    {
        //get generic turret options
        fireRateInput = GameObject.Find("turret firerate input").GetComponentsInChildren<Text>();
        turretName = GameObject.Find("SelectedTurret").GetComponent<Text>();
        aimType = GameObject.Find("Turret aim types").GetComponent<Dropdown>();
        numOfStreams = GameObject.Find("number of streams input").GetComponent<Dropdown>();
        streamToEdit = GameObject.Find("stream to edit input").GetComponent<Dropdown>();
        saveTurretSettings = GameObject.Find("Save turret settings").GetComponent<Button>();
        setTurretHealth = GameObject.Find("turret health input").GetComponentsInChildren<Text>();

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

    }

    // Text[] randNumberOfBul;
    // Text[] randRange;

    void setupBulletPanelsInputs()
    {
        //get generic bullet settings
        bulletStreamToEdit = GameObject.Find("bullet stream num input").GetComponent<Dropdown>();
        fireType = GameObject.Find("fire type input").GetComponent<Dropdown>();
        moveType = GameObject.Find("movement type input").GetComponent<Dropdown>();
        saveBulletSettings = GameObject.Find("Save bullet settings").GetComponent<Button>();

        //get bullet configuration panels
        streamshotUI = GameObject.Find("stream shot UI");
        shotgunUI = GameObject.Find("shotgun UI");
        randomBurstUI = GameObject.Find("random burst UI");

        //get stream configuration inputs
        streamNumberOfBul = GameObject.Find("stream num of bullets input").GetComponentsInChildren<Text>();
        bulletSpeedIncreaseCheck = GameObject.Find("bullet speed increase").GetComponent<Toggle>();
        bulletSpeedIncreaseAmmount = GameObject.Find("speed increase ammount input").GetComponentsInChildren<Text>();

        //get shotgun configuration inputs
        shotgunNumberOfBul = GameObject.Find("shotgun num of bullets input").GetComponentsInChildren<Text>();
        angleBetweenBul = GameObject.Find("arc between bullets input").GetComponentsInChildren<Text>();
        straightShotgunShot = GameObject.Find("straight shotgun check").GetComponent<Toggle>();

        //get random burst configuration inputs
        randNumberOfBul = GameObject.Find("rand burst num of bullets input").GetComponentsInChildren<Text>();
        randRange = GameObject.Find("rand burst range input").GetComponentsInChildren<Text>();
    }

    void setupDropdowns()
    {
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

        bulletStreamToEdit.onValueChanged.AddListener(delegate
        {
            streamToEditChanged(bulletStreamToEdit);
        }
        );

        fireType.onValueChanged.AddListener(delegate
        {
            turret.bulletFormation = fireType.value + 1;
            bulletFireType(fireType);
        }
        );

        moveType.onValueChanged.AddListener(delegate
        {
            bulletMoveType(moveType);
        }
        );
    }

    void setupButtons()
    {
        //set listeners for the save button and swap settings buttons
        toTurretSettings.onClick.AddListener(delegate
        {
            toTurretSettingsPress();
        });

        toBulletSettings.onClick.AddListener(delegate
        {
            toBulletSettingsPress();
        });


        saveTurretSettings.onClick.AddListener(delegate
        {
            saveTurretPressed();
        });


        saveBulletSettings.onClick.AddListener(delegate
        {
            saveBulletPressed();
        });

        saveBoss.onClick.AddListener(delegate
        {
            GetComponent<SaveBoss>().saveBoss();
        });

        TEMPLOAD.onClick.AddListener(delegate
        {
            GetComponent<SaveBoss>().LoadBoss();
        });

        TestBoss.onClick.AddListener(delegate
        {
            fightBoss();
        });

        clearAll.onClick.AddListener(delegate
        {
            clearAllTurrets();
        });

    }

    void clearAllTurrets()
    {
        for (int i = 1; i < boss.transform.childCount; i++)
        {
            Destroy(boss.transform.GetChild(i).gameObject);
        }
    }

    void fightBoss()
    {
        turrets = GameObject.FindGameObjectsWithTag("Turret");
        for (int i = 0; i < turrets.Length; i++)
        {
            if (turrets[i].GetComponent<Turret>().streamEnabled)
            {
                fireOnOrOffOnTurret(turrets[i], true);
            }
            else
            {
                fireOnOrOffOnTurret(turrets[i], false);
            }
        }
        gameManager.GetComponent<OnGameStart>().PlayMode();
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

        for (int i = 0; i < 4; i++)
        {
            turretChildren[i].GetComponent<Turret_Fire>().readyToFire = true;
        }

        turretName.text = "Selected turret \n" + currentSelectedTurret.name;

        fireOnOrOffOnTurret(currentTurret, true);

        SetAllValues();

        optionPanel.SetActive(true);
        turretPanel.SetActive(true);
        bulletPanel.SetActive(false);

        SetActiveTurretUI();
    }

    void SetAllValues()
    {
        numOfStreams.value = turret.numberActiveStreams - 1;
        numOfStreamsChanged(numOfStreams);

        aimType.value = turret.targetingType - 1;

        spiralDirection.isOn = turret.spiralDirection;

        fireType.value = turret.bulletFormation - 1;
        bulletFireType(fireType);
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
        turretChildren[0].GetComponent<Turret>().numberActiveStreams = change.value + 1;
        for (int i = 1; i < change.value + 1; i++)
        {
            turretChildren[i].SetActive(true);
            turretChildren[i].GetComponent<Turret>().streamEnabled = true;
            turretChildren[i].GetComponent<Turret>().numberActiveStreams = change.value+1;
        }

        for (int i = change.value + 1; i < 4; i++)
        {
            turretChildren[i].SetActive(false);
            turretChildren[i].GetComponent<Turret>().streamEnabled = false;
            turretChildren[i].GetComponent<Turret>().numberActiveStreams = change.value+1;
        }


        //creates a list of strings and adds them to the stream to edit dropdown
        streamToEdit.ClearOptions();
        bulletStreamToEdit.ClearOptions();
        for (int i = 1; i < change.value + 2; i++)
        {
            streamToEditOptions.Add(i.ToString());
        }
        streamToEdit.AddOptions(streamToEditOptions);
        bulletStreamToEdit.AddOptions(streamToEditOptions);
        bulletFireType(fireType);

        //Debug.Log(turretChildren[0].GetComponent<Turret>().numberActiveStreams);
    }

    void streamToEditChanged(Dropdown change)
    {
        fireOnOrOffOnTurret(currentSelectedTurret, false);
        Debug.Log(change.value);
        turret = turretChildren[change.value].GetComponent<Turret>();
        currentSelectedTurret = turretChildren[change.value];
        turretName.text = "Selected turret \n" + currentSelectedTurret.name;
        aimType.value = turret.targetingType - 1;
        fireOnOrOffOnTurret(currentSelectedTurret, true);
        bulletFireType(fireType);
    }

    void SetActiveTurretUI()
    {
        switch (turret.targetingType)
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

    void bulletFireType(Dropdown change)
    {
        
        switch (turret.bulletFormation)
        {
            case 1:
                streamshotUI.SetActive(false);
                shotgunUI.SetActive(false);
                randomBurstUI.SetActive(false);

                break;

            case 2:
                streamshotUI.SetActive(true);
                shotgunUI.SetActive(false);
                randomBurstUI.SetActive(false);

                break;

            case 3:
                streamshotUI.SetActive(false);
                shotgunUI.SetActive(true);
                randomBurstUI.SetActive(false);

                break;

            case 4:
                streamshotUI.SetActive(false);
                shotgunUI.SetActive(false);
                randomBurstUI.SetActive(true);
                
                break;
            default:
                break;
        }
    }

    void bulletMoveType(Dropdown change)
    {

    }

    void DropdownValueChanged(Dropdown change)
    {
        turret.targetingType = change.value + 1;

        switch (turret.targetingType)
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

    void toTurretSettingsPress()
    {
        turretPanel.SetActive(true);
        bulletPanel.SetActive(false);
    }

    void toBulletSettingsPress()
    {
        turretPanel.SetActive(false);
        bulletPanel.SetActive(true);
    }

    //save turret button pressed
    void saveTurretPressed()
    {
        if (fireRateInput[1].text != "")
        {
            Debug.Log("firerate set");
            turret.firerate = float.Parse(fireRateInput[1].text);
        }

        if (setTurretHealth[1].text != "")
        {
            turret.turretHealth = int.Parse(setTurretHealth[1].text);
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

        if (spiralDirection.isOn)
        {
            Debug.Log("spin on ");
            turret.spiralDirection = true;
        }
        else
        {
            Debug.Log("spin off ");
            turret.spiralDirection = false;
        }

        if (spiralRotationSpeed[1].text != null)
        {
            turret.rotateSpeed = float.Parse(spiralRotationSpeed[1].text);
        }
    }

    void saveSingleDirSettings()
    {
        if (singleDirectionAim[1].text != null)
        {
            turret.singleDirDirection = float.Parse(singleDirectionAim[1].text);
        }
    }

    void saveBulletPressed()
    {
        switch (turret.bulletFormation)
        {
            case 1:
                break;

            case 2:
                saveStreamShotSettings();
                break;

            case 3:
                saveShotgunSettings();
                break;
            case 4:
                saveRandomBurstSettings();
                break;
            default:
                break;
        }
    }

    void saveStreamShotSettings()
    {
        if (bulletSpeedIncreaseCheck.isOn == true)
        {
            turret.bulletSpeedIncreaseCheck = true;
            if (bulletSpeedIncreaseAmmount[1].text != "")
            {
                turret.bulletSpeedIncreaseAmmount = float.Parse(bulletSpeedIncreaseAmmount[1].text);
            }
        }
        else
        {
            turret.bulletSpeedIncreaseCheck = false;
        }

        if (streamNumberOfBul[1].text != "")
        {
            turret.numOfBullets = int.Parse(streamNumberOfBul[1].text);
        }
    }
    void saveShotgunSettings()
    {
        if (straightShotgunShot.isOn == true)
        {
            turret.shotgunStraight = true;
        }
        else
        {
            turret.shotgunStraight = false;
        }

        if (shotgunNumberOfBul[1].text != "")
        {
            turret.numOfBullets = int.Parse(shotgunNumberOfBul[1].text);
        }

        if (angleBetweenBul[1].text != "")
        {
            turret.angleBetweenBullets = float.Parse(angleBetweenBul[1].text);
        }
    }

    void saveRandomBurstSettings()
    {
        if (randNumberOfBul[1].text != "")
        {
            turret.numOfBullets = int.Parse(randNumberOfBul[1].text);
        }
        if (randRange[1].text != "")
        {
            turret.bulletRandomRange = float.Parse(randRange[1].text);
        }
    }
}
