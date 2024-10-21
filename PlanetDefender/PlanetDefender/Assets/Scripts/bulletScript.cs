using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    [SerializeField]
    float bulletDmg = 2f;
    [SerializeField]
    private LayerMask layerMask;
    Vector2 start;
    [SerializeField]
    bool planetKiller = false;
    [SerializeField]
    bool checkInFirst = false;
    private void Start()
    {
        start = new Vector2(transform.position.x  , transform.position.y);
    }
    // Update is called once per frame
    void Update()
    {


        projectileShoot();
        if (checkInFirst == true) { checkIfOut(); }
        else{ areWeIn(); }


    }
    void areWeIn()
    {
        if (GetComponent<Renderer>().isVisible)
        {
            checkInFirst = true;
        }
    }
    void projectileShoot()
    {
        start = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D shoot = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 0.1f, layerMask);

        if (shoot.collider != null && planetKiller == true)
        {
            shoot.collider.gameObject.GetComponent<PlanetManager>().PlanetGotHit(bulletDmg);
            Object.Destroy(gameObject);
        }else if(shoot.collider != null)
        {
            Object.Destroy(gameObject);
            shoot.collider.gameObject.GetComponent<healthScript>().YouGotHit(bulletDmg);
        }


    }
    void checkIfOut()
    {
        if (!GetComponent<Renderer>().isVisible)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
