using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask layer;
    public float hitDmg = 25f;
    public bool destroyOnhit = true;
    private bool dmgDone= false;    
    private void FixedUpdate()
    {
        if (dmgDone == false)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1f, layer);
            if (hit.collider != null)
            {
                //Debug.Log(hit.collider.transform.root.gameObject.name);

                healthScript hs = hit.collider.transform.root.gameObject.GetComponentInParent<healthScript>();
                if (hs != null)
                {
                    hs.YouGotHit(hitDmg);
                    dmgDone = true;


                }
                if (destroyOnhit) Destroy(gameObject);
            }
        }
    }
}
