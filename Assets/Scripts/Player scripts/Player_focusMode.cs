using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_focusMode : MonoBehaviour
{
    //public GameObject barrel_center;
    public GameObject barrel_left;
    public GameObject barrel_right;
    //public GameObject barrel_left_boost;
    //public GameObject barrel_right_boost;



    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            
            barrel_left.transform.eulerAngles = new Vector3(-90, 90, 90);
            //barrel_left_boost.transform.eulerAngles = new Vector3(-90, 90, 90);
            barrel_right.transform.eulerAngles = new Vector3(-90, 90, 90);
            //barrel_right_boost.transform.eulerAngles = new Vector3(-90, 90, 90);
        } 
        else
        {
            barrel_left.transform.eulerAngles = new Vector3(-95, 90, 90);
            //barrel_left_boost.transform.eulerAngles = new Vector3(-95, 90, 90);
            barrel_right.transform.eulerAngles = new Vector3(-85, 90, 90);
            //barrel_right_boost.transform.eulerAngles = new Vector3(-85, 90, 90);
        }
    }
}
