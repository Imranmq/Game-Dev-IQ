using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatcherService : MonoBehaviour
{


    [SerializeField]
    private LayerMask runnerMask;
    [SerializeField]
    private float hitLength;
    [SerializeField]
    private float hitActionTime;

    [Header("Ray Cast Hit Action X")]
    [SerializeField]
    private float hitActionStartRayTraceX;
    [SerializeField]
    private float hitActionEndRayTraceX;
    [Header("Ray Cast Hit Action Y")]
    [SerializeField]
    private float hitActionStartRayTraceY;
    [SerializeField]
    private float hitActionEndRayTraceY;


    private RaycastTouchCheck hitAction;

    private bool hitActionInProgress;
    private float hitActionProgressTimer;
    // Start is called before the first frame update
    void Start()
    {
        hitAction = new RaycastTouchCheck(new Vector2(hitActionStartRayTraceX, hitActionStartRayTraceY), new Vector2(hitActionEndRayTraceX, hitActionStartRayTraceY), Vector2.right, runnerMask, Vector2.right * 0f, Vector2.up * 0f, hitLength, Color.red); 
    }

    // Update is called once per frame
    void Update()
    {
        hitActionProgressTimer -= Time.deltaTime;
        if (Input.GetButton("Fire1") && hitActionProgressTimer <= 0) {
            hitActionInProgress = true;
            hitActionProgressTimer = hitActionTime;
        }               

    }
    private void FixedUpdate()
    {
        if (hitActionInProgress)
        {
            hitActionInProgress = false;
            RaycastHit2D hit = hitAction.DoRayCastGetCollidorObject(transform.position);
            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<RaycastService>().playerGotHit();                
                Debug.Log("I Hit him");
            }
        }     
    }
}
