using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnerService : MonoBehaviour
{
    enum SpawnPosition
    {
        Top,
        Right,
        Bottom,
        Left
    }
    #region variables
    [SerializeField]
    private GameObject planet;
    [SerializeField]
    private List<GameObject> typeOfAsteroids = new List<GameObject>();
    private List<GameObject> spawnedAsteroid = new List<GameObject>();

    private float spawnTimer = 3f;
    [SerializeField]
    private float spawnTime = 5f;   
    #endregion
    #region  next spawn object related
    private GameObject nextToSpawn = null;
    private Vector3 positionOfSpawningAsteroid;
    private float speedOfAsteroid = 3f;
    [SerializeField]
    private float asteroidSpeedRangeMin = 3f;
    [SerializeField]
    private float asteroidSpeedRangeMax = 6f;
    SpawnPosition sposition = SpawnPosition.Top;
    [SerializeField]
    private int numberOfSpawnsBeforeChangingPos = 5;
    private int currentSpawnsInPos = 0;
    private Camera cam;
    Vector3 fullPoint;
    Vector3 zeroPoint;
    [SerializeField]
    int offsetToOutsideCameraPos;
    #endregion

    #region method
    private void Start()
    {
        if (typeOfAsteroids.Count == 0)
        {
            Debug.Log("NO Asteroid type assigned To spawner");
        }
        cam = Camera.main;
        spawnTimer = spawnTime;
     
        //Debug.Log("pixelHeight" + cam.pixelHeight);
        //Debug.Log("pixelWidth" + cam.pixelWidth);
        fullPoint = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth + offsetToOutsideCameraPos, cam.pixelHeight + offsetToOutsideCameraPos, Camera.main.nearClipPlane));
        zeroPoint = cam.ScreenToWorldPoint(new Vector3(-offsetToOutsideCameraPos, -offsetToOutsideCameraPos, Camera.main.nearClipPlane));
        //Debug.Log("Camera Full POint");
        //Debug.Log(fullPoint);
        //Debug.Log("Camera Zero POint");
        //Debug.Log(zeroPoint);
    }
    private void Update()
    {
        SpawnAsteroid();
        SetNextToSpawn();


    }
    private void SpawnAsteroid()
    {
        if (spawnTimer < 0f)
        {
            if (nextToSpawn == null)
            {
                SetNextToSpawn();
            }            
            var spawnedObj = Instantiate(nextToSpawn, gameObject.transform);
            planet.GetComponent<PlanetManager>().enemyShipSpawned(spawnedObj);
            spawnedObj.transform.position = positionOfSpawningAsteroid;
            // # rotate toward the target (planet)
            Vector3 vectorToTarget = planet.transform.position - spawnedObj.transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            spawnedObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            // # add speed/velocity to the spawned body
            Rigidbody2D rBody = spawnedObj.GetComponent<Rigidbody2D>();
            rBody.velocity = spawnedObj.transform.TransformDirection(Vector2.right * speedOfAsteroid);
            
            currentSpawnsInPos++;
            nextToSpawn = null;
            spawnTimer = spawnTime;
        }
        else { spawnTimer -= Time.deltaTime; }

    }
    private void SetNextToSpawn()
    {
        if (nextToSpawn == null)
        {
            Debug.Log(sposition);
            nextToSpawn = typeOfAsteroids[Random.Range(0, typeOfAsteroids.Count - 1)];
            speedOfAsteroid = Random.Range(asteroidSpeedRangeMin, asteroidSpeedRangeMax);
            if (numberOfSpawnsBeforeChangingPos >= currentSpawnsInPos)
            {
                sposition = (SpawnPosition)Random.Range(0, 4);
                currentSpawnsInPos = 0;
            }
            switch (sposition)
            {
                case SpawnPosition.Top:
                    positionOfSpawningAsteroid = new Vector3(Random.Range(zeroPoint.x, fullPoint.x), fullPoint.y);
                    break;
                case SpawnPosition.Right:
                    positionOfSpawningAsteroid = new Vector3(fullPoint.x, Random.Range(zeroPoint.y, fullPoint.y));
                    break;
                case SpawnPosition.Bottom:
                    positionOfSpawningAsteroid = new Vector3(Random.Range(zeroPoint.x, fullPoint.x), zeroPoint.y);
                    break;
                case SpawnPosition.Left:
                    positionOfSpawningAsteroid = new Vector3(zeroPoint.x, Random.Range(zeroPoint.y, fullPoint.y));
                    break;
            }
        }
    }


    void OnGUI()
    {

        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();
    }
    #endregion


}
