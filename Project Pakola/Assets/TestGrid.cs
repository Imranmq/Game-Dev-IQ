using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour
{
    Grid grid;
    Grid agrid;
    Grid bgrid;
    Grid room;
    public GameObject littleHamzo;
    public GameObject Player;
    public List<GameObject> floorTiles;
    private int roomRows = 5;
    private int roomCols =50;
    private float roomCellSize = 1;
    private Vector3 roomPos = new Vector3(0f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        //grid = new Grid(4, 3, 10f , new Vector3(20f,0f));
        //agrid = new Grid(1, 2, 5f, new Vector3(-20f, 0f));
        //bgrid = new Grid(5, 5, 20f, new Vector3(0f, 50f));
        gridGen(roomCols, roomRows, roomCellSize,roomPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //int val =  gridClick(grid);
            //if(val < 0) val = gridClick(agrid);
            //if (val < 0) val = gridClick(bgrid);



        }
    }
    int gridClick(Grid grid)
    {
        int val = grid.GetValue(AstropolyUtils.GetMouseWorldPosition());
        if (val > -1)
        {
            GameObject gameObjectExist = grid.GetGameObjOnGridCell(AstropolyUtils.GetMouseWorldPosition());
            GameObject newlh = null;
            if (gameObjectExist == null) newlh = Instantiate(littleHamzo, AstropolyUtils.GetMouseWorldPosition(), Quaternion.identity);
            grid.SetValue(AstropolyUtils.GetMouseWorldPosition(), val + 1, newlh);
        }
        return val;
    }

    void gridGen(int cols, int rows, float  roomCellSize, Vector3 startPos)
    {
        room = new Grid(cols,rows,  roomCellSize, startPos);
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector3 pos = room.GetWolrdPosCenter(x, y);
                GameObject gm = null;
                GameObject tile = null;
                if (AstropolyUtils.checkCenter(x, y, rows, cols)) { gm = floorTiles[4]; }
                else if (AstropolyUtils.checkTopCenter(x, y, rows, cols)) { gm = floorTiles[1]; }
                else if (AstropolyUtils.checkBottomCenter(x, y, rows, cols)) { gm = floorTiles[7]; }
                else if (AstropolyUtils.checkTopLeft(x, y, rows, cols)) { gm = floorTiles[0]; }
                else if (AstropolyUtils.checkTopRight(x, y, rows, cols)) { gm = floorTiles[2]; }
                else if (AstropolyUtils.checkCenterLeft(x, y, rows, cols)) { gm = floorTiles[3]; }
                else if (AstropolyUtils.checkCenterRight(x, y, rows, cols)) { gm = floorTiles[5]; }
                else if (AstropolyUtils.checkBottomLeft(x, y, rows, cols)) { gm = floorTiles[6]; }
                else if (AstropolyUtils.checkBottomRight(x, y, rows, cols)) { gm = floorTiles[8]; }

                if (gm != null) tile = Instantiate(gm, pos, Quaternion.identity);
                if (tile != null) { room.SetValue(x, y, 1, tile); }



            }

        }
        SetRoomGridToPlayer(room);

    }
   public Grid GetRoomGrid()
    {
        return room;
    }

    public void SetRoomGridToPlayer(Grid g)
    {
        if(Player != null)
        {
            Player.GetComponent<PlayerGridManager>().setRoomGrid(g);
            Player.GetComponent<PlayerManager>().setRoomGrid(g);
        }
    }
}