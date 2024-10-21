using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    float yRotation = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        // Print the rotation around the global X Axis
        print(transform.eulerAngles.x);
        // Print the rotation around the global Y Axis
        print(transform.eulerAngles.y);
        // Print the rotation around the global Z Axis
        print(transform.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        yRotation += Input.GetAxis("Horizontal");
        transform.eulerAngles = new Vector3(0, 0, -yRotation);
    }
}
