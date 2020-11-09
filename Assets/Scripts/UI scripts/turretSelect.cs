using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class turretSelect : MonoBehaviour
{

    GameObject optionPanel;

    GameObject fireRateInput;
    GameObject turretName;
    GameObject aimType;
    // Start is called before the first frame update
    void Start()
    {
        optionPanel = GameObject.Find("turret options");
        //
        fireRateInput = GameObject.Find("turr firerate");
        turretName = GameObject.Find("SelectedTurret");
        aimType = GameObject.Find("Turret aim types");

        fireRateInput.GetComponentsInChildren<Text>()[1].text = GetComponent<Turret>().firerate.ToString();
        turretName.GetComponent<Text>().text = "Selected turret " + this.name;
        aimType.GetComponent<Dropdown>().value = GetComponent<Turret>().targetingType - 1;

        aimType.GetComponent<Dropdown>().onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(aimType.GetComponent<Dropdown>());
        }
        );
      }

    private void OnMouseDown()
    {
        GetComponent<Turret_Fire>().enabled = true;
        GetComponent<Turret_Targeting>().enabled = true;

        //optionPanel.SetActive(true);
       
        if (optionPanel.GetComponentsInChildren<Text>()[1].text != null)
        {
            GetComponent<Turret>().firerate = float.Parse(fireRateInput.GetComponentsInChildren<Text>()[0].text);
        }
    }
    void DropdownValueChanged(Dropdown change)
    {
        GetComponent<Turret>().targetingType = change.value + 1;
    }
}
