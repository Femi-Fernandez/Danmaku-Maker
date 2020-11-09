using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretDeselect : MonoBehaviour
{
    private void OnMouseDown()
    {
        //optionPanel.gameObject.SetActive(false);
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        for (int i = 0; i < turrets.Length; i++)
        {
            if (turrets[i] != this.gameObject)
            {
                turrets[i].GetComponent<Turret_Fire>().enabled = false;
                turrets[i].GetComponent<Turret_Targeting>().enabled = false;
                turrets[i].transform.rotation = Quaternion.Euler(0, 0, 0);
            }


        }
    }
}
