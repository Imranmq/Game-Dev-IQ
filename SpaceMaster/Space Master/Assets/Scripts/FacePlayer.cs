using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public Transform playerObj;
    public float rotSpeed = 135f;
  
    void Update()
    {
        if (playerObj == null)
        {
            GameObject go = GameObject.Find("PlayerShip");
            if(go != null)
            {
                playerObj = go.transform;
            }
        }

        // Found the playerObj or it doesnt exist
        // if still player not found then return , do not execute rest of code
        if (playerObj == null) return;
          
        // This are executes now when playerObj is found

        Vector3 dir = playerObj.position - transform.position;
        dir.Normalize();

        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        Quaternion desiredRotation =  Quaternion.Euler(0, 0, zAngle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotSpeed * Time.deltaTime);
        
    }
}
