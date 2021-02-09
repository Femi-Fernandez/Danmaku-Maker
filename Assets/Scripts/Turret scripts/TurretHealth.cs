using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHealth : MonoBehaviour
{

    public float health;
    public turretSubwaveStorage subStor;
    public bossWaveControl bossWaveCont;

    // Start is called before the first frame update
    private void Start()
    {
        bossWaveCont = transform.parent.GetComponentInChildren<bossWaveControl>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player Bullet")
        {
            Damage();
            Destroy(collision.gameObject);
        }
    }

    void Damage()
    {
        health -= 10;
        //updateBossHealthText();

        if (health <= 0)
        {
            subStor.hasBeenDestroyed[bossWaveCont.currentWave] = true;
            this.transform.gameObject.SetActive(false);
            

           // youWinText.enabled = true;
           // youWinText.text = "YOU WIN! Congrats!";
        }
    }
}
