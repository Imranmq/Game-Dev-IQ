using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    public float fuelbarAmmount = 0.5f;
    private Image fuelBarCom;

    // Start is called before the first frame update
    void Start()
    {
        fuelBarCom =  gameObject.GetComponent<Image>();
        
    }


    public void SetValueOfFuelBar(float fuelPercentage)
    {
        fuelBarCom.fillAmount = fuelPercentage;
    }
}
