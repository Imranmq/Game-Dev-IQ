using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    #region Internal Variables
    private List<GameObject> enemyShips = new List<GameObject>();
    [SerializeField]
    private GameObject[] clocks = new GameObject[12];
    #endregion
    #region HealthRelated variables    
    float health = 100f;
    public HealthBarManagerUI HealthUIService;
    #endregion
    #region UI RELATED variables
    private GameObject selectedItem;
    #endregion
    #region Unity methods
    private void Start()
    {

        if (HealthUIService != null)
        {
            HealthUIService.SetMaxHealth(health);
        }
    }
    private void Update()
    {
        checkHealth();
        checkEnemyShipsRange();

    }
    #endregion
    #region Methods called by other objects
    public void PlanetGotHit(float dmg)
    {
        health -= dmg;
        HealthUIService.SetHealth(health);
    }

    public void enemyShipSpawned(GameObject enShip)
    { enemyShips.Add(enShip); }
    public void removeEnemyShipFromList(GameObject enShip)
    { enemyShips.Remove(enShip); }
    public void itemToPlaceSelected(bool selected, GameObject item)
    {
        if (selected) { selectedItem = item; }
        else { selectedItem = null; }
    }
    public GameObject getSelectedItemToPlace()
    { return selectedItem; }
    #endregion
    #region InternalMethods
    private void checkEnemyShipsRange()
    {
        enemyShips.ForEach(delegate (GameObject enShip)
        {
            if (enShip != null)
            {
                var heading = enShip.transform.position - transform.position;
                /*  TO DO :
                    if X is positive it means we are on right side of planet otherwise left
                    if Y is positive it means we are on top side of planet otherwise down
               */
                var sqrMag = heading.sqrMagnitude;
                if (heading.x == 0 && heading.y > 0)
                {
                    // top side
                    clocks[10].GetComponent<TileClockManager>().checkRange(sqrMag, enShip);
                    clocks[11].GetComponent<TileClockManager>().checkRange(sqrMag, enShip);
                    clocks[0].GetComponent<TileClockManager>().checkRange(sqrMag, enShip);
                    clocks[1].GetComponent<TileClockManager>().checkRange(sqrMag, enShip);
                }
                else if (heading.x == 0 && heading.y < 0)
                {
                    // bottom side
                    clocks[4].GetComponent<TileClockManager>().checkRange(sqrMag, enShip);
                    clocks[5].GetComponent<TileClockManager>().checkRange(sqrMag, enShip);
                    clocks[6].GetComponent<TileClockManager>().checkRange(sqrMag, enShip);
                    clocks[7].GetComponent<TileClockManager>().checkRange(sqrMag, enShip);
                }
                else if (heading.y == 0 && heading.x < 0)
                {
                    // left side
                    clocks[7].GetComponent<TileClockManager>().checkRange(sqrMag, enShip);
                    clocks[8].GetComponent<TileClockManager>().checkRange(sqrMag, enShip);
                    clocks[9].GetComponent<TileClockManager>().checkRange(sqrMag, enShip);
                    clocks[10].GetComponent<TileClockManager>().checkRange(sqrMag, enShip);
                }
                else if (heading.y == 0 && heading.x > 0)
                {
                    // right side
                    clocks[1].GetComponent<TileClockManager>().checkRange(sqrMag, enShip);
                    clocks[2].GetComponent<TileClockManager>().checkRange(sqrMag, enShip);
                    clocks[3].GetComponent<TileClockManager>().checkRange(sqrMag, enShip);
                    clocks[4].GetComponent<TileClockManager>().checkRange(sqrMag, enShip);
                }
                else
                {
                    int cVal = 0;
                    int cRange = 11;
                    // top right 
                    if (heading.x > 0 && heading.y > 0)
                    {
                        cRange = 3;
                    }
                    // bottom right 
                    else if (heading.x > 0 && heading.y < 0)
                    {
                        cVal = 2;
                        cRange = 5;
                    }
                    // bottom left
                    else if (heading.x < 0 && heading.y < 0)
                    {
                        cVal = 6;
                        cRange = 9;
                    }
                    // top left
                    else if (heading.x < 0 && heading.y > 0)
                    {
                        cVal = 8;
                        cRange = 11;
                    }
                    for (int c = cVal; c <= cRange; c++)
                    {

                        clocks[c].GetComponent<TileClockManager>().checkRange(sqrMag, enShip);

                    }
                }
            }
            else
            {

            }
  
        });
    }

    private void checkHealth()
    {
        if (health <= 0f)
        {
            Debug.Log("Game OVER");
            Object.Destroy(gameObject);
        }
    }
    #endregion
}
