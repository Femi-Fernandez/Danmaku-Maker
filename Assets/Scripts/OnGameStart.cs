using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameStart : MonoBehaviour
{

    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject turretPanel;
    [SerializeField]
    private GameObject optionsPanel;

    [SerializeField]
    private GameObject UIManager;

    GameObject[] bullets;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UIManager.GetComponent<SceneControl>().EnterScene());
        
        TestMode();
    }


    public void TestMode()
    {
        player.SetActive(true);
        boss.SetActive(true);
        turretPanel.SetActive(true);
        boss.transform.GetChild(0).GetComponent<bossHealth>().health = 1000;
        player.GetComponent<playerHealth>().health = 5;
        player.GetComponent<BoxCollider2D>().enabled = false;
        boss.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
    }

    public void PlayMode()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        for (int i = 0; i < turrets.Length; i++)
        {
            turrets[i].transform.rotation = Quaternion.Euler(0, 0, -90);
        }

        optionsPanel.SetActive(false);
        turretPanel.SetActive(false);
        player.GetComponent<BoxCollider2D>().enabled = true;
        boss.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;

    }
}
