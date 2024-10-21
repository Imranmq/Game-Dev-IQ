using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipments 
{
    private List<Equipment> equipments = new List<Equipment>();

    public void LoadEquipments()
    {
        Sprite sprGHook = Resources.Load<Sprite>("Sprites/Equipments/grapleGunpng");
        Debug.Log(sprGHook);
        Equipment grapHook = new Equipment("Grappling Hook", sprGHook);
      
        equipments.Add(grapHook);
    }
    public List<Equipment> getEquipments()
    {
        return equipments;
    }
    public Equipment getSingleEquipment(string name)
    {
        Equipment eq = equipments.Find(equip => equip.name == name);
        return eq;
    }
}


public class Equipment
{
    public Sprite equipSprite { get; private set; }
    public string name { get; private set; }

    public Equipment(string givenName  , Sprite sprite){
        name = givenName;
        equipSprite = sprite;    
    }
 
}