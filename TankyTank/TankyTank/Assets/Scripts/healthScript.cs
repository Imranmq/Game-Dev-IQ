using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthScript : MonoBehaviour
{
    [SerializeField]
    public float health = 50f;
    private bool disableonEnd= true;
    void Update()
    {
      
        checkHealth();

    }
    public void YouGotHit(float dmg)
    {
        health -= dmg;
        Debug.Log("Dmg: " + dmg);
    }

    public float getHealth()
    {
        return health;
    }

    private void checkHealth()
    {
        if (health <= 0f)
        {
            if (disableonEnd) {
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
