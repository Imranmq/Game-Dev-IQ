using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    float enemyRate = 5;
    float nextEnemy = 1;
    float spawnDistance = 12f;
    int enemyCount = 0;
    int score = 0;
    // Update is called once per frame
    void Update()
    {
        nextEnemy -= Time.deltaTime;
        if(nextEnemy <= 0 && enemyCount < 15)
        {
            nextEnemy = enemyRate;
           
            if(enemyRate < 1.5)
            {
                enemyRate = 1.5f;
            }

            Vector3 offset = Random.onUnitSphere;
            offset.z = 0;
            offset = offset.normalized * spawnDistance;

            Instantiate(enemyPrefab, transform.position + offset, Quaternion.identity);
            enemyCount++;
        }

    }
    public float EnemycountDecrease()
    {
        enemyCount--;
        score++;
        return enemyCount;
    }
    private void OnGUI()
    {        
        GUI.Label(new Rect(Screen.width - 150, 0, 150, 75), "Current Enemies: " + enemyCount);
        GUI.Label(new Rect(Screen.width / 2 - 100, 0, 100, 100), "Kills: " + score);
    }

}
