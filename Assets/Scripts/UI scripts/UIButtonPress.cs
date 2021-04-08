using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //Button thisButton;
    private GameObject buttonText;
    private Vector2 textStartPos;

    private bool isPressed;
    // Start is called before the first frame update
    void Start()
    {
        //thisButton = GetComponent<Button>();
        buttonText = transform.GetChild(0).gameObject;
        textStartPos = transform.GetChild(0).transform.position;
    }


    // Start is called before the first frame update


    void moveTextDown()
    {

        buttonText.transform.position = new Vector2(textStartPos.x, textStartPos.y - 5.5f);
        //currentPos = buttonText.transform.position;     
    }

    void Update()
    {
        if (isPressed == true)
        {  
            moveTextDown();
        }
        else
        {
            buttonText.transform.position = textStartPos;
        }
    }
    public void OnPointerDown(PointerEventData data)
    {
        isPressed = true;
    }
    public void OnPointerUp(PointerEventData data)
    {
        isPressed = false;
    }
}
