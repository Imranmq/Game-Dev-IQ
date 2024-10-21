using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClockManager : MonoBehaviour
{
    #region Variables
    [SerializeField]
    GameObject mainPlanet;
    [SerializeField]
    private GameObject spawnedObject;
    private TurretService turretService;
  
    #endregion
    #region Methods
    public void checkRange(float sqrMgt, GameObject enShip)
    {
     
        if(spawnedObject  && spawnedObject.tag == "Turret" && sqrMgt < turretService.detectionRange) 
        {
            turretService.enemyInRange(enShip);
        }
    }
    private void OnMouseDown()
    {
       
        if(mainPlanet != null && spawnedObject == null)
        {
           
            GameObject objToSpawn = mainPlanet.GetComponent<PlanetManager>().getSelectedItemToPlace();
            if (objToSpawn != null && objToSpawn.tag =="Turret") {
                spawnedObject = Instantiate(objToSpawn, this.transform);
                turretService = spawnedObject.GetComponent<TurretService>();
            }
        }
    }
    private void Awake()
    {
        if(spawnedObject != null)
        {
            turretService = spawnedObject.GetComponent<TurretService>();
        }
    }
    #endregion
}
    