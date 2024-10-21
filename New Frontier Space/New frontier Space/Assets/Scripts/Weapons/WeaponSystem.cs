using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour    
{
   
    public GameObject Laser;
    public int weaponType = 1; // player weapons type : starting Mining Laser

    private float weaponFireDelay = 0f;
    private bool laserShooting = false;

    private GameObject shootingLaser;
  
    public void ShootWeapon()
    {
        // weapon Type 1 is Laser
        if (weaponType == 1) {
            laserShooting = !laserShooting;
            if (laserShooting == true)
            {
                Vector3 offset = new Vector3(0, 0.5f, 0);
                Vector3 offsetBullet = transform.rotation * offset;
                shootingLaser =Instantiate(Laser, transform.position, transform.rotation);
               
                        
            }
            else
            {
                if (shootingLaser)
                {
                    Destroy(shootingLaser);
                }
            }
        }
    }

    public void StopShooting()
    {
        laserShooting = false;
        if (shootingLaser)
        {
            Destroy(shootingLaser);
            
        }
    }

}
