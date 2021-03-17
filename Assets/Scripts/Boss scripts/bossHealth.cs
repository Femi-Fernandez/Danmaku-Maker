using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossHealth : MonoBehaviour
{

    public int health;
    [SerializeField]
    private Text bossHealthText;

    [SerializeField]
    private Text youWinText;

    [SerializeField]
    private bossHealthBar bossHPBar;

    private void Start()
    {
        youWinText.enabled = false;
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
        updateBossHealthText();
        bossHPBar.setHealth(health);
        if (health <= 0)
        {
            this.transform.parent.gameObject.SetActive(false);
            youWinText.enabled = true;
            youWinText.text = "YOU WIN! Congrats!";
        }
    }

    public void updateBossHealthText()
    {
        bossHealthText.text = "boss health: " + health.ToString();
    }
}
