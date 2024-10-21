using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthScript : MonoBehaviour
{
    [SerializeField]
    float health = 50f ;

    void Update()
    {
      
        checkHealth();

    }
    public void YouGotHit(float dmg)
    {
        health -= dmg;
    }

    public float getHealth()
    {
        return health;
    }

    private void checkHealth()
    {
        if (health < 0f)
        {
            Destroy(gameObject);
        }
    }
}
