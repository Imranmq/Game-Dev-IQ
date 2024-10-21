using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovementScroller : MonoBehaviour
{
   public float paralexEffect = 5f;
    void Update()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mat = mr.material;
        Vector2 offsetVal = mat.mainTextureOffset;
        offsetVal.x = transform.position.x / transform.localScale.x / paralexEffect;
        offsetVal.y = transform.position.y / transform.localScale.y / paralexEffect;
        mat.mainTextureOffset = offsetVal; 
    }
}
