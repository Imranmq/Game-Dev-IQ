using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarManagerUI : MonoBehaviour
{
    Slider slider;
    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }
    public void SetMaxHealth(float health)
    {
     
        slider.maxValue = health;
        SetHealth(health);
    }
    public void SetHealth(float health)
    {
     
        slider.value = health;
    }
}
