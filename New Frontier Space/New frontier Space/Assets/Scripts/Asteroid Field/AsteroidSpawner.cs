using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject entityToSpawn;
    public float spawnRate = 0.025f;
    float nextEntityTime = 1f;
    float spawnDistance = 16f; // Not used
    int entityCount = 0;
    int oreGenerationPercentage = 10;
    bool startGeneratingOres = false;
    public int MaxEntities;

    private void Start()
    {
        GameObject mainLogicObj = GameObject.FindWithTag("MainGameObject");
        if (mainLogicObj != null) {
            oreGenerationPercentage = mainLogicObj.GetComponent<MainGameLogic>().GetOreGeneration();
            Debug.Log("Ore Generation Percentage : " + oreGenerationPercentage);
            startGeneratingOres = true;
        }
        else
        {
            Debug.Log("Main Game Object Not Found in AsteroidSpawner Start method");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startGeneratingOres == true)
        {
            nextEntityTime -= Time.deltaTime;
            if (nextEntityTime <= 0 && entityCount < MaxEntities)
            {
                nextEntityTime = spawnRate;
                //Vector3 offset = Random.onUnitSphere;
                //offset.z = 0;
                //offset = offset.normalized * spawnDistance;
                Vector3 tmpPos = Camera.main.WorldToScreenPoint(transform.position);
                if (tmpPos.x > Screen.width)
                {
                    Debug.Log("X > Width");                    
                }
                int minVal = -300;
                int maxVal = Screen.width - 100;
                int newXPos = Random.Range(minVal , maxVal);
                Vector3 newPos = new Vector3(newXPos ,0, 0);
                Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(newXPos, Screen.height + 50, +10));
                transform.position = point;                                
                Instantiate(entityToSpawn, transform.position, Quaternion.identity);
                entityCount++;
            }
        }
    }
}
