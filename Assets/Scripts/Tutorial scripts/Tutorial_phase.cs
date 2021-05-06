using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_phase : MonoBehaviour
{
    // Start is called before the first frame update

    //public GameObject[] popUpLocation;
    private int popUpIndex = -1;
    public GameObject tutorialTextBox;
    Text tutText;

    public GameObject pressKeyText;
    public float waitTime = 2.0f;
    public  float currentWaitTime;
    public SceneControl sceneControl;


    GameObject turretPanel;
    public GameObject wavePanel;


    GameObject wavePanelButton;
    GameObject turretPanelButton;
    GameObject bulletPanelButton;

    GameObject turretOptionsPanel;
    GameObject bulletOptionsPanel;



    GameObject toTurretSettings;
    GameObject toBulletSettings;

    GameObject turretHelpButton;
    GameObject bulletHelpButton;
    GameObject streamHelpButton;
    GameObject shotgunHelpButton;
    GameObject randomHelpButton;
    GameObject targetPlayerHelpButton;
    GameObject arcShotHelpButton;
    GameObject spiralShotHelpButton;
    GameObject singleDirectionHelpButton;
    GameObject sineMovementHelpButton;
    GameObject variableSpeedHelpButton;
    GameObject travelThenTargetHelpButtons;


    Dropdown subPhaseDropdown;
    Dropdown subPhaseToEdit;

    Dropdown PhaseDropdown;
    Dropdown PhaseToEdit;

    Button SaveTurret;


    GameObject saveBoss;
    GameObject TEMPLOAD;
    GameObject TestBoss;
    GameObject clearAll;


    Turret currentTurret;
    void Start()
    {
        currentWaitTime = -1.0f;
        tutText = tutorialTextBox.GetComponentInChildren<Text>();


        pressKeyText.SetActive(false);
        StartCoroutine(setAll());
        StartCoroutine(setStartStatus());
  
    }


    // Update is called once per frame
    void Update()
    {
        currentWaitTime += Time.deltaTime;

        if (currentWaitTime > waitTime)
        {
            pressKeyText.SetActive(true);
        }
        else
        {
            pressKeyText.SetActive(false);
        }

         switch (popUpIndex)
         {
         case -1:
             tutText.text = " Welcome to Danmaku maker, the bullet hell boss creator!";
             increaseIndex();
             break;
         
             case 0:
                 tutText.text = "This tutorial will go over the phase panel.";
                 if (Input.anyKeyDown)
                 {
                     increaseIndex();                     
                 }
                 break;
         
             case 1:
                 tutText.text = "To begin, please drop a turret onto the boss.";

                turretPanel.SetActive(true);

                if (GameObject.FindGameObjectWithTag("Turret") != null)
                {
                    increaseIndex();
                }
                break;
         
             case 2:
                turretPanel.SetActive(false);
                tutText.text = "Now, select the 'phase' button on the right panel.";
                wavePanelButton.SetActive(true);
                if (wavePanel.activeInHierarchy)
                {
                    increaseIndex();
                }
                break;
         
             case 3:
                
                tutText.text = "The phase panel allows you to really get funky with the boss. Using what you know so far, you would only be able to create a" +
                    " boss with a single phase. After a while, it would get pretty dull, dodging the same single pattern over and over. This is what the phase panel is for.";
                toBulletSettings.SetActive(true);
                toTurretSettings.SetActive(true);
      
                if (!wavePanel.activeInHierarchy)
                    wavePanel.SetActive(true);


                if (Input.anyKeyDown)
                {
                    increaseIndex();
                }
                break;
         
             case 4:
                tutText.text = "First, some explanations as to what phase and subphases are.";

                if (!wavePanel.activeInHierarchy)
                    wavePanel.SetActive(true);

                if (Input.anyKeyDown)
                {
                    increaseIndex();
                }
                break;
         
             case 5:
                tutText.text = "Phases will change at specific health points of the boss. For example, if you have 2 Phases, the second phase " +
                    "will start once the boss reaches 50% health, 66% and 33% for 3 phases etc. This allows you to make the boss get progressively more difficult";

                if (!wavePanel.activeInHierarchy)
                    wavePanel.SetActive(true);

                if (Input.anyKeyDown)
                {
                    increaseIndex();
                }

                break;
         
             case 6:
                tutText.text = "Subphases allow you to make the boss fire different patterns before it reaches set hp values. The boss will loop through a maximum of " +
                    "4 different subphases that you create per phase.";
                if (Input.anyKeyDown)
                {
                    increaseIndex();
                }

                break;
         
             case 7:
                tutText.text = "IMPORTANT: you must make sure you save each turret on each subphase that you have. Otherwise, the turret will not fire in the subphase." +
                    "\n(Future release will have save all feature (if there is one) )";

                if (Input.anyKeyDown)
                {
                    increaseIndex();
                }
                break;
         
             case 8:

                tutText.text = "This concludes the phase tutorial";
                if (Input.anyKeyDown)
                {
                    increaseIndex("fin");
                }
                break;


            default:
                break;

        }   
    }


    void increaseIndex()
    {
        if (currentWaitTime > waitTime)
        {
            popUpIndex++;
            currentWaitTime = 0;
        }
    }

    void increaseIndex(string isEnd)
    {
        if (currentWaitTime > waitTime)
        {
            if (isEnd == "fin")
            {
                StartCoroutine(sceneControl.ExitScene("main menu"));
            }
            popUpIndex++;
            currentWaitTime = 0;
        }
    }

    IEnumerator setStartStatus()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return null;
        }
        turretPanel.SetActive(false);

        wavePanelButton.SetActive(false);
        turretPanelButton.SetActive(false);
        bulletPanelButton.SetActive(false);


        toTurretSettings.SetActive(false);
        toBulletSettings.SetActive(false);

        wavePanel.SetActive(false);

        turretHelpButton.SetActive(false);
        bulletHelpButton.SetActive(false);
        streamHelpButton.SetActive(false);
        shotgunHelpButton.SetActive(false);
        randomHelpButton.SetActive(false);
        targetPlayerHelpButton.SetActive(false);
        arcShotHelpButton.SetActive(false);
        spiralShotHelpButton.SetActive(false);
        singleDirectionHelpButton.SetActive(false);
        sineMovementHelpButton.SetActive(false);
        variableSpeedHelpButton.SetActive(false);
        travelThenTargetHelpButtons.SetActive(false);

        saveBoss.SetActive(false);
        TEMPLOAD.SetActive(false);
        TestBoss.SetActive(false);
        clearAll.SetActive(false);
    }

    IEnumerator setAll()
    {
        
        turretPanel = GameObject.Find("turret panel");
        wavePanel.SetActive(true);

        wavePanelButton = GameObject.Find("to Wave Settings");
        turretPanelButton = GameObject.Find("to Turret select");
        bulletPanelButton = GameObject.Find("to Bullet select");

        turretOptionsPanel = GameObject.Find("turret options");
        bulletOptionsPanel = GameObject.Find("bullet options");



        toTurretSettings = GameObject.Find("Turret settings");
        toBulletSettings = GameObject.Find("Bullet settings");

        turretHelpButton = GameObject.Find("turret options info button");
        bulletHelpButton = GameObject.Find("bullet options info button");
        streamHelpButton = GameObject.Find("stream shot info button");
        shotgunHelpButton = GameObject.Find("stream shot info button");
        randomHelpButton = GameObject.Find("random burst info button");
        targetPlayerHelpButton = GameObject.Find("target player info button");
        arcShotHelpButton = GameObject.Find("arc shot info button");
        spiralShotHelpButton = GameObject.Find("spiral shot info button");
        singleDirectionHelpButton = GameObject.Find("single direction info button");
        sineMovementHelpButton = GameObject.Find("sine movement info button");
        variableSpeedHelpButton = GameObject.Find("variable speed info button");
        travelThenTargetHelpButtons = GameObject.Find("travel then target info button");

        subPhaseDropdown = GameObject.Find("number Of sub-waves Input").GetComponent<Dropdown>();
        subPhaseToEdit = GameObject.Find("sub-wave to edit Input").GetComponent<Dropdown>();
        PhaseDropdown = GameObject.Find("number Of Waves Input").GetComponent<Dropdown>();
        PhaseToEdit = GameObject.Find("wave to edit Input").GetComponent<Dropdown>();

        SaveTurret = GameObject.Find("Save turret settings").GetComponent<Button>();

        saveBoss = GameObject.Find("Save Boss");
        TEMPLOAD = GameObject.Find("TEMP load");
        TestBoss = GameObject.Find("TEST");
        clearAll = GameObject.Find("clear boss");
        yield return null;
    }
}
