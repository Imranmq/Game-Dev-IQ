using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomConstantMovement : MonoBehaviour
{
    public float speed = 10.0f;
   
   
   
    void Update()
    {
        float translation =  speed;
     

        translation *= Time.deltaTime;
       

        transform.Translate(0, translation, 0);
     
    }
}
