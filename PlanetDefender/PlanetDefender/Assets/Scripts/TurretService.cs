using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretService : MonoBehaviour
{
    #region public ref variables
    // temp targetEnShip
    [SerializeField]
    GameObject targetEnShip;

    public GameObject turretHead;
    public GameObject bulletPrefab;
    #endregion
    #region variables
    [SerializeField]
    private float rotationSpeed= 5f;
    [SerializeField]
    public float detectionRange { get; } = 25f; 
    #endregion
    #region turret shooting variables 
    [SerializeField]
    private float shootSpeed = 1f;  
    private float shootTime = 0.2f;
    private float shootTimer = 0f;
    #endregion
    #region methods 

    public void enemyInRange(GameObject enShip)
    {
        if (targetEnShip == null)
        {
            targetEnShip = enShip;
           
        }
    }   
    private void turnTowardEnemy()
    {        
        float angle = Mathf.Atan2(transform.position.y - targetEnShip.transform.position.y , transform.position.x - targetEnShip.transform.position.x ) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
     
        turretHead.transform.rotation = Quaternion.Slerp(turretHead.transform.rotation, targetRotation, Time.deltaTime * 5f);
        
    }
    private void shootEnemy()
    {
        if (shootTimer < 0)
        {
           
            if (bulletPrefab != null)
            {
                GameObject shootedBullet = Instantiate(bulletPrefab, new Vector3(turretHead.transform.position.x, turretHead.transform.position.y, 0f),turretHead.transform.rotation);
                Rigidbody2D rBody = shootedBullet.GetComponent<Rigidbody2D>();
                Vector3 eulerRotation = new Vector3(shootedBullet.transform.eulerAngles.x, shootedBullet.transform.eulerAngles.y, turretHead.transform.eulerAngles.z + 90);

                shootedBullet.transform.rotation = Quaternion.Euler(eulerRotation);

                rBody.velocity = turretHead.transform.TransformDirection(Vector2.left * shootSpeed) ;
            }else
            {
                // Debugging : LOG the bug that no prefab is assign
            }
            shootTimer = shootTime;


        }
        else
        {
            shootTimer -= Time.deltaTime;
        }
    }
    private void Update()
    {
        if(targetEnShip != null)
        {
          turnTowardEnemy();
          shootEnemy();

        }
    }


    #endregion
}
