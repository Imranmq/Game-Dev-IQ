using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UISliderManager : MonoBehaviour
{
    Slider slider;
    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }
    public void SetValue(float value)
    {
        slider.value = value;
    }
    public void SetMaxValue(float value)
    {

        slider.maxValue = value;
       // SetValue(value);
    }
}
