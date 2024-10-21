using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerGridManager pGm;
    Grid roomGrid;
    private void Start()
    {
        if(AstropolyUtils.CheckForNotNull(pGm))
        {
            pGm = gameObject.GetComponent<PlayerGridManager>();
        }
    }
    private void Update()
    {
        if (Input.GetButton("Fire1")){
            InteractButtonClicked();
        }
    }
    private void InteractButtonClicked()
    {
        if(AstropolyUtils.CheckForNotNull(pGm))
        {
            int gX = pGm.getGridPositionX();
            int gY = pGm.getGridPositionY();
            // Logic Todo : check the facing position of the player then adjust the grid position to place item on. 
            //"Assuming it is always facing toward its left"
            int placeGridX = gX - 1;
            int placeGridY = gY;

            GameObject gTile = roomGrid.GetGameObjectOnGrid(placeGridX, placeGridY);
            if (AstropolyUtils.CheckForNotNull(gTile))
            {
                TileManagerScript gTileScript = gTile.GetComponent<TileManagerScript>();

            }
        }
    }
       public void setRoomGrid(Grid g) {
        roomGrid = g;
    }
}
