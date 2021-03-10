using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInfoPanel : MonoBehaviour
{
    GameObject[] helpBoxes = new GameObject[7];

    GameObject infoPanel;
    Button close;
    // Start is called before the first frame update
    void Start()
    {
        helpBoxes[0] = GameObject.Find("Help boxes 1");
        helpBoxes[1] = GameObject.Find("Help boxes 2");
        helpBoxes[2] = GameObject.Find("Help boxes 3");
        helpBoxes[3] = GameObject.Find("Help boxes 4");
        helpBoxes[4] = GameObject.Find("Help boxes 5");
        helpBoxes[5] = GameObject.Find("Help boxes 6");
        helpBoxes[6] = GameObject.Find("Help boxes 7");

        infoPanel = GameObject.Find("Info Panel");
        close = GameObject.Find("close info panel").GetComponent<Button>();

        close.onClick.AddListener(delegate
        {
            infoPanel.SetActive(false);
        });
    }


    public void displayInfo(string infoType)
    {
        switch (infoType)
        { 
            case "turret":
                helpBoxes[0].SetActive(true);
                helpBoxes[0].transform.GetChild(0).GetComponent<Text>().text = "turret health";
                helpBoxes[0].transform.GetChild(1).GetComponent<Text>().text = "Sets the health of the selected turret";

                helpBoxes[1].SetActive(true);
                helpBoxes[1].transform.GetChild(0).GetComponent<Text>().text = "# of streams";
                helpBoxes[1].transform.GetChild(1).GetComponent<Text>().text = "Sets how many streams of bullets the turret can fire. each stream can fire different types of bullet, in completely separate patterns and movement styles.";

                helpBoxes[2].SetActive(true);
                helpBoxes[2].transform.GetChild(0).GetComponent<Text>().text = "Stream to edit";
                helpBoxes[2].transform.GetChild(1).GetComponent<Text>().text = "Select the stream that you wish to modify.";

                helpBoxes[3].SetActive(true);
                helpBoxes[3].transform.GetChild(0).GetComponent<Text>().text = "Aiming type";
                helpBoxes[3].transform.GetChild(1).GetComponent<Text>().text = "Target player: fires at the players current position \nArc aim: rotates within a fixed angle \nSpiral: Spins in a spiral pattern \nSingle direction: fires in a single direction";

                helpBoxes[4].SetActive(true);
                helpBoxes[4].transform.GetChild(0).GetComponent<Text>().text = "turret firerate";
                helpBoxes[4].transform.GetChild(1).GetComponent<Text>().text = "set the firing delay of the turret. The lower the number, the faster the turret fires.";
               
                helpBoxes[5].SetActive(true);
                helpBoxes[5].transform.GetChild(0).GetComponent<Text>().text = "Active in wave";
                helpBoxes[5].transform.GetChild(1).GetComponent<Text>().text = "when ticked, the turret will be active in the currently selected wave. If you want a turret to appear later in the fight, disable it in the earlier waves.";

                helpBoxes[6].SetActive(true);
                helpBoxes[6].transform.GetChild(0).GetComponent<Text>().text = "isDestructable";
                helpBoxes[6].transform.GetChild(1).GetComponent<Text>().text = "Tick if you want the turret to be destroyable in the current wave. Would reccomend leaving the main firing turret invincible for the entire fight, so the player must dodge.";

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
