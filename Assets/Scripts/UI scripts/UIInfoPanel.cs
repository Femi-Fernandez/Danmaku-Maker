using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInfoPanel : MonoBehaviour
{
    public GameObject[] helpBoxes = new GameObject[7];

    public GameObject infoPanel;
    Button close;
    // Start is called before the first frame update
    private void Start()
    {
        infoPanel = GameObject.Find("Info Panel");
        infoPanel.SetActive(true);
        helpBoxes[0] = GameObject.Find("Help boxes 1");
        helpBoxes[1] = GameObject.Find("Help boxes 2");
        helpBoxes[2] = GameObject.Find("Help boxes 3");
        helpBoxes[3] = GameObject.Find("Help boxes 4");
        helpBoxes[4] = GameObject.Find("Help boxes 5");
        helpBoxes[5] = GameObject.Find("Help boxes 6");
        helpBoxes[6] = GameObject.Find("Help boxes 7");
        infoPanel.SetActive(false);
    }

    public void displayInfo(string infoType)
    {
        switch (infoType)
        { 
            case "turret":
                helpBoxes[0].SetActive(true);
                helpBoxes[0].transform.GetChild(0).GetComponent<Text>().text = "Turret health";
                helpBoxes[0].transform.GetChild(1).GetComponent<Text>().text = "Sets the health of the selected turret. only applicable if 'Is Destructable' is toggled on";

                helpBoxes[1].SetActive(true);
                helpBoxes[1].transform.GetChild(0).GetComponent<Text>().text = "Num of streams";
                helpBoxes[1].transform.GetChild(1).GetComponent<Text>().text = "Sets how many streams of bullets the turret can fire. each stream can fire different types of bullet, in completely separate patterns and movement styles.";

                helpBoxes[2].SetActive(true);
                helpBoxes[2].transform.GetChild(0).GetComponent<Text>().text = "Stream to edit";
                helpBoxes[2].transform.GetChild(1).GetComponent<Text>().text = "Select the stream that you wish to modify.";

                helpBoxes[3].SetActive(true);
                helpBoxes[3].transform.GetChild(0).GetComponent<Text>().text = "Aiming type";
                helpBoxes[3].transform.GetChild(1).GetComponent<Text>().text = "Target player: fires at the players current position \nArc aim: rotates within a fixed angle \nSpiral: Spins in a spiral pattern \nSingle direction: fires in a single direction";

                helpBoxes[4].SetActive(true);
                helpBoxes[4].transform.GetChild(0).GetComponent<Text>().text = "Turret firerate";
                helpBoxes[4].transform.GetChild(1).GetComponent<Text>().text = "Set the firing delay of the turret. The lower the number, the faster the turret fires.";
               
                helpBoxes[5].SetActive(true);
                helpBoxes[5].transform.GetChild(0).GetComponent<Text>().text = "Active in wave";
                helpBoxes[5].transform.GetChild(1).GetComponent<Text>().text = "When ticked, the turret will be active in the currently selected wave. If you want a turret to appear later in the fight, disable it in the earlier waves.";

                helpBoxes[6].SetActive(true);
                helpBoxes[6].transform.GetChild(0).GetComponent<Text>().text = "Is destructable";
                helpBoxes[6].transform.GetChild(1).GetComponent<Text>().text = "Tick if you want the turret to be destroyable in the current wave. Would reccomend leaving the main firing turret invincible for the entire fight.";

                break;

            case "bullet":
                helpBoxes[0].SetActive(true);
                helpBoxes[0].transform.GetChild(0).GetComponent<Text>().text = "Stream number";
                helpBoxes[0].transform.GetChild(1).GetComponent<Text>().text = "Select which stream you want to edit";

                helpBoxes[1].SetActive(true);
                helpBoxes[1].transform.GetChild(0).GetComponent<Text>().text = "Fire Type";
                helpBoxes[1].transform.GetChild(1).GetComponent<Text>().text = "Single shot: fire a single bullet \nStaggered burst: fire a stream of bullets, then pause. \n shotgun burst: fires a spread of bullets \n random burst: fires a random burst of bullets.";

                helpBoxes[2].SetActive(true);
                helpBoxes[2].transform.GetChild(0).GetComponent<Text>().text = "Movement type";
                helpBoxes[2].transform.GetChild(1).GetComponent<Text>().text = "Straight: bullets move straight \nWave: bullets move in a sine wave \ninc/dec: bullets speed up and slow down \n travel/change: travel straight then change";

                helpBoxes[3].SetActive(true);
                helpBoxes[3].transform.GetChild(0).GetComponent<Text>().text = "Bullet speed";
                helpBoxes[3].transform.GetChild(1).GetComponent<Text>().text = "Set the starting speed of fired bullets.";

                for (int i = 4; i < 7; i++)
                {
                    helpBoxes[i].SetActive(false);
                }
                break;

            case "stream":
                helpBoxes[0].SetActive(true);
                helpBoxes[0].transform.GetChild(0).GetComponent<Text>().text = "Number of bullets";
                helpBoxes[0].transform.GetChild(1).GetComponent<Text>().text = "Select how many bullets are fired in sequence.";

                helpBoxes[1].SetActive(true);
                helpBoxes[1].transform.GetChild(0).GetComponent<Text>().text = "Bullet speed increase";
                helpBoxes[1].transform.GetChild(1).GetComponent<Text>().text = "Tick if you want each bullet to increase or decrease in speed. ";

                helpBoxes[2].SetActive(true);
                helpBoxes[2].transform.GetChild(0).GetComponent<Text>().text = "Speed increase ammount";
                helpBoxes[2].transform.GetChild(1).GetComponent<Text>().text = "Set how much speed you want to add to each bullet in the stream. (use a small number so the change is gradual)";

                for (int i = 3; i < 7; i++)
                {
                    helpBoxes[i].SetActive(false);
                }
                break;

            case "shotgun":
                helpBoxes[0].SetActive(true);
                helpBoxes[0].transform.GetChild(0).GetComponent<Text>().text = "Number of bullets";
                helpBoxes[0].transform.GetChild(1).GetComponent<Text>().text = "Select how many bullets are fired in the shotgun spread";

                helpBoxes[1].SetActive(true);
                helpBoxes[1].transform.GetChild(0).GetComponent<Text>().text = "Angle between bullets";
                helpBoxes[1].transform.GetChild(1).GetComponent<Text>().text = "Sets the angle between each bullet in the spread. keep the value small for more concentrated fire, and larger numbers for more radial firing types.";

                helpBoxes[2].SetActive(true);
                helpBoxes[2].transform.GetChild(0).GetComponent<Text>().text = "Straight shotgun bullets";
                helpBoxes[2].transform.GetChild(1).GetComponent<Text>().text = "Toggles if the bullets fire in a straight line. use if you want a flat shot of bullets instead of a curved angle";

                for (int i = 3; i < 7; i++)
                {
                    helpBoxes[i].SetActive(false);
                }

                break;

            case "random":
                helpBoxes[0].SetActive(true);
                helpBoxes[0].transform.GetChild(0).GetComponent<Text>().text = "Number of bullets";
                helpBoxes[0].transform.GetChild(1).GetComponent<Text>().text = "Select how many bullets are fired in the random spread";

                helpBoxes[1].SetActive(true);
                helpBoxes[1].transform.GetChild(0).GetComponent<Text>().text = "Random burst range";
                helpBoxes[1].transform.GetChild(1).GetComponent<Text>().text = "Sets the area that the random burst can fire in.";
                
                for (int i = 2; i < 7; i++)
                {
                    helpBoxes[i].SetActive(false);
                }
                break;

            case "target player":

                helpBoxes[0].SetActive(true);
                helpBoxes[0].transform.GetChild(0).GetComponent<Text>().text = "Smooth targeting";
                helpBoxes[0].transform.GetChild(1).GetComponent<Text>().text = "Toggle for if you want the turret's targeting to lag behind where the player is slightly.";

                helpBoxes[1].SetActive(true);
                helpBoxes[1].transform.GetChild(0).GetComponent<Text>().text = "Smooth targeting speed";
                helpBoxes[1].transform.GetChild(1).GetComponent<Text>().text = "Set the speed of the smooth targeting. The lower the number, the slower the turret will move.";

                helpBoxes[2].SetActive(true);
                helpBoxes[2].transform.GetChild(0).GetComponent<Text>().text = "Targeting offset";
                helpBoxes[2].transform.GetChild(1).GetComponent<Text>().text = "Set if you want the turret to not fire directly at the player. Use if you want to restrict the players movement around the stage. \nUse negative values to offset to the left, positive for the right.";

                for (int i = 3; i < 7; i++)
                {
                    helpBoxes[i].SetActive(false);
                }
                break;

            case "arc shot":

                helpBoxes[0].SetActive(true);
                helpBoxes[0].transform.GetChild(0).GetComponent<Text>().text = "Arc size";
                helpBoxes[0].transform.GetChild(1).GetComponent<Text>().text = "Sets the angle that the turret will rotate within";

                helpBoxes[1].SetActive(true);
                helpBoxes[1].transform.GetChild(0).GetComponent<Text>().text = "Arc direction";
                helpBoxes[1].transform.GetChild(1).GetComponent<Text>().text = "Sets the direction that the centre of the arc points at. \nUse negative values to point left, positive to point right.";

                helpBoxes[2].SetActive(true);
                helpBoxes[2].transform.GetChild(0).GetComponent<Text>().text = "Rotation speed";
                helpBoxes[2].transform.GetChild(1).GetComponent<Text>().text = "Set how fast the turret will rotate.";
                
                for (int i = 3; i < 7; i++)
                {
                    helpBoxes[i].SetActive(false);
                }
                break;

            case "spiral shot":

                helpBoxes[0].SetActive(true);
                helpBoxes[0].transform.GetChild(0).GetComponent<Text>().text = "Rotation speed";
                helpBoxes[0].transform.GetChild(1).GetComponent<Text>().text = "Set how fast the turret will spin";

                helpBoxes[1].SetActive(true);
                helpBoxes[1].transform.GetChild(0).GetComponent<Text>().text = "Anti-Clockwise";
                helpBoxes[1].transform.GetChild(1).GetComponent<Text>().text = "toggle if the turret will spin clockwise or anticlockwise. \ntick for the turret to spin coutner";

                for (int i = 2; i < 7; i++)
                {
                    helpBoxes[i].SetActive(false);
                }
                break;

            case "single direction":

                helpBoxes[0].SetActive(true);
                helpBoxes[0].transform.GetChild(0).GetComponent<Text>().text = "Set direction";
                helpBoxes[0].transform.GetChild(1).GetComponent<Text>().text = "set what direction the turret will point in \nUse negative numbers to point left, positive for right.";


                for (int i = 1; i < 7; i++)
                {
                    helpBoxes[i].SetActive(false);
                }
                break;

            case "sine movement":

                helpBoxes[0].SetActive(true);
                helpBoxes[0].transform.GetChild(0).GetComponent<Text>().text = "Amplitude";
                helpBoxes[0].transform.GetChild(1).GetComponent<Text>().text = "How big the wave movement of the bullet is. ";

                helpBoxes[1].SetActive(true);
                helpBoxes[1].transform.GetChild(0).GetComponent<Text>().text = "Frequency";
                helpBoxes[1].transform.GetChild(1).GetComponent<Text>().text = "How short are the wave lengths? the bigger the number the faster the bullet will complete a wave.";

                helpBoxes[2].SetActive(true);
                helpBoxes[2].transform.GetChild(0).GetComponent<Text>().text = "Tips";
                helpBoxes[2].transform.GetChild(1).GetComponent<Text>().text = "keep the frequency lower than the amplitude to make the bullets movements less erratic ";

                for (int i = 3; i < 7; i++)
                {
                    helpBoxes[i].SetActive(false);
                }

                break;

            case "variable speed":
                
                helpBoxes[0].SetActive(true);
                helpBoxes[0].transform.GetChild(0).GetComponent<Text>().text = "Fastest speed";
                helpBoxes[0].transform.GetChild(1).GetComponent<Text>().text = "Set the max speed that the bullets will move at.";

                helpBoxes[1].SetActive(true);
                helpBoxes[1].transform.GetChild(0).GetComponent<Text>().text = "Slowest speed";
                helpBoxes[1].transform.GetChild(1).GetComponent<Text>().text = "Set the slowest speed that the bullets will move at. you can set a negative value to have the bullet move backwards.";

                helpBoxes[2].SetActive(true);
                helpBoxes[2].transform.GetChild(0).GetComponent<Text>().text = "Frequency";
                helpBoxes[2].transform.GetChild(1).GetComponent<Text>().text = "Set how fast the bullet's speed changes.";

                helpBoxes[3].SetActive(true);
                helpBoxes[3].transform.GetChild(0).GetComponent<Text>().text = "Tips";
                helpBoxes[3].transform.GetChild(1).GetComponent<Text>().text = "The bullet will start at the speed set in bullet speed";

                for (int i = 4; i < 7; i++)
                {
                    helpBoxes[i].SetActive(false);
                }

                break;

            case "travel then target":

                helpBoxes[0].SetActive(true);
                helpBoxes[0].transform.GetChild(0).GetComponent<Text>().text = "Time until change";
                helpBoxes[0].transform.GetChild(1).GetComponent<Text>().text = "Set how long it takes for the bullet to change targeting types";

                helpBoxes[1].SetActive(true);
                helpBoxes[1].transform.GetChild(0).GetComponent<Text>().text = "NTT: Target player";
                helpBoxes[1].transform.GetChild(1).GetComponent<Text>().text = "Bullet targets player when it changes targeting types";

                helpBoxes[2].SetActive(true);
                helpBoxes[2].transform.GetChild(0).GetComponent<Text>().text = "NTT: drop down";
                helpBoxes[2].transform.GetChild(1).GetComponent<Text>().text = "Bullet moves straight down when it changes targeting types";
                
                helpBoxes[3].SetActive(true);
                helpBoxes[3].transform.GetChild(0).GetComponent<Text>().text = "NTT: Random";
                helpBoxes[3].transform.GetChild(1).GetComponent<Text>().text = "Bullet moves in a random directon when it changes targeting types";

                helpBoxes[4].SetActive(true);
                helpBoxes[4].transform.GetChild(0).GetComponent<Text>().text = "speed after target";
                helpBoxes[4].transform.GetChild(1).GetComponent<Text>().text = "set the speed the bullet moves at after it changes targeting types";

                for (int i = 5; i < 7; i++)
                {
                    helpBoxes[i].SetActive(false);
                }

                break;

            default:
                for (int i = 0; i < 7; i++)
                {
                    helpBoxes[i].SetActive(false);
                }
                break;
        }
    }
}
