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
        optionPanel.GetComponentsInChildren<Text>()[1].text = "1";
      }
    //
    //  // Update is called once per frame
    //  void Update()
    //  {
    //      
    //  }
    private void OnMouseDown()
    {
        GetComponent<bul_shoot_down>().enabled = true;
        GetComponent<point_at_player>().enabled = true;

        optionPanel.gameObject.SetActive(true);
        //optionPanel.transform.Find("firerate_val").GetComponent<Text>().text = GetComponent<bul_shoot_down>().fireRate.ToString();
       
        if (optionPanel.GetComponentInChildren<Text>().text != null)
        {
            GetComponent<bul_shoot_down>().fireRate = float.Parse( optionPanel.GetComponentInChildren<Text>().text );
        }
    }
}
