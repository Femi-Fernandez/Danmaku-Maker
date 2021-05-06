using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Control + M + H to create colapse code
    //control + M + U to remove colapsed code

    //Variables

    //main UI panels
    GameObject optionPanel;
    GameObject turretPanel;
    GameObject bulletPanel;
    GameObject infoPanel;

    //help buttons
    Button turretHelpButton;
    Button bulletHelpButton;
    Button streamHelpButton;
    Button shotgunHelpButton;
    Button randomHelpButton;
    Button targetPlayerHelpButton;
    Button arcShotHelpButton;
    Button spiralShotHelpButton;
    Button singleDirectionHelpButton;
    Button sineMovementHelpButton;
    Button variableSpeedHelpButton;
    Button travelThenTargetHelpButtons;

    UIInfoPanel uiInfoPanel;
    Button closeInfoPanel;

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

    Toggle activeWave;

    Toggle isDestructable;
    //Toggle activeWave2;
    //Toggle activeWave3;
    //Toggle activeWave4;

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
    Text[] bulletBaseSpeed;

    //bullet movement panels
    GameObject sineMovementUI;
    GameObject varableSpeedUI;
    GameObject travelThenTargetUI;

    //sin movement variables
    Text[] amplitude;
    Text[] frequency;

    //variable speed movement variables
    Text[] maxSpeed;
    Text[] minSpeed;
    Text[] speedChangeFrequency;

    //travel then target variables
    Text[] timeUntilChange;
    Dropdown newTargetingType;
    Text[] speedAfterTarget;

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

    GameObject inputOOR;
    //turret info
    public Turret turret;
    turretSubwaveStorage subwaveStorage;

    TurretMinMaxVal turValCheck;
    GameObject mainTurret;
    public GameObject currentSelectedTurret;
    GameObject[] turretChildren = new GameObject[4];

    //save button
    Button saveBoss;
    Button TEMPLOAD;
    public Button TestBoss;
    Button clearAll;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private GameObject gameManager;

    GameObject[] turrets;

    GameObject[] coreTiles;
    GameObject[] tiles;

    public int waveNum;
    public int subwaveNum;
    public int arraySlot;

    public int bulletType;

    public AnalyticsCommands AC;
    public EdgeCollider2D edgeCol;


    float fadeOut;
    float duration;
    float durationLength = 3;
    float fadeOutVal;
    Vector3 badValStartpos;
    Vector3 badValEndpos;
    Color zm;
    // Start is called before the first frame update
    void Start()
    {
        //get main panels
        optionPanel = GameObject.Find("Options panel");
        turretPanel = GameObject.Find("turret options");
        bulletPanel = GameObject.Find("bullet options");
        infoPanel = GameObject.Find("Info Panel");
        uiInfoPanel = GetComponent<UIInfoPanel>();


        //get turret settings panels
        targetPlayerUI = GameObject.Find("Target player UI");
        arcShotUI = GameObject.Find("Arc shot UI");
        spiralShotUI = GameObject.Find("Spiral shot UI");
        singleDirectionUI = GameObject.Find("Single direction UI");

        closeInfoPanel = GameObject.Find("close info panel").GetComponent<Button>();

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
        setupHelpButtons();

        inputOOR = GameObject.Find("badValInput");
        badValStartpos = inputOOR.transform.position;
        badValEndpos = new Vector3(inputOOR.transform.position.x, inputOOR.transform.position.y +20, inputOOR.transform.position.z) ;
        zm = inputOOR.GetComponent<Text>().color;


        coreTiles = GameObject.FindGameObjectsWithTag("coreTiles");
        tiles = GameObject.FindGameObjectsWithTag("tiles");
        turValCheck = GetComponent<TurretMinMaxVal>();

        inputOOR.SetActive(false);

        activeWave.isOn = true;
        isDestructable.isOn = true;


        TestBoss.interactable = false;

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
        //infoPanel.SetActive(false);
    }
    //finds all the inputs and toggles for the Turret UI panels. 
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

        activeWave = GameObject.Find("Active in wave toggle").GetComponent<Toggle>();
        // activeWave2 = GameObject.Find("wave 2 toggle").GetComponent<Toggle>();
        // activeWave3 = GameObject.Find("wave 3 toggle").GetComponent<Toggle>();
        // activeWave4 = GameObject.Find("wave 4 toggle").GetComponent<Toggle>();
        isDestructable= GameObject.Find("Is destructable toggle").GetComponent<Toggle>();

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

    int GetArraySlot()
    {
        return arraySlot = (waveNum * 4) + subwaveNum;
    }
    void setupBulletPanelsInputs()
    {
        //get generic bullet settings
        bulletStreamToEdit = GameObject.Find("bullet stream num input").GetComponent<Dropdown>();
        fireType = GameObject.Find("fire type input").GetComponent<Dropdown>();
        moveType = GameObject.Find("movement type input").GetComponent<Dropdown>();
        saveBulletSettings = GameObject.Find("Save bullet settings").GetComponent<Button>();
        bulletBaseSpeed = GameObject.Find("bullet speed input").GetComponentsInChildren<Text>();

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



        //individual bullet movement configuration panels 
        sineMovementUI = GameObject.Find("sine movement UI");
        varableSpeedUI = GameObject.Find("variable speed movement UI");
        travelThenTargetUI = GameObject.Find("travel then change movement UI");

        //sine movement inputs
        amplitude = GameObject.Find("Amplitude input").GetComponentsInChildren<Text>();
        frequency = GameObject.Find("Frequency input").GetComponentsInChildren<Text>();

        //variable speed change inputs
        maxSpeed = GameObject.Find("Fastest speed input").GetComponentsInChildren<Text>();
        minSpeed = GameObject.Find("Slowest speed input").GetComponentsInChildren<Text>();
        speedChangeFrequency = GameObject.Find("Speed change frequency input").GetComponentsInChildren<Text>();

        //travel then target inputs
        timeUntilChange = GameObject.Find("time until change input").GetComponentsInChildren<Text>();
        newTargetingType = GameObject.Find("new target type input").GetComponent<Dropdown>();
        speedAfterTarget = GameObject.Find("Speed after target input").GetComponentsInChildren<Text>();
    }

    //finds all the inputs and toggles for the Bullet UI panels. 
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
            turret.bulletMovementType = moveType.value;
            bulletMoveType(moveType);
        }
        );

        newTargetingType.onValueChanged.AddListener(delegate
        {
            turret.bulletNewTargetingType = newTargetingType.value;
        }
        );

    }

    //sets listeners for all the buttons on the UI
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

        closeInfoPanel.onClick.AddListener(delegate
        {
            infoPanel.SetActive(false);
        });

    }

    void setupHelpButtons()
    {
        turretHelpButton = GameObject.Find("turret options info button").GetComponent<Button>();
        bulletHelpButton = GameObject.Find("bullet options info button").GetComponent<Button>();
        streamHelpButton = GameObject.Find("stream shot info button").GetComponent<Button>();
        shotgunHelpButton = GameObject.Find("stream shot info button").GetComponent<Button>();
        randomHelpButton = GameObject.Find("random burst info button").GetComponent<Button>();
        targetPlayerHelpButton = GameObject.Find("target player info button").GetComponent<Button>();
        arcShotHelpButton = GameObject.Find("arc shot info button").GetComponent<Button>();
        spiralShotHelpButton = GameObject.Find("spiral shot info button").GetComponent<Button>();
        singleDirectionHelpButton = GameObject.Find("single direction info button").GetComponent<Button>();
        sineMovementHelpButton = GameObject.Find("sine movement info button").GetComponent<Button>();
        variableSpeedHelpButton = GameObject.Find("variable speed info button").GetComponent<Button>();
        travelThenTargetHelpButtons = GameObject.Find("travel then target info button").GetComponent<Button>();
       


        turretHelpButton.onClick.AddListener(delegate
        {
            infoPanel.SetActive(true);
            uiInfoPanel.displayInfo("turret");
        });

        bulletHelpButton.onClick.AddListener(delegate
        {
            infoPanel.SetActive(true);
            uiInfoPanel.displayInfo("bullet");
        });

        streamHelpButton.onClick.AddListener(delegate
        {
            infoPanel.SetActive(true);
            uiInfoPanel.displayInfo("stream");
        });

        shotgunHelpButton.onClick.AddListener(delegate
        {
            infoPanel.SetActive(true);
            uiInfoPanel.displayInfo("shotgun");
        });

        randomHelpButton.onClick.AddListener(delegate
        {
            infoPanel.SetActive(true);
            uiInfoPanel.displayInfo("random");
        });

        targetPlayerHelpButton.onClick.AddListener(delegate
        {
            infoPanel.SetActive(true);
            uiInfoPanel.displayInfo("target player");
        });

        arcShotHelpButton.onClick.AddListener(delegate
        {
            infoPanel.SetActive(true);
            uiInfoPanel.displayInfo("arc shot");
        });

        spiralShotHelpButton.onClick.AddListener(delegate
        {
            infoPanel.SetActive(true);
            uiInfoPanel.displayInfo("spiral shot");
        });

        singleDirectionHelpButton.onClick.AddListener(delegate
        {
            infoPanel.SetActive(true);
            uiInfoPanel.displayInfo("single direction");
        });

        sineMovementHelpButton.onClick.AddListener(delegate
        {
            infoPanel.SetActive(true);
            uiInfoPanel.displayInfo("sine movement");
        });

        variableSpeedHelpButton.onClick.AddListener(delegate
        {
            infoPanel.SetActive(true);
            uiInfoPanel.displayInfo("variable speed");
        });

        travelThenTargetHelpButtons.onClick.AddListener(delegate
        {
            infoPanel.SetActive(true);
            uiInfoPanel.displayInfo("travel then target");
        });


    }

    //deletes all the currently placed turrets and resets tilemap. 
    void clearAllTurrets()
    {
        for (int i = 1; i < boss.transform.childCount; i++)
        {
            Destroy(boss.transform.GetChild(i).gameObject);
        }

        StartCoroutine(clearTiles());

        bulletPanel.SetActive(false);
        turretPanel.SetActive(false);
        optionPanel.SetActive(false);

    }
    //wait a frame to clear all tiles
    //weird bug occurs if not where tiles with turrets would not dissapear
     IEnumerator clearTiles()
    {
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].GetComponent<bossGridCheck>().removeTile();
        }
        List<Vector2> linePoints = new List<Vector2>();
        linePoints.Add(new Vector2(0, 0));
        linePoints.Add(new Vector2(0, 0));
        edgeCol.points = linePoints.ToArray();
    }

    //checks for what turrets should be enabled, and enables their firing scripts. also deactivates the option panel. 
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
        //optionPanel.SetActive(false);
    }

    /// <summary>
    /// this is the method that is called when a turret is selected to be edited.
    /// it sets the current turret variable, the Turret script and the children of the turret to that of the selected turret.
    /// then it enables the option panel and turret panels and disables the bullet panel and sets the current turret as the current active turret. 
    /// </summary>
    /// <param name="currentTurret"></param>
    public void turretSelected(GameObject currentTurret)
    {

        if (currentSelectedTurret == currentTurret)
        {
            turretChildren[0] = mainTurret.transform.GetChild(0).gameObject;
            turretChildren[1] = mainTurret.transform.GetChild(1).gameObject;
            turretChildren[2] = mainTurret.transform.GetChild(2).gameObject;
            turretChildren[3] = mainTurret.transform.GetChild(3).gameObject;

            for (int i = 0; i < 4; i++)
            {
                //turretChildren[i].GetComponent<Turret_Fire>().readyToFire = true;
            }

            turretName.text = "Selected turret \n" + currentSelectedTurret.name;

            fireOnOrOffOnTurret(currentTurret, true);

            setupTurret();//NEEDS TESTING
            optionPanel.SetActive(true);
            turretPanel.SetActive(true);
            bulletPanel.SetActive(false);


            SetActiveTurretUI();
            setInputFields();

        }
        else
        {
            currentSelectedTurret = currentTurret;
            turret = currentTurret.GetComponent<Turret>();
            mainTurret = currentTurret.transform.parent.gameObject;
            subwaveStorage = mainTurret.GetComponent<turretSubwaveStorage>();

            //Debug.Log(mainTurret.name);

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

            setupTurret();//NEEDS TESTING

            optionPanel.SetActive(true);
            turretPanel.SetActive(true);
            bulletPanel.SetActive(false);


            SetActiveTurretUI();
            setInputFields();
        }
    }


    //sets up the turret to fire while in the creation mode so the player can see the stream they are editing
    void setupTurret()
    {


        for (int i = 0; i < turretChildren.Length; i++)
        {
            fireOnOrOffOnTurret(turretChildren[i], false);
        }
        turret = turretChildren[0].GetComponent<Turret>();
        currentSelectedTurret = turretChildren[0];
        aimType.value = turret.targetingType - 1;
        fireOnOrOffOnTurret(currentSelectedTurret, true);

        numOfStreams.value = turret.numberActiveStreams - 1;
        numOfStreamsChanged(numOfStreams);


        aimType.value = turret.targetingType - 1;

        fireType.value = turret.bulletFormation - 1;
        bulletFireType(fireType);
        bulletMoveType(moveType);
    }


    //enables and disables the firing scripts on the inputted turret. 
    void fireOnOrOffOnTurret(GameObject turr, bool b)
    {
        turr.GetComponent<Turret_Fire>().enabled = b;
        turr.GetComponent<Turret_Targeting>().enabled = b;
        turr.GetComponent<Turret_BulletSetup>().enabled = b;
    }

    //when the number of streams change, this script enables and disables the inputted number of streams,
    //and updates the streamToEdit and bulletStreamToEdit dropdowns. 
    void numOfStreamsChanged(Dropdown change)
    {
        List<string> streamToEditOptions = new List<string> { };
        //loops through children activating them up to the number selected
        turretChildren[0].GetComponent<Turret>().numberActiveStreams = change.value + 1;
        for (int i = 1; i < change.value + 1; i++)
        {
            turretChildren[i].SetActive(true);
            turretChildren[i].GetComponent<Turret>().streamEnabled = true;
            turretChildren[i].GetComponent<Turret>().numberActiveStreams = change.value + 1;

            subwaveStorage.streamEnabled[GetArraySlot(), i] = turretChildren[i].GetComponent<Turret>().streamEnabled;
            subwaveStorage.numberActiveStreams[GetArraySlot()] = turretChildren[i].GetComponent<Turret>().numberActiveStreams;
        }

        for (int i = change.value + 1; i < 4; i++)
        {
            turretChildren[i].SetActive(false);
            turretChildren[i].GetComponent<Turret>().streamEnabled = false;
            turretChildren[i].GetComponent<Turret>().numberActiveStreams = change.value + 1;

            subwaveStorage.streamEnabled[GetArraySlot(), i] = turretChildren[i].GetComponent<Turret>().streamEnabled;
            subwaveStorage.numberActiveStreams[GetArraySlot()] = turretChildren[i].GetComponent<Turret>().numberActiveStreams;
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

    //disables the currently firing turret firing scripts, updates the currently selected turret and enables  its firing scripts. 
    void streamToEditChanged(Dropdown change)
    {
        fireOnOrOffOnTurret(currentSelectedTurret, false);
        //Debug.Log(change.value);
        turret = turretChildren[change.value].GetComponent<Turret>();
        currentSelectedTurret = turretChildren[change.value];
        currentSelectedTurret.GetComponent<Turret_Fire>().fireTimer = 0;
        currentSelectedTurret.GetComponent<Turret_Fire>().readyToFire = true;
        turretName.text = "Selected turret \n" + currentSelectedTurret.name;
        aimType.value = turret.targetingType - 1;
        fireOnOrOffOnTurret(currentSelectedTurret, true);

        bulletStreamToEdit.value = change.value;
        streamToEdit.value = change.value;

        bulletFireType(fireType);

    }

    //changes what turret UI is displayed based on the targetingType Dropdown
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

    //changes what bullet firing UI is displayed based on the bulletFormation dropdown
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

    //TBI: changes what bullet movement UI is displayed based on the bulletMovement dropdown
    void bulletMoveType(Dropdown change)
    {
        turret.bulletMovementType = change.value;

        switch (turret.bulletMovementType)
        {
            case 0:
                sineMovementUI.SetActive(false);
                varableSpeedUI.SetActive(false);
                travelThenTargetUI.SetActive(false);
                break;
            case 1:
                sineMovementUI.SetActive(true);
                varableSpeedUI.SetActive(false);
                travelThenTargetUI.SetActive(false);
                break;
            case 2:
                sineMovementUI.SetActive(false);
                varableSpeedUI.SetActive(true);
                travelThenTargetUI.SetActive(false);
                break;
            case 3:
                sineMovementUI.SetActive(false);
                varableSpeedUI.SetActive(false);
                travelThenTargetUI.SetActive(true);
                break;
            default:
                break;
        }
    }

 
    //changes what turret UI is displayed based on the targetingType Dropdown
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

    //enables the turretPanel and disables the bulletpanel
    void toTurretSettingsPress()
    {
        turretPanel.SetActive(true);
        bulletPanel.SetActive(false);
    }

    //enables the bulletPanel and disables the turretpanel
    void toBulletSettingsPress()
    {
        turretPanel.SetActive(false);
        bulletPanel.SetActive(true);
    }

    bool checkTurretVal(float userNum, float[] minMaxVal) 
    {
        return userNum >= minMaxVal[0] && userNum <= minMaxVal[1];
    }

    bool checkTurretVal(int userNum, int[] minMaxVal)
    {
        return userNum >= minMaxVal[0] && userNum <= minMaxVal[1];
    }

    void badValue(float userNum, float[] minMaxVal, string ErrorValName)
    {
        inputOOR.SetActive(true);

        zm.a = 1.0f;
        inputOOR.GetComponent<Text>().color = zm;
        //Debug.Log("here???");
        duration = 0;
        if (userNum >= minMaxVal[0])
        {
            inputOOR.GetComponent<Text>().text = ErrorValName + " value to Big! Use value between " + minMaxVal[0] + " and " + minMaxVal[1];
        }

        if (userNum <= minMaxVal[1])
        {
            inputOOR.GetComponent<Text>().text = ErrorValName + " value to Small! Use value between " + minMaxVal[0] + " and " + minMaxVal[1];
        }

        StartCoroutine(badValFade());
    }
    void badValue(int userNum, int[] minMaxVal, string ErrorValName)
    {
        inputOOR.SetActive(true);

        zm.a = 1.0f;
        inputOOR.GetComponent<Text>().color = zm;
        //Debug.Log("here???");
        duration = 0;
        if (userNum >= minMaxVal[0])
        {
            inputOOR.GetComponent<Text>().text = ErrorValName + " value to Big! Use value between " + minMaxVal[0] + " and " + minMaxVal[1];
        }

        if (userNum <= minMaxVal[1])
        {
            inputOOR.GetComponent<Text>().text = ErrorValName + " value to Small! Use value between " + minMaxVal[0] + " and " + minMaxVal[1];
        }

        StartCoroutine(badValFade());
    }


    IEnumerator badValFade()
    {

        while (duration <= durationLength - .5f)
        {
            inputOOR.transform.position = Vector3.Lerp(badValStartpos, badValEndpos, duration/durationLength);
            duration += Time.deltaTime;
            Debug.Log(duration);
            yield return null;
        }


        inputOOR.GetComponent<Text>().color = zm;
        fadeOutVal = 0;
        while ( duration <= durationLength)
        {
            inputOOR.transform.position = Vector3.Lerp(badValStartpos, badValEndpos, duration / durationLength);
            fadeOutVal += Time.deltaTime;
            zm.a = Mathf.Lerp(1, 0, fadeOutVal /.5f);

            inputOOR.GetComponent<Text>().color = zm;
            duration += Time.deltaTime;
            yield return null;
        }
        inputOOR.SetActive(false);
    }
    //saves turret settings based on what targeting type is selected. 
    void saveTurretPressed()
    {
        saveTurretBaseSettings();
       // Debug.Log("Wavenum: " + waveNum + ", subWaveNum: " + subwaveNum + ", arraySlot: " + GetArraySlot());
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
       
        setSubwaveStorage();
        TestBoss.interactable = true;
        //AC.saveTurretPressed();
    }

    void saveTurretBaseSettings()
    {
        if (fireRateInput[1].text != "")
        {
            if (checkTurretVal(float.Parse(fireRateInput[1].text), turValCheck.firerate))
            {
                turret.firerate = float.Parse(fireRateInput[1].text);
                //Debug.Log("Value correct!");
            }
            else
            {
                badValue(float.Parse(fireRateInput[1].text), turValCheck.firerate, "Fire rate");
            }
        }

        if (setTurretHealth[1].text != "")
        {
            if (checkTurretVal(int.Parse(setTurretHealth[1].text), turValCheck.turretHealth))
            {
                turret.turretHealth = int.Parse(setTurretHealth[1].text);
            }
            else
            {
                badValue(int.Parse(setTurretHealth[1].text), turValCheck.turretHealth,  "Turret Health");
            }

        }

        subwaveStorage.activeInWave[waveNum] = activeWave.isOn;
        //Debug.Log(waveNum);
        subwaveStorage.isDestroyable[waveNum] = isDestructable.isOn;
    }
    //void saveSineM

    void saveTargetPlayerSettings()
    {

        if (smoothTargetToggle.isOn == true)
        {
            turret.smoothTarget = true;

            if (smoothTargetSpeed[1].text != "")
            {
                if (checkTurretVal(float.Parse(smoothTargetSpeed[1].text), turValCheck.smoothTargetSpeed))
                {
                    turret.smoothTargetSpeed = float.Parse(smoothTargetSpeed[1].text);
                }
                else
                {
                    badValue(float.Parse(smoothTargetSpeed[1].text), turValCheck.smoothTargetSpeed, "Smooth Target Speed");
                }

            }
        }
        else
        {
            turret.smoothTarget = false;
        }

        if (targetingOffset[1].text != "")
        {
            //Debug.Log("offset set");               
            if (checkTurretVal(float.Parse(targetingOffset[1].text), turValCheck.targetPlayerOffsetAmmount))
            {
                 turret.targetPlayerOffsetAmmount = float.Parse(targetingOffset[1].text);
            }
            else
            {
                badValue(float.Parse(targetingOffset[1].text), turValCheck.targetPlayerOffsetAmmount, "Targeting offset");
            }
        }
    }

    void saveArcShotSettings()
    {
        if (arcSize[1].text != "")
        {
            
            if (checkTurretVal(float.Parse(arcSize[1].text), turValCheck.rotateAngleWidth))
            {
                turret.rotateAngleWidth = float.Parse(arcSize[1].text);
            }
            else
            {
                badValue(float.Parse(arcSize[1].text), turValCheck.rotateAngleWidth, "Arc size");
            }

        }
        if (arcDirection[1].text != "")
        {
           
            if (checkTurretVal(float.Parse(arcDirection[1].text), turValCheck.rotateAngleDirection))
            {
                turret.rotateAngleDirection = float.Parse(arcDirection[1].text) + 90;
            }
            else
            {
                badValue(float.Parse(arcDirection[1].text), turValCheck.rotateAngleDirection, "Angle direction");
            }
        }
        if (rotationSpeed[1].text != "")
        {
            
            if (checkTurretVal(float.Parse(rotationSpeed[1].text), turValCheck.rotateSpeed))
            {
                turret.rotateSpeed = float.Parse(rotationSpeed[1].text);
            }
            else
            {
                badValue(float.Parse(rotationSpeed[1].text), turValCheck.rotateSpeed, "Rotation speed");
            }
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

        if (spiralRotationSpeed[1].text != "")
        {
           
            if (checkTurretVal(float.Parse(spiralRotationSpeed[1].text), turValCheck.rotateSpeed))
            {
                turret.rotateSpeed = float.Parse(spiralRotationSpeed[1].text);
            }
            else
            {
                badValue(float.Parse(spiralRotationSpeed[1].text), turValCheck.rotateSpeed, "Rotation speed");
            }
        }
    }

    void saveSingleDirSettings()
    {
        if (singleDirectionAim[1].text != "")
        {
           
            if (checkTurretVal(float.Parse(singleDirectionAim[1].text), turValCheck.singleDirDirection))
            {
                turret.singleDirDirection = float.Parse(singleDirectionAim[1].text);
            }
            else
            {
                badValue(float.Parse(singleDirectionAim[1].text), turValCheck.singleDirDirection, "Direction");
            }
        }
    }

    //saves bullet settings based on what targeting type is selected.
    void saveBulletPressed()
    {

        saveBulletBase();

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

        switch (turret.bulletMovementType)
        {
            case 0:
                break;
            case 1:
                saveSinMoveSettings();
                break;
            case 2:
                saveVariablSpeedSettings();
                break;
            case 3:
                saveTravelThenTargetSettings();
                Debug.Log("travel target saved?");
                break;
            default:
                break;
        }

        setSubwaveStorage();
        TestBoss.interactable = true;
        AC.saveBulletPressed();
    }

    //save bullet firing style methods
    void saveBulletBase()
    {
        if (bulletBaseSpeed[1].text != "")
        {
            //turret.bulletBaseSpeed = 3;
            if (checkTurretVal(float.Parse(bulletBaseSpeed[1].text), turValCheck.bulletBaseSpeed))
            {
                turret.bulletBaseSpeed = float.Parse(bulletBaseSpeed[1].text);
            }
            else
            {
                badValue(float.Parse(bulletBaseSpeed[1].text), turValCheck.bulletBaseSpeed, "Bullet speed");
            }
        }
        else
        {
            turret.bulletBaseSpeed = 3;
        }
    }

    void saveStreamShotSettings()
    {
        if (bulletSpeedIncreaseCheck.isOn == true)
        {
            turret.bulletSpeedIncreaseCheck = true;

            if (bulletSpeedIncreaseAmmount[1].text != "")
            {
               
                if (checkTurretVal(float.Parse(bulletSpeedIncreaseAmmount[1].text), turValCheck.bulletSpeedIncreaseAmmount))
                {
                    turret.bulletSpeedIncreaseAmmount = float.Parse(bulletSpeedIncreaseAmmount[1].text);
                }
                else
                {
                    badValue(float.Parse(bulletSpeedIncreaseAmmount[1].text), turValCheck.bulletSpeedIncreaseAmmount, "Bullet speed increase amount");
                }
            }
        }
        else
        {
            turret.bulletSpeedIncreaseCheck = false;

        }

        if (streamNumberOfBul[1].text != "")
        {
           
            if (checkTurretVal(int.Parse(streamNumberOfBul[1].text), turValCheck.numOfBullets))
            {
                turret.numOfBullets = int.Parse(streamNumberOfBul[1].text);
            }
            else
            {
                badValue(int.Parse(streamNumberOfBul[1].text), turValCheck.numOfBullets, "Number of bullets");
            }
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
           
            if (checkTurretVal(int.Parse(shotgunNumberOfBul[1].text), turValCheck.numOfBullets))
            {
                turret.numOfBullets = int.Parse(shotgunNumberOfBul[1].text);
            }
            else
            {
                badValue(int.Parse(shotgunNumberOfBul[1].text), turValCheck.numOfBullets, "Number of bullets");
            }


        }

        if (angleBetweenBul[1].text != "")
        {
            
            if (checkTurretVal(float.Parse(angleBetweenBul[1].text), turValCheck.angleBetweenBullets))
            {
                turret.angleBetweenBullets = float.Parse(angleBetweenBul[1].text);
            }
            else
            {
                badValue(float.Parse(angleBetweenBul[1].text), turValCheck.angleBetweenBullets, "Angle between bullets");
            }
        }
    }

    void saveRandomBurstSettings()
    {
        if (randNumberOfBul[1].text != "")
        {        
            if (checkTurretVal(int.Parse(randNumberOfBul[1].text), turValCheck.numOfBullets))
            {
                turret.numOfBullets = int.Parse(randNumberOfBul[1].text);
            }
            else
            {
                badValue(int.Parse(randNumberOfBul[1].text), turValCheck.numOfBullets, "Number of bullets");
            }
        }

        if (randRange[1].text != "")
        {        
            if (checkTurretVal(float.Parse(randRange[1].text), turValCheck.bulletRandomRange))
            {
                turret.bulletRandomRange = float.Parse(randRange[1].text);
            }
            else
            {
                badValue(float.Parse(randRange[1].text), turValCheck.bulletRandomRange, "Random bullet range");
            }
        }
    }

    //save bullet movement style methods
    void saveSinMoveSettings()
    {
        if (amplitude[1].text != "")
        {
            if (checkTurretVal(float.Parse(amplitude[1].text), turValCheck.bulletAmplitude))
            {
                turret.bulletAmplitude = float.Parse(amplitude[1].text);
            }
            else
            {
                badValue(float.Parse(amplitude[1].text), turValCheck.bulletAmplitude, "Wave amplitude");
            }
        }

        if (frequency[1].text != "")
        {
            if (checkTurretVal(float.Parse(frequency[1].text), turValCheck.bulletFrequency))
            {
                turret.bulletFrequency = float.Parse(frequency[1].text);
            }
            else
            {
                badValue(float.Parse(frequency[1].text), turValCheck.bulletFrequency, "Wave frequency");
            }

        }
    }

    void saveVariablSpeedSettings()
    {
        if (maxSpeed[1].text != "")
        {

            if (checkTurretVal(float.Parse(maxSpeed[1].text), turValCheck.bulletMaxSpeed))
            {
                turret.bulletMaxSpeed = float.Parse(maxSpeed[1].text);
            }
            else
            {
                badValue(float.Parse(maxSpeed[1].text), turValCheck.bulletMaxSpeed, "Varying bullet speed max");
            }
        }
        if (minSpeed[1].text != "")
        {

            if (checkTurretVal(float.Parse(minSpeed[1].text), turValCheck.bulletMinSpeed))
            {
                turret.bulletMinSpeed = float.Parse(minSpeed[1].text);
            }
            else
            {
                badValue(float.Parse(minSpeed[1].text), turValCheck.bulletMinSpeed, "Varying bullet speed min");
            }
        }
        if (speedChangeFrequency[1].text != "")
        {
           
            if (checkTurretVal(float.Parse(speedChangeFrequency[1].text), turValCheck.bulletSpeedChangeFrequency))
            {
                turret.bulletSpeedChangeFrequency = float.Parse(speedChangeFrequency[1].text);
            }
            else
            {
                badValue(float.Parse(speedChangeFrequency[1].text), turValCheck.bulletSpeedChangeFrequency, "Varying bullet speed change frequency");
            }
        }
    }

    void saveTravelThenTargetSettings()
    {
        if (timeUntilChange[1].text != "")
        {
            if (checkTurretVal(float.Parse(timeUntilChange[1].text), turValCheck.bulletTimeUntilChange))
            {
                turret.bulletTimeUntilChange = float.Parse(timeUntilChange[1].text);
            }
            else
            {
                badValue(float.Parse(timeUntilChange[1].text), turValCheck.bulletTimeUntilChange, "Time until target change");
            }
        }

        turret.bulletNewTargetingType = newTargetingType.value;

        if (speedAfterTarget[1].text != "")
        {
            if (checkTurretVal(float.Parse(speedAfterTarget[1].text), turValCheck.bulletSpeedAfterTarget))
            {
                turret.bulletSpeedAfterTarget = float.Parse(speedAfterTarget[1].text);
            }
            else
            {
                badValue(float.Parse(speedAfterTarget[1].text), turValCheck.bulletSpeedAfterTarget, "Speed after target change");
            }
        }

    }

    void setSubwaveStorage()
    {
        //Debug.Log("streamToEdit value: " + streamToEdit.value);
        subwaveStorage.bulletFormation[GetArraySlot(), streamToEdit.value] = turret.bulletFormation;
        subwaveStorage.streamEnabled[GetArraySlot(), streamToEdit.value] = turret.streamEnabled;
        subwaveStorage.turretLocation = turret.turretLocation;

        subwaveStorage.targetingType[GetArraySlot(), streamToEdit.value] = turret.targetingType;
        subwaveStorage.firerate[GetArraySlot(), streamToEdit.value] = turret.firerate;
        subwaveStorage.turretHealth[waveNum] = turret.turretHealth;
        //subwave count, numactivestreams
        //subwaveStorage.SubwaveCount[wavenum] = 

        ////subwaveStorage.activeInWave[waveNum] = true;
        //subwaveStorage.activeInWave[0] = activeWave1.isOn;
        //subwaveStorage.activeInWave[1] = activeWave2.isOn;
        //subwaveStorage.activeInWave[2] = activeWave3.isOn;
        //subwaveStorage.activeInWave[3] = activeWave4.isOn;
        //

        // subwaveStorage.streamEnabled[GetArraySlot(), streamToEdit.value] = turret.streamEnabled;
        subwaveStorage.smoothTarget[GetArraySlot(), streamToEdit.value] = turret.smoothTarget;
        subwaveStorage.smoothTargetSpeed[GetArraySlot(), streamToEdit.value] = turret.smoothTargetSpeed;
        subwaveStorage.smoothTarget[GetArraySlot(), streamToEdit.value] = turret.smoothTarget;
        subwaveStorage.targetPlayerOffsetAmmount[GetArraySlot(), streamToEdit.value] = turret.targetPlayerOffsetAmmount;
        subwaveStorage.rotateAngleWidth[GetArraySlot(), streamToEdit.value] = turret.rotateAngleWidth;
        subwaveStorage.rotateAngleDirection[GetArraySlot(), streamToEdit.value] = turret.rotateAngleDirection;
        subwaveStorage.rotateSpeed[GetArraySlot(), streamToEdit.value] = turret.rotateSpeed;
        //CHECK
        subwaveStorage.spiralDirection[GetArraySlot(), streamToEdit.value] = turret.spiralDirection;
        subwaveStorage.rotateSpeed[GetArraySlot(), streamToEdit.value] = turret.rotateSpeed;
        subwaveStorage.singleDirDirection[GetArraySlot(), streamToEdit.value] = turret.singleDirDirection;
        subwaveStorage.bulletBaseSpeed[GetArraySlot(), streamToEdit.value] = turret.bulletBaseSpeed;
        subwaveStorage.bulletSpeedIncreaseCheck[GetArraySlot(), streamToEdit.value] = turret.bulletSpeedIncreaseCheck;
        subwaveStorage.bulletSpeedIncreaseAmmount[GetArraySlot(), streamToEdit.value] = turret.bulletSpeedIncreaseAmmount;
        subwaveStorage.bulletSpeedIncreaseCheck[GetArraySlot(), streamToEdit.value] = turret.bulletSpeedIncreaseCheck;
        subwaveStorage.numOfBullets[GetArraySlot(), streamToEdit.value] = turret.numOfBullets;
        subwaveStorage.shotgunStraight[GetArraySlot(), streamToEdit.value] = turret.shotgunStraight;
        subwaveStorage.shotgunStraight[GetArraySlot(), streamToEdit.value] = turret.shotgunStraight;
        subwaveStorage.numOfBullets[GetArraySlot(), streamToEdit.value] = turret.numOfBullets;
        subwaveStorage.angleBetweenBullets[GetArraySlot(), streamToEdit.value] = turret.angleBetweenBullets;
        subwaveStorage.numOfBullets[GetArraySlot(), streamToEdit.value] = turret.numOfBullets;
        subwaveStorage.bulletRandomRange[GetArraySlot(), streamToEdit.value] = turret.bulletRandomRange;


        subwaveStorage.bulletMovementType[GetArraySlot(), streamToEdit.value] = turret.bulletMovementType;
        subwaveStorage.bulletAmplitude[GetArraySlot(), streamToEdit.value] = turret.bulletAmplitude;
        subwaveStorage.bulletFrequency[GetArraySlot(), streamToEdit.value] = turret.bulletFrequency;

        subwaveStorage.bulletMaxSpeed[GetArraySlot(), streamToEdit.value] = turret.bulletMaxSpeed;
        subwaveStorage.bulletMinSpeed[GetArraySlot(), streamToEdit.value] = turret.bulletMinSpeed;
        subwaveStorage.bulletSpeedChangeFrequency[GetArraySlot(), streamToEdit.value] = turret.bulletSpeedChangeFrequency;

        subwaveStorage.timeUntilChange[GetArraySlot(), streamToEdit.value] = turret.bulletTimeUntilChange;
        subwaveStorage.newTargetingType[GetArraySlot(), streamToEdit.value] = turret.bulletNewTargetingType;
        subwaveStorage.speedAfterTarget[GetArraySlot(), streamToEdit.value] = turret.bulletSpeedAfterTarget;

        subwaveStorage.bulletType[GetArraySlot(), streamToEdit.value] = turret.bulletType;

        subwaveStorage.turretSavedOnce[GetArraySlot(), streamToEdit.value] = true;
    }

    public void clearInputFields()
    {
        //clear all turret iputs
        //Debug.Log(turretPanel.activeSelf);
        optionPanel.SetActive(true);
        if (turretPanel.activeSelf)
        {
            fireRateInput[1].GetComponentInParent<InputField>().text = "";
            setTurretHealth[1].GetComponentInParent<InputField>().text = "";

            if (turret.targetingType == 1)
            {
                smoothTargetSpeed[1].GetComponentInParent<InputField>().text = "";
                targetingOffset[1].GetComponentInParent<InputField>().text = "";
            }
            if (turret.targetingType == 2)
            {
                arcSize[1].GetComponentInParent<InputField>().text = "";
                arcDirection[1].GetComponentInParent<InputField>().text = "";
                rotationSpeed[1].GetComponentInParent<InputField>().text = "";
            }
            if (turret.targetingType == 3)
            {

                spiralRotationSpeed[1].GetComponentInParent<InputField>().text = "";

            }
            if (turret.targetingType == 4)
            {

                singleDirectionAim[1].GetComponentInParent<InputField>().text = "";
            }
        }

        //clear all bullet inputs
        if (bulletPanel.activeSelf)
        {
            if (turret.bulletFormation == 2)
            {
                //stream configuration inputs
                streamNumberOfBul[1].GetComponentInParent<InputField>().text = "";
                //  bulletSpeedIncreaseCheck toggle
                bulletSpeedIncreaseAmmount[1].GetComponentInParent<InputField>().text = "";
            }

            if (turret.bulletFormation == 3)
            {
                //shotgun configuration inputs
                shotgunNumberOfBul[1].GetComponentInParent<InputField>().text = "";
                angleBetweenBul[1].GetComponentInParent<InputField>().text = "";
                //  straightShotgunShot toggle
            }

            if (turret.bulletFormation == 4)
            {
                //random burst configuration inputs
                randNumberOfBul[1].GetComponentInParent<InputField>().text = "";
                randRange[1].GetComponentInParent<InputField>().text = "";
            }


        }

        setInputFields();
    }

    void setInputFields()
    {
        //for (int i = 0; i < 4; i++)
        //{
        bool TPanCheck = false;
        bool BPancheck = false;
        

        if (!turretPanel.activeInHierarchy)
        {
            TPanCheck = true;
            turretPanel.SetActive(true);
        }

        //general turret options
        fireRateInput[0].text = subwaveStorage.firerate[GetArraySlot(), streamToEdit.value].ToString();
            aimType.value = subwaveStorage.targetingType[GetArraySlot(), streamToEdit.value] - 1;
            numOfStreams.value = subwaveStorage.numberActiveStreams[GetArraySlot()] - 1;
            setTurretHealth[0].text = subwaveStorage.turretHealth[waveNum].ToString();

            //target player options
            smoothTargetToggle.isOn = subwaveStorage.smoothTarget[GetArraySlot(), streamToEdit.value];
            smoothTargetSpeed[0].text = subwaveStorage.smoothTargetSpeed[GetArraySlot(), streamToEdit.value].ToString();
            targetingOffset[0].text = subwaveStorage.targetPlayerOffsetAmmount[GetArraySlot(), streamToEdit.value].ToString();

            //arc shot options
            arcSize[0].text = subwaveStorage.rotateAngleWidth[GetArraySlot(), streamToEdit.value].ToString();
            arcDirection[0].text = subwaveStorage.rotateAngleDirection[GetArraySlot(), streamToEdit.value].ToString();
            rotationSpeed[0].text = subwaveStorage.rotateSpeed[GetArraySlot(), streamToEdit.value].ToString();

            //spiral shot options
            spiralRotationSpeed[0].text = subwaveStorage.rotateSpeed[GetArraySlot(), streamToEdit.value].ToString();
            //CHECK

            spiralDirection.isOn = subwaveStorage.spiralDirection[GetArraySlot(), streamToEdit.value];

        if (!bulletPanel.activeInHierarchy)
        {
            BPancheck = true;
            bulletPanel.SetActive(true);
        }

            

            //single direction options
            singleDirectionAim[0].text = subwaveStorage.singleDirDirection[GetArraySlot(), streamToEdit.value].ToString();

            fireType.value = subwaveStorage.fireType[GetArraySlot(), streamToEdit.value];

            shotgunNumberOfBul[0].text = subwaveStorage.numOfBullets[GetArraySlot(), streamToEdit.value].ToString();

            amplitude[0].text = subwaveStorage.bulletAmplitude[GetArraySlot(), streamToEdit.value].ToString();
            frequency[0].text = subwaveStorage.bulletFrequency[GetArraySlot(), streamToEdit.value].ToString();

            maxSpeed[0].text = subwaveStorage.bulletMaxSpeed[GetArraySlot(), streamToEdit.value].ToString();
            minSpeed[0].text = subwaveStorage.bulletMinSpeed[GetArraySlot(), streamToEdit.value].ToString();
            speedChangeFrequency[0].text = subwaveStorage.bulletSpeedChangeFrequency[GetArraySlot(), streamToEdit.value].ToString();

            activeWave.isOn = subwaveStorage.activeInWave[waveNum];
            isDestructable.isOn = subwaveStorage.isDestroyable[waveNum];

        if (BPancheck)
        {
            bulletPanel.SetActive(false);
        }

        if (TPanCheck)
        {
            turretPanel.SetActive(false);
        }
        //}
        SetAllValues();
    }

    public void SetAllValues()
    {
        turret.firerate = 1;
        //for (int i = 0; i < 4; i++)
        //{
            if (subwaveStorage.turretSavedOnce[GetArraySlot(), streamToEdit.value])
            {
                turret.bulletFormation = subwaveStorage.bulletFormation[GetArraySlot(), streamToEdit.value];
                turretChildren[0].GetComponent<Turret>().streamEnabled = subwaveStorage.streamEnabled[GetArraySlot(), streamToEdit.value];

                turret.targetingType = subwaveStorage.targetingType[GetArraySlot(), streamToEdit.value];
                turret.firerate = subwaveStorage.firerate[GetArraySlot(), streamToEdit.value];
                turret.turretHealth = subwaveStorage.turretHealth[waveNum];
                turretChildren[0].GetComponent<Turret>().streamEnabled = subwaveStorage.streamEnabled[GetArraySlot(), streamToEdit.value];
                turret.smoothTarget = subwaveStorage.smoothTarget[GetArraySlot(), streamToEdit.value];
                turret.smoothTargetSpeed = subwaveStorage.smoothTargetSpeed[GetArraySlot(), streamToEdit.value];
                turret.smoothTarget = subwaveStorage.smoothTarget[GetArraySlot(), streamToEdit.value];
                turret.targetPlayerOffsetAmmount = subwaveStorage.targetPlayerOffsetAmmount[GetArraySlot(), streamToEdit.value];
                turret.rotateAngleWidth = subwaveStorage.rotateAngleWidth[GetArraySlot(), streamToEdit.value];
                turret.rotateAngleDirection = subwaveStorage.rotateAngleDirection[GetArraySlot(), streamToEdit.value];
                turret.rotateSpeed = subwaveStorage.rotateSpeed[GetArraySlot(), streamToEdit.value];
                turretChildren[streamToEdit.value].GetComponent<Turret>().spiralDirection = subwaveStorage.spiralDirection[GetArraySlot(), streamToEdit.value];
                turret.rotateSpeed = subwaveStorage.rotateSpeed[GetArraySlot(), streamToEdit.value];
                turret.singleDirDirection = subwaveStorage.singleDirDirection[GetArraySlot(), streamToEdit.value];

                turret.bulletFormation = subwaveStorage.bulletFormation[GetArraySlot(), streamToEdit.value];
                
                turret.bulletAmplitude = subwaveStorage.bulletAmplitude[GetArraySlot(), streamToEdit.value];
                turret.bulletFrequency = subwaveStorage.bulletFrequency[GetArraySlot(), streamToEdit.value];

                turret.bulletMaxSpeed = subwaveStorage.bulletMaxSpeed[GetArraySlot(), streamToEdit.value];
                turret.bulletMinSpeed = subwaveStorage.bulletMinSpeed[GetArraySlot(), streamToEdit.value];
                turret.bulletSpeedChangeFrequency = subwaveStorage.bulletSpeedChangeFrequency[GetArraySlot(), streamToEdit.value] ;

                turret.bulletType = subwaveStorage.bulletType[GetArraySlot(), streamToEdit.value];
            }
        //}
        setupTurret();
    }

    public void deselect()
    {
        optionPanel.gameObject.SetActive(false);
        bulletPanel.gameObject.SetActive(false);

        gameManager.GetComponent<OnGameStart>().TestMode();


        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        for (int i = 0; i < turrets.Length; i++)
        {
            turrets[i].GetComponent<Turret_Fire>().enabled = false;
            turrets[i].GetComponent<Turret_Targeting>().enabled = false;
            turrets[i].GetComponent<Turret_BulletSetup>().enabled = false;
            turrets[i].transform.rotation = Quaternion.Euler(0, 0, -90);
        }

    }

    public void toggleWaveSelect(int numberOfWaves)
    {
       // Toggle[] waveToggles = new Toggle[4];
       //
       // waveToggles[0] = activeWave1;
       // waveToggles[1] = activeWave2;
       // waveToggles[2] = activeWave3;
       // waveToggles[3] = activeWave4;
       //
       // for (int i = 0; i < numberOfWaves+1; i++)
       // {
       //     waveToggles[i].gameObject.SetActive(true);
       // }
       // for (int i = numberOfWaves+1; i < 4; i++)
       // {
       //     waveToggles[i].gameObject.SetActive(false);
       // }
    }

    public void saveDurationSettings(float durartion)
    {

        subwaveStorage.subwaveDuration[GetArraySlot()] = durartion;

    }

}
