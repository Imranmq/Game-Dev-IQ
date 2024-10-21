using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueToBarsImage : MonoBehaviour
{   
    private Image imagebarComponent;

    // Start is called before the first frame update
    void Start()
    {
        imagebarComponent = gameObject.GetComponent<Image>();

    }


    public void SetValueOfDashboardBar(float valuePercentage)
    {
        imagebarComponent.fillAmount = valuePercentage;
    }
}
