using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularSpawnerTesting : MonoBehaviour
{
    public GameObject circlularShapes;
    public int pieceCount = 12;
    public float radius = 2f;
    
    void Awake()
    {
        InstantiateCircle();
    }

    void InstantiateCircle()
    {
        float angle = 360f / (float)pieceCount;
  
        for (int i = 0; i < pieceCount; i++)
        {
            Quaternion rotation = Quaternion.AngleAxis(i * angle, new Vector3(0f, 0f, 1f));
            Vector3 direction = rotation * Vector3.up;

            Vector3 position = transform.position + (direction * radius);
           GameObject obj = Instantiate(circlularShapes, position,Quaternion.identity);
            RotateAroundTarget rtService =obj.GetComponent<RotateAroundTarget>();
            rtService.setCenter(transform.position);
            rtService.setCurrentAngleDegree(i *angle);
            rtService.setText("C", i);
            obj.transform.localScale = new Vector2(radius, radius);
        }
    }
}
