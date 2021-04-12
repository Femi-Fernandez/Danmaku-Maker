using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISmoothText : MonoBehaviour
{
    // Start is called before the first frame update
    Text thisText;

    RectTransform rect;
    int numToMultiply;
    void Start()
    {
        numToMultiply = 5;
        thisText = GetComponent<Text>();
        rect = GetComponent<RectTransform>();

        thisText.fontSize = thisText.fontSize * numToMultiply;
        transform.localScale = transform.localScale / numToMultiply;
        rect.sizeDelta = new Vector2(rect.sizeDelta.x * numToMultiply, rect.sizeDelta.y * numToMultiply);


    }


}
