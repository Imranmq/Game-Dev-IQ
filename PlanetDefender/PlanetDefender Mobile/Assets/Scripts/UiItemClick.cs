using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiItemClick : MonoBehaviour
{
    [SerializeField]
    GameObject mainPlanet;
    [SerializeField]
    GameObject itemPrefabInUI;
  public void onItemUIClicked(bool val)
    {
        Debug.Log(gameObject.name +" : " + val);
        if (val == true)
        {
            gameObject.transform.localScale =new Vector3(1.1f, 1.1f, 1.1f);
            if (mainPlanet != null)
            {
                mainPlanet.GetComponent<PlanetManager>().itemToPlaceSelected(true, itemPrefabInUI);

            }
        }else
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            mainPlanet.GetComponent<PlanetManager>().itemToPlaceSelected(false, itemPrefabInUI);
        }

    }
}
