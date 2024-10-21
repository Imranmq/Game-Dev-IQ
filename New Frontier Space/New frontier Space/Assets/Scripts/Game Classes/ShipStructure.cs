using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStructure
{
    // Hard points where ship attachment will be placed.
    public List<HardPoints> hardPoints = new List<HardPoints>();
    // Attachments that the ship has in inventory ( if hardpoints ref in any attachment is available it means its in use at that point)
    public List<Attachment> playerAttachment { get; set; } = new List<Attachment>();
    // fuel Value and Fuel tank. To Do : Put the Fuel tank a shape block.
    public float fuelValue = 1000f;
    public float fuelTank = 1000f;
    // Ship X ,Y ,Z position.
    private float positionX = 10f;
    private float positionY = 10f;
    private float positionZ = 0;
    // Max modules of each type the ship can have. 
    public int maxMiningLaser { get; set; } = 2;
    public int maxBoosters { get; set; } = 2;
    private float thrust = 0f;
    private float thrustIncrease = 2f;
    private float defaultThrustIncrease = 2f;    
    private float thrustValue = 20f;  //speed 
    private float turnValue = 100f;

    // Ship game object
    private GameObject shipGameobject;
    
    public Vector3 GetShipPositions()
    {
        Vector3 shipPos = new Vector3(positionX, positionY, positionZ);
        return shipPos;
    }
    public void ThrustChange(int direction) {
        //  0 is decrease thrust
        if (direction == 0 )
        {
            if (thrust >= thrustIncrease)
            {
                thrust = thrust - thrustIncrease;
                thrustIncrease = thrustIncrease - (thrustIncrease * 0.1f);
            }
            else
            {
                thrustIncrease = defaultThrustIncrease;
                thrust = 0f;
            }
        }
        //  1 is Increase thrust
        else
        {
            if (thrust < (100f - thrustIncrease))
            {
                thrust = thrust + thrustIncrease;
                thrustIncrease = thrustIncrease + (thrustIncrease * 0.1f);
            }
            else
            {
                thrust = 100f;
            }
        }
      
    }
    // The speed with which to move ahead
    public float GetPositionTranslation()
    {

        return (thrust / 100) * thrustValue;
    }
    // Rotation Translation Value
    public float GetRotationTranslation(float rotationInput)
    {
        return rotationInput * turnValue;
    }
    // SYNC ship position with the ship game object
    public void SetPlayerPosition(Vector3 position)
    {
        positionX = position.x;
        positionY = position.y;
        positionZ = position.z;
}
    // GET THRUSTER VALUE FOR THRUSTER GUI
    public float GetTempThrustVal() { return thrust; }

    // SET SHIP GAME OBJECT 
    public void SetShipGameObject(GameObject ship)
    {
       shipGameobject = ship;
    }

    // ADD attachment to the ship
    public void AddAttachment(Attachment attachment)
    {
        playerAttachment.Add(attachment);
    }

    public void AddHardPoints(List <HardPoints> hardpoints)
    {
        foreach(var hardpoint in hardpoints)
        {
            hardPoints.Add(hardpoint);
        }        
    }
}

