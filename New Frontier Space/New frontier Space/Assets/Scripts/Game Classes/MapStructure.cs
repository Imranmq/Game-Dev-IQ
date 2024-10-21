using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStructure
{ 
    private List<MapObject> spaceUniverseMap = new List<MapObject>() { new MapObject("Earth",0,0,"New Hope")};

    public List<MapObject> GetMapUniverse()
    {
        return spaceUniverseMap;
    }
}

public class MapObject
{
    string sectorName;
    float xMapPos;
    float yMapPos;
    List<string> stations = new List<string>();    

    public MapObject(string sectorName, float xMapPos, float yMapPos , string station)
    {
        this.sectorName = sectorName;
        this.xMapPos = xMapPos;
        this.yMapPos = yMapPos;
        this.stations.Add(station);
    }
}
