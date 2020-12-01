using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public float health;
    [SerializeField]
    private Text playerHealthText;

    [SerializeField]
    private Text youLoseText;



    public void Damage()
    {
        health -= 1;
        updatePlayerHealthText();

        if (health <= 0)
        {
            Destroy(this);
            youLoseText.enabled = true;
            youLoseText.text = "You died!";
            this.gameObject.SetActive(false);
        }
    }

    public void updatePlayerHealthText()
    {
        playerHealthText.text = "your health: " + health.ToString();
    }
}
