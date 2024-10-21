using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManagerScript : MonoBehaviour
{
   public GameObject defaultObj;
   public void PlaceObjOnTile(string name)
    {
        GameObject prefab = GetPrefabOfName(name);
        if(AstropolyUtils.CheckForNotNull(prefab))
        {
            Instantiate(prefab);
        }
    }
    GameObject GetPrefabOfName(string name)
    {
        // Logic Todo : find Prefab with name in Placeable Folder in Prefab Assets Folder. 
        return defaultObj;
    }
}
