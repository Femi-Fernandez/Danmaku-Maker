using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class turretSelect : MonoBehaviour
{

    public RectTransform optionPanel;
    // Start is called before the first frame update
      void Start()
      {
        optionPanel.GetComponentsInChildren<Text>()[1].text = GetComponent<Turret>().firerate.ToString();
      }
    //
    //  // Update is called once per frame
    //  void Update()
    //  {
    //      
    //  }
    private void OnMouseDown()
    {
        GetComponent<Turret_Fire>().enabled = true;
        GetComponent<Turret_Targeting>().enabled = true;

        optionPanel.gameObject.SetActive(true);
       
        if (optionPanel.GetComponentsInChildren<Text>()[1].text != null)
        {
            GetComponent<Turret>().firerate = float.Parse( optionPanel.GetComponentInChildren<Text>().text );
        }
    }
}
