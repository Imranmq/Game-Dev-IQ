using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 0.5f, 0);
    public GameObject bulletObject; 
    public float fireDelay = 0.25f;
    float coolDownShootTime = 0;
    // Update is called once per frame
    void Update()
    {
        coolDownShootTime -= Time.deltaTime;
        if (Input.GetButton("Fire1") && coolDownShootTime <=0) {
            //Debug.Log("SHOOTING FIRE");
            coolDownShootTime = fireDelay;
            Vector3 offsetBullet = transform.rotation * offset;
            Instantiate(bulletObject, transform.position, transform.rotation);
        }
    }
}
