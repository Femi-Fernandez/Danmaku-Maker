using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_bullet : MonoBehaviour
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


    Dropdown fireType;
    Dropdown moveType;
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
                 tutText.text = "This tutorial will go over the bullet panel.";
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
                tutText.text = "Great! Now, select the turret to open the configuration panel.";
                if (turretOptionsPanel.activeInHierarchy)
                {
                    increaseIndex();
                }
                break;
         
             case 3:
                
                tutText.text = "At the top of the panel on the left, you will see two new buttons. you can switch between the turret options and bullet options using them." +
                    "\nYou should already know how the turret aiming options work, so click on the button that says 'bullet'.";
                toBulletSettings.SetActive(true);
                toTurretSettings.SetActive(true);
                if (bulletOptionsPanel.activeInHierarchy)
                {
                    increaseIndex();
                }
                break;
         
             case 4:
                tutText.text = "Great! Here, we can set what formation the turrets will fire in, as well as how the bullets will move once fired." +
                    "\nWe will start with the bullet fire types.";
                if (Input.anyKeyDown)
                {
                    increaseIndex();
                }
                break;
         
             case 5:
                tutText.text = "Single shot, as it says, will fire a single bullet only. pretty simple." +
                    "\nTo continue, change he fire type to stream shot";
                if (fireType.value == 1)
                {
                    increaseIndex();
                }

                break;
         
             case 6:
                tutText.text = "Stream shot shoots a string of bullets in a row." +
                    "\nGreat when used with spiral aim or arc aim, as it covers a wider space compared to a single bullet." +
                    "\nTo continue, change the fire type to shotgun burst.";
                if (fireType.value == 2)
                {
                    increaseIndex();
                }

                break;
         
             case 7:
                tutText.text = "Shotgun burst shoots a spread of bullets in an arc." +
                    "\nYou can get creative with it, for example if you want a 4 way spiral set the number of bullets to 4 and the angle to 90." +
                    "\nTo continue, change the fire type to random burst.";

                if (fireType.value == 3)
                {
                    increaseIndex();
                }
                break;
         
             case 8:
                   tutText.text = "Random burst is similar to shotgin burst, however the bullets are shot randomly." +
                    "\n you simply set the angle it fires in and it will fire a random assortment of bullets within it.";
                if (Input.anyKeyDown)
                {
                    increaseIndex();
                }

                break;
         
             case 9:
                tutText.text = "thats it for the current firing types. Next is the bullet movement types.";
                if (Input.anyKeyDown)
                {
                    increaseIndex();
                }
                break;
         
             case 10:
                tutText.fontSize = 20;
                tutText.text = "The straight movement type will simply shoot the bullet in a straight line. Nothing too fancy." +
                    "\nTo proceed, change the bullet move type to Wave.";
                if (moveType.value == 1)
                {
                    increaseIndex();
                } 
                break;
         
             case 11:
                tutText.text = "Wave movement allows you to set the amplitude and frequency of the wave the bullet will move on." +
                    "\nFor a standard wave, set the two values to be the same." +
                    "\nTo proceed, change the bullet move type to speed increase/decrease";
                if (moveType.value == 2)
                {
                    increaseIndex();
                }
                break;
         
             case 12:
                tutText.text = "Speed increase/decrease allows you to give bullets a pulsing movement." +
                    " Set their maximum speed and minimum speed and how long they alternate between them." +
                    " This allows you to make patterns with bullets that fire outward then return back to the boss, for example." +
                    "\nto proceed, change the bullet move type to travel and change.";
                if (moveType.value == 3)
                {
                    increaseIndex();
                }
                break;
         
             case 13:
                tutText.text = "The last bullet movement option lets the bullet change its movement mid-flight. Sick!" +
                    "\nset how long it takes for the bullet to change its targeting type, then set how fast it moves after its targeting changes.";
                if (Input.anyKeyDown)
                {
                    increaseIndex();
                }

                break;
         
             case 14:
                tutText.text = "going over thenew targeting types quickly:" +
                    "\nTarget player makes the bullets travel toward where the player was when the targeting changed." +
                    "\nDrop down sends the bullets directly downward toward the bottom of the screen. " +
                    "\nRandom sends the bullets in a random direction.";
                if (Input.anyKeyDown)
                {
                    increaseIndex();
                }
                break;

            case 15:
                tutText.text = "Phew! thats alot of options! Once you mess around with it it isnt as complicated as it looks.";
                if (Input.anyKeyDown)
                {
                    increaseIndex();
                }

                break;



            case 16:
                tutText.text = "That concludes the bullet configuration tutorial/";
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

        fireType = GameObject.Find("fire type input").GetComponent<Dropdown>();
        moveType = GameObject.Find("movement type input").GetComponent<Dropdown>();

        saveBoss = GameObject.Find("Save Boss");
        TEMPLOAD = GameObject.Find("TEMP load");
        TestBoss = GameObject.Find("TEST");
        clearAll = GameObject.Find("clear boss");
        yield return null;
    }
}
