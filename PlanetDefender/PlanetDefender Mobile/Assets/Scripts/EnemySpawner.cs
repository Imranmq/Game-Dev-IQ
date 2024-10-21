using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject planetObject;
    [SerializeField]
    private List<GameObject> enemyShipType = new List<GameObject>();
    private void Start()
    {
        if(planetObject == null)
        {
            Debug.Log("ERROR : NO PLANET ASSIGN TO ENEMY SPAWNER");

        }else
        {
            enemyShipType.ForEach(delegate (GameObject enType) {
                SpawnShip(enType);
            });
        }
    }
    #region spawn methods
    void SpawnShip(GameObject enType)
    {
        GameObject enShip = Instantiate(enType, transform.position, enType.transform.rotation);
        planetObject.GetComponent<PlanetManager>().enemyShipSpawned(enShip);
        enShip.GetComponent<EnemyShip>().planetObjScript = planetObject.GetComponent<PlanetManager>();
    }
    #endregion

}
