using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_turret : MonoBehaviour
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

    GameObject wavePanelButton;
    GameObject turretPanelButton;
    GameObject bulletPanelButton;

    GameObject turretOptionsPanel;

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


    Dropdown aimType;

    GameObject saveBoss;
    GameObject TEMPLOAD;
    GameObject TestBoss;
    GameObject clearAll;
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
                 tutText.text = "This tutorial will go over the very basics of boss creation.";
                 if (Input.anyKeyDown)
                 {
                     increaseIndex();                     
                 }
                 break;
         
             case 1:
                 tutText.text = "Firstly, when creating a boss, you must place turrets onto the boss. These turrets are where the bullets are fired from.";
                 if (Input.anyKeyDown)
                 {
                     increaseIndex();     
                 }
                 break;
         
             case 2:
                 tutText.text = "Click and drag a turret from the right side pannel onto the boss. " +
                     "Anywhere on the top half of the screen is fine, as the boss will automatically connect the turret to itself.";
         
                 turretPanel.SetActive(true);
         
                if (GameObject.FindGameObjectWithTag("Turret") != null)
                 {
                     increaseIndex();
                 }
                 break;
         
             case 3:
                turretPanel.SetActive(false);
                tutText.text = "Brilliant! Now we can start configuring the turret. To do this, select the turret by clicking on it. ";
             if (turretOptionsPanel.activeInHierarchy)
             {
                 increaseIndex();
             }
             break;
         
             case 4:
                tutText.text = "Great! This panel is where you can configure this turrets targeting settings, as how much health it has...";
                if (Input.anyKeyDown)
                {
                    increaseIndex();
                }
                break;
         
             case 5:
                tutText.text = "...As well as how many streams of bullets it fires and if it can be destroyed in the current wave and if it is active or not. ";
                if (Input.anyKeyDown)
                {
                    increaseIndex();
                }

                break;
         
             case 6:
                tutText.text = "Lets not worry about that for now. Lets just focus on this turret first. \n" +
                    "Currently, it is stuck firing once per second at you. Lets change that!";
                if (Input.anyKeyDown)
                {
                    increaseIndex();
                }

                break;
         
             case 7:
                tutText.text = "Select the 'Turret firerate' text box and set the value to something small, (anything less than 0.5 should be good.)";

                if (GameObject.FindGameObjectWithTag("Turret").GetComponent<Turret>().firerate <= 0.5f)
                {
                    increaseIndex();
                }
                break;
         
             case 8:
                   tutText.text = "Nice! the value set in turret fireRate dictates the delay between the bullets that are fired. \n Lets look at the different aiming types.";
                if (Input.anyKeyDown)
                {
                    increaseIndex();
                }

                break;
         
             case 9:
                tutText.text = "The current setting makes the turret aim at the players current position. " +
                    "\nUseful for pressuring the player to move around the playfield." +
                    "\nSet the fire type to Arc aim to continue.";
                if (aimType.value == 1)
                {
                    increaseIndex();
                }
                break;
         
             case 10:
                tutText.fontSize = 20;
                tutText.text = "Arc aim rotates the turret within a fixed user angle. You can set the direction it points in, and how fast it rotates." +
                    "\nTo point it right, put how many degrees you want it to be rotated in arc direction. To make it point left, use a negative value." +
                    "\nuseful for applying pressure to general areas of the screen." +
                    "\nPress save turret settings to see your changes." +
                    "\nSet the fire type to Spiral to continue.";
                if (aimType.value == 2)
                {
                    increaseIndex();
                } 
                break;
         
             case 11:
                tutText.text = "Spiral aim makes the turret spin. Simple, really." +
                    "Set if the turret rotates clockwise or coutner clockwise by pressing the anti-clockwise toggle" +
                    "\nUseful for applying pressure to the entire screen. " +
                    "\nSet the fire type to Single direction to continue.";
                if (aimType.value == 3)
                {
                    increaseIndex();
                }
                break;
         
             case 12:
                tutText.text = "the last aiming style, Single direction." +
                    "\nset the direction it points in in the same way you aim arc shot (set decrees it rotates left or right.)" +
                    "\nthis mode is great for applying a constant stream of pressure to specfic areas of the screen.";
                if (Input.anyKeyDown)
                {
                    increaseIndex();
                }
                break;
         
             case 13:
                tutText.text = "Using just these aiming styles you can create some challenging bosses, however your boss will feel slightly lacking." +
                    "\nIn the next tutorial, you will learn about the bullet settings, and how to customise the type of shots your turrets fire. ";
                if (Input.anyKeyDown)
                {
                    increaseIndex();
                }

                break;
         
             case 14:
                tutText.text = "This concludes the Turret aiming tutorial. ";
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

        wavePanelButton = GameObject.Find("to Wave Settings");
        turretPanelButton = GameObject.Find("to Turret select");
        bulletPanelButton = GameObject.Find("to Bullet select");


        turretOptionsPanel = GameObject.Find("turret options");

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

        aimType = GameObject.Find("Turret aim types").GetComponent<Dropdown>();


        saveBoss = GameObject.Find("Save Boss");
        TEMPLOAD = GameObject.Find("TEMP load");
        TestBoss = GameObject.Find("TEST");
        clearAll = GameObject.Find("clear boss");
        yield return null;
    }
}
