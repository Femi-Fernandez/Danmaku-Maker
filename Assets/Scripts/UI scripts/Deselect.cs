using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deselect : MonoBehaviour
{

    public RectTransform optionPanel;

    private void OnMouseDown()
    {
        optionPanel.gameObject.SetActive(false);
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        for (int i = 0; i < turrets.Length; i++)
        {
            turrets[i].GetComponent<bul_shoot_down>().enabled = false;
            turrets[i].GetComponent<point_at_player>().enabled = false;
            turrets[i].transform.rotation = Quaternion.Euler(0,0,0);
        }
    }
}
