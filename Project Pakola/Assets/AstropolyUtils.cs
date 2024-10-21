using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AstropolyUtils
{
    public static TextMesh CreateTextOnWolrd(string text, Color color, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, TextAnchor textAnchor = TextAnchor.MiddleCenter, TextAlignment textAlignment = TextAlignment.Center, int sortingOrder = 5000)
    {
        GameObject gameObject = new GameObject("Text_World", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vector = GetMOuseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vector.z = 0f;
        return vector;
    }
    public static Vector3 GetMOuseWorldPositionWithZ()
    {
        return GetMOuseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMOuseWorldPositionWithZ(Camera worldCam)
    {
        return GetMOuseWorldPositionWithZ(Input.mousePosition, worldCam);
    }
    public static Vector3 GetMOuseWorldPositionWithZ(Vector3 screenPos, Camera worldCam)
    {
        Vector3 wPos = worldCam.ScreenToWorldPoint(screenPos);
        return wPos;
    }

    public static bool checkTopLeft(int x, int y, int rows, int cols)
    {
        bool value = false;
        if (x == 0 && y == rows -1) value = true;
        return value;
    }
    public static bool checkTopCenter(int x, int y, int rows, int cols)
    {
        bool value = false;
        if (x > 0 && x < cols -1 && y == rows-1) value = true;
        return value;
    }
    public static bool checkTopRight(int x, int y, int rows, int cols)
    {
        bool value = false;
        if (x == cols -1 && y == rows -1) value = true;
        return value;
    }
    public static bool checkCenterLeft(int x, int y, int rows, int cols)
    {
        bool value = false;
        if (x == 0 && y > 0 && y < rows -1 ) value = true;
        return value;
    }
    public static bool checkCenter(int x, int y, int rows, int cols)
    {
        bool value = false;
        if (x > 0 && x < cols -1  && y > 0 && y < rows -1 ) value = true;
        return value;
    }
    public static bool checkCenterRight(int x, int y, int rows, int cols)
    {
        bool value = false;
        if (x == cols -1 && y > 0 && y < rows -1) value = true;
        return value;
    }
    public static bool checkBottomLeft(int x, int y, int rows, int cols)
    {
        bool value = false;    
        if (x == 0 && y == 0) value = true;

        return value;
    }
    public static bool checkBottomCenter(int x, int y, int rows, int cols)
    {
        bool value = false;
        if (x > 0 && x < cols -1 && y == 0 ) value = true;
        return value;
    }
    public static bool checkBottomRight(int x, int y, int rows, int cols)
    {
        bool value = false;
        if (x == cols -1 && y == 0) value = true;
        return value;
    }
    public static bool CheckForNotNull<T>(T arg)
    {
        if(arg == null)
        {
            return false;
        }else
        {
            return true;
        }
    }
    
}
