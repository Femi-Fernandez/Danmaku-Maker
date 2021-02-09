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
    private GameObject wavePanel;

    [SerializeField]
    private GameObject UIManager;

    GameObject[] turretMain;

    GameObject[] bullets;

    public AnalyticsCommands AC;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UIManager.GetComponent<SceneControl>().EnterScene());
        turretMain = GameObject.FindGameObjectsWithTag("turret Main");
        TestMode();
    }


    public void TestMode()
    {
        player.SetActive(true);
        boss.SetActive(true);
        turretPanel.SetActive(true);
        wavePanel.SetActive(false);
        boss.transform.GetChild(0).GetComponent<bossHealth>().health = 3000;
        player.GetComponent<playerHealth>().health = 5;
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.transform.GetChild(1).GetComponent<PlayerShoot>().bulletCollidersOn = false;
        player.transform.GetChild(2).GetComponent<PlayerShoot>().bulletCollidersOn = false;

        boss.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
        boss.transform.GetChild(0).GetComponent<bossWaveControl>().fightingBoss = false;

        GameObject[] turrets = GameObject.FindGameObjectsWithTag("turret Main");
        for (int i = 0; i < turretMain.Length; i++)
        {
            turretMain[i].SetActive(true);
            if (turretMain[i].GetComponent<BoxCollider2D>())
            {
                turretMain[i].GetComponent<BoxCollider2D>().enabled = true;
            }


            for (int j = 0; j < 4; j++)
            {
                turretMain[i].GetComponent<turretSubwaveStorage>().hasBeenDestroyed[j] = false;
            }
        }
    }

    public void PlayMode()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        for (int i = 0; i < turrets.Length; i++)
        {
            turrets[i].transform.rotation = Quaternion.Euler(0, 0, -90);
        }

         turretMain = GameObject.FindGameObjectsWithTag("turret Main");
        for (int i = 0; i < turretMain.Length; i++)
        {
            turretMain[i].GetComponent<TurretHealth>().health = turretMain[i].transform.GetChild(0).GetComponent<Turret>().turretHealth;
          // Debug.Log(turretMain[i].transform.GetChild(0));
            turretMain[i].GetComponent<BoxCollider2D>().enabled = true;

            for (int j = 0;  j< 4; j++)
            {
                turretMain[i].GetComponent<turretSubwaveStorage>().hasBeenDestroyed[j] = false;
            }
           
        }

        optionsPanel.SetActive(false);
        turretPanel.SetActive(false);
        player.GetComponent<BoxCollider2D>().enabled = true;
        player.transform.GetChild(1).GetComponent<PlayerShoot>().bulletCollidersOn = true;
        player.transform.GetChild(2).GetComponent<PlayerShoot>().bulletCollidersOn = true;
        boss.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
        boss.transform.GetChild(0).GetComponent<bossWaveControl>().fightingBoss = true;
        boss.transform.GetChild(0).GetComponent<bossWaveControl>().wavesStarted = false;
        boss.transform.GetChild(0).GetComponent<bossWaveControl>().currentWave = 0;
        boss.transform.GetChild(0).GetComponent<bossWaveControl>().currentSubwave = 0;

        player.transform.position = new Vector3(0, -4.1f, 0);

        AC.testBossPressed();
        //UIManager.GetComponent<UIManager>().SetAllValues();
    }
}
