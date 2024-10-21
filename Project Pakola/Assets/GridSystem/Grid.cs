using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private int[,] gridArray;
    private GameObject[,] gameObjArray;
    private TextMesh[,] debugTextArray;
    private bool showTextGrid = false;
    public Grid(int width, int height, float cellSize , Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new int[width, height];
        gameObjArray = new GameObject[width, height];
        debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                //Debug.Log(x + ", " + y);
                if (showTextGrid)
                {
                    debugTextArray[x, y] = AstropolyUtils.CreateTextOnWolrd(gridArray[x, y].ToString(), Color.white, null, GetWolrdPos(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 20);
                    Debug.DrawLine(GetWolrdPos(x, y), GetWolrdPos(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(GetWolrdPos(x, y), GetWolrdPos(x + 1, y), Color.white, 100f);
                }

            }
        }
        if (showTextGrid)
        {
            Debug.DrawLine(GetWolrdPos(0, height), GetWolrdPos(width, height), Color.white, 100f);
            Debug.DrawLine(GetWolrdPos(width, 0), GetWolrdPos(width, height), Color.white, 100f);
        }

        // SetValue(2, 1, 56);
    }
    public Vector3 GetWolrdPos(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }
    public Vector3 GetWolrdPosCenter(int x, int y)
    {
        return GetWolrdPos(x, y) + new Vector3(cellSize, cellSize) * 0.5f + originPosition;
    }
    public void GetScreenPos(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);

    }

    public void SetValue(int x, int y, int value, GameObject go)
    {
        if (x < 0 || y < 0 || x > width || y > height)
        {
            Debug.Log("Not a valid grid cell for placement");
        }
        else
        {
            gridArray[x, y] = value;
            if (go != null) gameObjArray[x, y] = go;
            if(showTextGrid) debugTextArray[x, y].text = gridArray[x, y].ToString();
        }

    }
    public void SetValue(Vector3 worldPos, int value, GameObject goValue)
    {
        int x, y;
        GetScreenPos(worldPos, out x, out y);

        SetValue(x, y, value, goValue);
    }

    public int GetValue(int x, int y)
    {
        if (x < 0 || y < 0 || x > width || y > height)
        {
            Debug.Log("Not a valid grid cell provided");
            return -1;
        }
        else
        {
            return gridArray[x, y];
        }
    }
    public GameObject GetGameObjectOnGrid(int x, int y)
    {
        if (x < 0 || y < 0 || x > width || y > height)
        {
            Debug.Log("Not a valid grid cell provided");
            return null;
        }
        else
        {
            return gameObjArray[x, y];
        }
    }
    public GameObject GetGameObjectOnGrid(Vector3 wolrdPos)
    {
        int x, y;
        GetScreenPos(wolrdPos, out x, out y);
        return GetGameObjectOnGrid(x, y);
    }
    public int GetValue(Vector3 wolrdPos)
    {
        int x, y;
        GetScreenPos(wolrdPos, out x, out y);
        return GetValue(x, y);
    }
    public int[] GetXYValue(Vector3 worldPos)
    {

        int[] array = new int[2];
        int x, y;
        GetScreenPos(worldPos, out x, out y);
        if (x < 0 || y < 0 || x > width || y > height)
        {
            Debug.Log("Out of Grid Range Vector3 world position provided");
            return null;
        }
        else
        {
            array[0] = x;
            array[1] = y;
            return array;
        }
    }
    public GameObject GetGameObjOnGridCell(Vector3 wolrdPos)
    {
        int x, y;
        GetScreenPos(wolrdPos, out x, out y);
        return GetGameObjOnGridCell(x, y);
    }
    public GameObject GetGameObjOnGridCell(int x, int y)
    {
        if (x < 0 || y < 0 || x > width || y > height)
        {
            Debug.Log("Not a valid grid cell provided");
            return null;
        }
        else
        {           
            return gameObjArray[x, y] ;
        }
    }
}
