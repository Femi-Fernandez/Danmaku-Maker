using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossHealthBar : MonoBehaviour
{

    public Slider bossHealthSlider;

    // Start is called before the first frame update
    public void setHealth(int healthVal) 
    {
        bossHealthSlider.value = healthVal;
    }

    public void setMax(int Max)
    {
        bossHealthSlider.maxValue = Max;
        bossHealthSlider.value = Max;
    }
}
