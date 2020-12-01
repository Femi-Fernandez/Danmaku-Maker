using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteManager : MonoBehaviour
{

    public Animator anim;
    public float moveAmmount;
    public float changeAmmountStart;
    float changeAmmount;

    bool shiftPressed;
    // Start is called before the first frame update
    void start()
    {
        moveAmmount = 2;
        changeAmmount = changeAmmountStart;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        anim.SetInteger("direction", Convert.ToInt32(moveAmmount));

        if (Input.GetKey("left shift"))
        {
            changeAmmount = changeAmmountStart / 2;
        }
        else
        {
            changeAmmount = changeAmmountStart;
        }

        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            moveAmmount = moveAmmount - changeAmmount;
        }
        if (moveAmmount <0)
        {
            moveAmmount = 0;
        }
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            moveAmmount = moveAmmount + changeAmmount;
        }
        if (moveAmmount >4)
        {
            moveAmmount = 4;
        }

         if (moveAmmount > 2 && !(Input.GetKey("right") || Input.GetKey("d")))
         {
             moveAmmount -= changeAmmount;
         }
         if (moveAmmount < 2 && !(Input.GetKey("left") || Input.GetKey("a")))
         {
             moveAmmount += changeAmmount;
         }
    }
}
