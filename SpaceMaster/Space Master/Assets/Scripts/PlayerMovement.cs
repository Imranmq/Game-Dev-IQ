using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool upKeyPressing;
    private bool downKeyPressing;
    private bool leftKeyPressing;
    private bool rightKeyPressing;

    public float maxSpeed = 5.5f;
    public float rotSpeed = 180f;
    float shipBoundaryRadius = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       

        // Rotate
        
        // Get rotation quaternion
        Quaternion rot = transform.rotation;
        
        // Get z euler angle
        float z = rot.eulerAngles.z;
        
        // Change the z angle based on input
        z -= Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        
        // Recreate Quaternian euler angle;
        rot = Quaternion.Euler(0,0,z);
        
        // Set Rotation quaternion to new value
        transform.rotation = rot;

        // MOVE
        Vector3 pos = transform.position;   
        Vector3 velocity = new Vector3(0,Input.GetAxis("Vertical") * maxSpeed * Time.deltaTime,0);
        // Multiply rotation to forward direction(velocity) in that order (quaternion * vector)
        pos += rot * velocity;
        // Limit the ship movement inside the camera screen
        //Vertical Limit
        if(pos.y + shipBoundaryRadius> Camera.main.orthographicSize)
        {
            pos.y = Camera.main.orthographicSize - shipBoundaryRadius;
        }
        if (pos.y - shipBoundaryRadius < -Camera.main.orthographicSize)
        {
            pos.y = -Camera.main.orthographicSize + shipBoundaryRadius;
        }            
        // Calculating Screen ratio for width dynamically
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float widthOrtho = Camera.main.orthographicSize * screenRatio;
        //Horizontal Limit
        if (pos.x + shipBoundaryRadius > widthOrtho)
        {
            pos.x = widthOrtho - shipBoundaryRadius;
        }
        if (pos.x - shipBoundaryRadius < -widthOrtho)
        {
            pos.x = -widthOrtho + shipBoundaryRadius;
        }
        transform.position = pos;

    }
}
