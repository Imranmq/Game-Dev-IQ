using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSelfDestroy : MonoBehaviour
{    
    private void OnTriggerEnter2D()
    {
        Destroy(gameObject);
    }
}
