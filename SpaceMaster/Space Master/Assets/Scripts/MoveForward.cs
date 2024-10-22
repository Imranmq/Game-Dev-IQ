using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float maxSpeed = 5;
        // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime, 0);
        // Multiply rotation to forward direction(velocity) in that order (quaternion * vector)
        pos += transform.rotation * velocity;
        transform.position = pos;

    }
}
