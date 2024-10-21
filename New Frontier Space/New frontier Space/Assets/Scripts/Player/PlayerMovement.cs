using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float rotationSpeed = 100f;


    PlayerStructure player;
    private float thrusterTimerFrame = 0.1f;
    private float thrusterTimerFramLimit = 0.1f;
    private float tempThrustVal = 0f;

    // Thrust bar Game Object
    private GameObject ThrustBar;
    private GameObject FuelBar;

    private void Start()
    {
        ThrustBar = GameObject.Find("ThrustBarValue");
        FuelBar = GameObject.Find("FuelBarCom");
    }   

    public void SetPlayerClass (PlayerStructure playerClass)
    {
        player = playerClass;
    }


    //public void CalculateFuelChange(float fuelConsumed)
    //{

    //    fuelValue = fuelValue - fuelConsumed;
    //    if (fuelValue < 1)
    //    {
    //        gameObject.GetComponent<PlayerMovement>().SetFuelBool(false);
    //    }
    //    float newFuelPercentage = fuelValue / fuelTank;
    //    FuelBarObjectScript.SetValueOfFuelBar(newFuelPercentage);
    //}

    // Update is called once per frame
    void Update()
    {
        thrusterTimerFrame = thrusterTimerFrame - Time.deltaTime;

        // Check if we have reached beyond 2 seconds.
        // Subtracting two is more accurate over time than resetting to zero.
       
       
        if (player != null)
        {
            if (thrusterTimerFrame <= 0)
            {
                thrusterTimerFrame = thrusterTimerFramLimit;

                if (Input.GetKey("x"))
                {
                    player.PlayerShip.ThrustChange(1);
                    tempThrustVal = player.PlayerShip.GetTempThrustVal();
                }
                if (Input.GetKey("z"))
                {
                    player.PlayerShip.ThrustChange(0);
                    tempThrustVal = player.PlayerShip.GetTempThrustVal();
                }
                if (ThrustBar)
                {
                    ThrustBar.GetComponent<ValueToBarsImage>().SetValueOfDashboardBar(tempThrustVal / 100);
                }
            }

            float translation = player.PlayerShip.GetPositionTranslation();
            //float translation = Input.GetAxis("Vertical") * speed;
            float rotation = player.PlayerShip.GetRotationTranslation(Input.GetAxis("Horizontal"));           
            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;
            transform.Translate(0, translation, 0);
            transform.Rotate(0, 0, rotation);

            player.PlayerShip.SetPlayerPosition(transform.position);
            // NOT DISCUSSED FIRE SYSTEM
            if (Input.GetButton("Fire1"))
            {
                //  Debug.Log("SHOOTING FIRE");
                gameObject.GetComponent<WeaponSystem>().ShootWeapon();
            }
            else
            {
                gameObject.GetComponent<WeaponSystem>().StopShooting();
            }
        }
       
    }

}
