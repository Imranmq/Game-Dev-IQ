using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    public float invulduration = 0f;
    public int health = 2;
    float invulTimer = 0;
    int correctLayer;

    private void Start()
    {
        correctLayer = gameObject.layer;
        if (gameObject.name == "PlayerShip")
        {
            bool healthsetter = GameObject.Find("PlayerSpawner").GetComponent<PlayerSpawner>().setHealth(health);
        }
        

    }
    private void OnTriggerEnter2D()
    {
        Debug.Log("Trigger");
        
        health--;
        if (gameObject.name == "PlayerShip")
        {
            bool healthDecreaser = GameObject.Find("PlayerSpawner").GetComponent<PlayerSpawner>().setHealth(health);
        }
        invulTimer = invulduration;
        gameObject.layer = 10;                
        if(health <= 0)
        {
            Die();
        } 
    }
    private void Update()
    {
        invulTimer -= Time.deltaTime;
        if(invulTimer <= 0)
        {
            gameObject.layer = correctLayer;
        }
    }
    void Die()
    {
        float enemyCountAfterDestroying = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().EnemycountDecrease();
        Destroy(gameObject);
    }
}
