using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    #region bullet related variables
    [SerializeField]
    private float shootSpeed = 1f;
    [SerializeField]
    GameObject bullet;
    private float shootTime =  0.2f;
    private float shootTimer = 0f;

    private healthScript hlthScript;
    
    #endregion
    #region variables   
    public PlanetManager planetObjScript { get; set; }
    #endregion
    #region unity methods
    private void Awake()
    {
        hlthScript = gameObject.GetComponent<healthScript>();
    }
    void Update()
    {
        //shootBullet();
        checkHealth();

    }
    #endregion
    #region health related methods
    private void checkHealth()
    {
        
        if (hlthScript != null && hlthScript.getHealth() <= 0f)
        {
            if (planetObjScript)
            {
                planetObjScript.removeEnemyShipFromList(gameObject);
            }
      
        }
    }
    #endregion
    #region shooting related method
    void shootBullet()
    {
        if (shootTimer < 0)
        {
            //start = new Vector2(transform.position.x - 0.4f, transform.position.y);
            //RaycastHit2D shoot = Physics2D.Raycast(start, Vector2.right, distance, layerMask);

            //if (shoot.collider != null)
            //{
            //    Debug.Log(shoot);
            //}
            if (bullet != null)
            {
                GameObject shootedBullet = Instantiate(bullet, new Vector3(transform.position.x + 0.4f, transform.position.y, 0f), transform.rotation);
                Rigidbody2D rBody = shootedBullet.GetComponent<Rigidbody2D>();
                rBody.velocity = transform.TransformDirection(Vector2.up * shootSpeed);
            }
            shootTimer = shootTime;


        }
        else
        {
            shootTimer -= Time.deltaTime;
        }

    }
    #endregion
    #region methods
    
    #endregion
}
