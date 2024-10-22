using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 0.5f, 0);
    public GameObject bulletObject;
    public float fireDelay = 0.30f;
    float coolDownShootTime = 0;
    int bulletLayer;
    public Transform playerObj;
    void Start()
    {
        bulletLayer = gameObject.layer;   
    }
    // Update is called once per frame
    void Update()
    {
        if (playerObj == null)
        {
            GameObject go = GameObject.Find("PlayerShip");
            if (go != null)
            {
                playerObj = go.transform;
            }
        }

        coolDownShootTime -= Time.deltaTime;
        if ( coolDownShootTime <= 0 && playerObj != null && Vector3.Distance(transform.position, playerObj.position)<4)
        {
            Debug.Log("SHOOTING FIRE");
            coolDownShootTime = fireDelay;
            Vector3 offsetBullet = transform.rotation * offset;
            
            GameObject bulletObj = (GameObject)Instantiate(bulletObject, transform.position, transform.rotation);
            bulletObj.layer = bulletLayer;
        }
    }
}
