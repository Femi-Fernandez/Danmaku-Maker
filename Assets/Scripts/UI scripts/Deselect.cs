using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deselect : MonoBehaviour
{

    public RectTransform optionPanel;
    public RectTransform bulletPanel;

    private void OnMouseDown()
    {
        optionPanel.gameObject.SetActive(false);
        bulletPanel.gameObject.SetActive(false);

        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        for (int i = 0; i < turrets.Length; i++)
        {
            turrets[i].GetComponent<Turret_Fire>().enabled = false;
            turrets[i].GetComponent<Turret_Targeting>().enabled = false;
            turrets[i].GetComponent<Turret_BulletSetup>().enabled = false;
            turrets[i].transform.rotation = Quaternion.Euler(0,0,-90);
        }
    }
}
