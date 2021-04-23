using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public float health;
    [SerializeField]
    private Text playerHealthText;

    [SerializeField]
    private Text youLoseText;

    public Color spriteNorm;
    public Color spriteDamage;
    public SpriteRenderer sprite;

    public AnalyticsCommands AC;

    public void Damage()
    {
        health -= 1;
        updatePlayerHealthText();
        StartCoroutine(takenDamage());
        if (health <= 0)
        {
            //Destroy(this);
            youLoseText.enabled = true;
            youLoseText.text = "You died!";
            this.gameObject.SetActive(false);
            AC.playerDefeated();
        }
    }

    public void updatePlayerHealthText()
    {
        playerHealthText.text = "your health: " + health.ToString();
    }

    IEnumerator takenDamage()
    {
        sprite.color = spriteDamage;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        GetComponent<BoxCollider2D>().enabled = true;
        sprite.color = spriteNorm;
    }
}
