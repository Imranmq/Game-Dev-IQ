using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentStructure
{
    private List<Attachment> attachments = new List<Attachment>() { new MiningLaser("Mining Laser",1 , 5f , "T1"  )
        ,new Thruster("Attachments/Engines","engine-01",500f ,"T1")};

    public List<Attachment> GetAttachmentsList()
    {
      
        return attachments;
    }
}

public class Attachment
{
    public string folderName;
    public string fileName;
    int positionX = 0;
    int positionY = 0;
    int positionNumber = 0;
    HardPoints onHardPoint;

    public HardPoints GetHardPoint()
    {
        return onHardPoint;
    }
    public void SetHardPoint(HardPoints hardpoint)
    {
        onHardPoint = hardpoint;
    }
}

public class WeaponObject : Attachment
{
          
}


public class MiningLaser : WeaponObject
{
    public string weaponName { get; set; }
    public int weaponType { get; set; }
    float weaponDmg;
    public string tier { get; set; }

    public MiningLaser(string weaponName, int weaponType, float weaponDmg , string tier)
    {       
        this.weaponName = weaponName;
        this.weaponType = weaponType;
        this.weaponDmg = weaponDmg;
        this.tier = tier;     

    }

}

public class Thruster : Attachment
{
    public float thrust { get; set; }
    public string tier { get; set; }

    public Thruster(string folderName, string fileName, float thrust , string tier)
    {
        this.folderName = folderName;
        this.fileName = fileName;
        this.thrust = thrust;
        this.tier = tier;
    }

}


public class HardPoints
{

    float xPos = 0f;
    float yPos = 0f;
}