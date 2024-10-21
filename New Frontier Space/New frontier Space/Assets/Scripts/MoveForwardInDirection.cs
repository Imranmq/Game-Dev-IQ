using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardInDirection : MonoBehaviour
{
    public Transform playerobj;
    public float rotSpeed = 1f;
    float moveSpeed = 1;
    public float minSpeed = 0.25f;
    public float maxSpeed = 0.50f;
    bool turnedTowardsPlayerCheck = false;
    private void Start()
    {
        moveSpeed = Random.Range(minSpeed, maxSpeed);
        transform.localScale = new Vector3(0.75f, 0.75f, 1);
      
    }
    void Update()
    {
        if (playerobj == null)
        {
            GameObject go = GameObject.Find("PlayerShip");
            if (go != null)
            {
                playerobj = go.transform;
            }
        }

        // found the playerobj or it doesnt exist
        // if still player not found then return , do not execute rest of code
        if (playerobj == null) return;
        // check if we have turned towards player
        if (!turnedTowardsPlayerCheck)
        {
            //turnTowardsPlayer();
            TakeDirection();
            turnedTowardsPlayerCheck = true;
        }
        // Move Forward
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0, moveSpeed * Time.deltaTime, 0);
        // Multiply rotation to forward direction(velocity) in that order (quaternion * vector)
        pos += transform.rotation * velocity;
        transform.position = pos;

        Vector3 CamPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + 100, -100, +10));
        if(transform.position.x > CamPoint.x || transform.position.y < CamPoint.y)
        {
            Destroy(gameObject);
        }
    }
    
    // NOT IN USE
    void turnTowardsPlayer()
    {
        Vector3 dir = playerobj.position - transform.position;
        dir.Normalize();

        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - (90 + Random.Range(-45 ,45));
        Quaternion desiredRotation = Quaternion.Euler(0, 0, zAngle);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotSpeed);
    }

    void TakeDirection()
    {
        float randomRotation = Random.Range(-130f, -160f);
        transform.eulerAngles = new Vector3(0, 0, randomRotation);
    }
    
}
