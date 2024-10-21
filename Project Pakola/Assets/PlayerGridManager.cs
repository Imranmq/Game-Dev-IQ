using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGridManager : MonoBehaviour
{
    public GameObject GridManager;
    Grid roomGrid;
    private int gridPosX;
    private int gridPosY;
   
    public void Start()
    {
        if(GridManager != null && GridManager.GetComponent<TestGrid>() != null)
        {
            roomGrid = GridManager.GetComponent<TestGrid>().GetRoomGrid();            
        }
    }
    public void setRoomGrid(Grid g) {
        roomGrid = g;
    }
    public void playerMoveGridCheck()
    {
        if (AstropolyUtils.CheckForNotNull(roomGrid))
        {
        
            int[] gridXY = roomGrid.GetXYValue(transform.position);          
            if(gridXY != null)
            {
                gridPosX = gridXY[0];
                gridPosY = gridXY[1];
                Debug.Log("X : " + gridPosX + " , Y : " + gridPosY);
            }

        };
    }
    public int getGridPositionX()
    {
        return gridPosX;
    }
    public int getGridPositionY()
    {
        return gridPosY;
    }
    
}
