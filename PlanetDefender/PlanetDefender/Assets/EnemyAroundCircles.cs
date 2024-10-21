using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAroundCircles : MonoBehaviour
{
    public GameObject enemyPefab;
    public float radius = 2f;
    public int pieceCount = 12;
    void Start()
    {
        CreateEnemiesAroundPoint(pieceCount, transform.position, radius);
    }

    public void CreateEnemiesAroundPoint(int num, Vector3 point, float radius)
    {

        for (int i = 1; i <= num; i++)
        {

            /* Distance around the circle */
            var radians = 2 * Mathf.PI / num * i;
        
            /* Get the vector direction */
            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);

            var spawnDir = new Vector3(horizontal, vertical, 0);

            /* Get the spawn position */
            var spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

            /* Now spawn */
            GameObject enemy = Instantiate(enemyPefab, spawnPos, Quaternion.identity) as GameObject;
            RotateAroundTarget rtService = enemy.GetComponent<RotateAroundTarget>();
            rtService.setText("E", i);
            rtService.setCenter(transform.position);
            rtService.setCurrentAngle(radians);
          

            ///* Rotate the enemy to face towards player */
            //enemy.transform.LookAt(point);

            ///* Adjust height */
            //enemy.transform.Translate(new Vector3(0, enemy.transform.localScale.y / 2, 0));
        }
    }
}
