using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastService : MonoBehaviour
{
    private enum JumpState
    {
        None = 0,Holding,
    }

    // Ray Tracing collision related
    [Header("Ray Tracing Collision Related")]
    [SerializeField]
    private LayerMask platformMask;
    [SerializeField]
    private float paralelInsetLength;
    [SerializeField]
    private float perpenInsetLength;
    [SerializeField]
    private float groundLength;
    [Space(10)]
    // Physics engine player movement feel related
    [Header("Physics Engine Related")]
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float horizontalTurnSnapSpeed;
    [SerializeField]
    private float horizontalSpeedAccel;
    [SerializeField]
    private float horizontalStopAccel;
    [SerializeField]
    private float horizTopSpeed;
    [SerializeField]
    private float jumpInputLeewayPeriod;
    [SerializeField]
    private float jumpStartSpeed;
    [SerializeField]
    private float jumpMaxHoldPeriod;
    [SerializeField]
    private float groundNotTouchJumpTimer;
   
    private Vector2 velocity;
    
    private bool jumped;
    private float jumpStartTimer;
    private float jumpHolderTimer;
    private bool jumpInputDown;
    private JumpState jumpState;
    private float inAirFromGroundTimer;
    private bool inAirGroundTimerRunning = false;
    private bool downed = false;
    private int currentDirection; // + is right and - is left
    // player items related
    Equipment playerItem;
     
    private GameObject handItem;
    //Animation related
    private Animator animator;
    PhotonView pView;

    // Ray Cast player movement related
    private RaycastMoveDirection moveDown;
    private RaycastMoveDirection moveLeft;
    private RaycastMoveDirection moveRight;
    private RaycastMoveDirection moveUp;
    private RaycastTouchCheck groundDown;

    [Header("Left Ray Trace Position X")]
    [Space(2)]
    [Header("Ray Tracing Positions")]
    [SerializeField]
    private float leftStartRayTraceX;
    [SerializeField]
    private float leftEndRayTraceX; 
    [Header("Left Ray Trace Position Y")]
    [SerializeField]
    private float leftStartRayTraceY;
    [SerializeField]
    private float leftEndRayTraceY;
    [Space(2)]
    [Header("Up Ray Trace Position X")]
    [SerializeField]
    private float upRayStartTraceX;
    [SerializeField]
    private float upRayEndTraceX;  
    [Header("Up Ray Trace Position Y")]
    [SerializeField]
    private float upRayStartTraceY;
    [SerializeField]
    private float upRayEndTraceY;
    [Space(2)]
    [Header("Right Ray Trace Position X")]
    [SerializeField]
    private float rightStartRayTraceX;
    [SerializeField]
    private float rightEndRayTraceX;
    [Header("Right Ray Trace Position Y")]
    [SerializeField]
    private float rightStartRayTraceY;
    [SerializeField]
    private float rightEndRayTraceY;
    [Space(2)]
    [Header("Down Ray Trace Position X")]
    [SerializeField]
    private float downRayStartTraceX;
    [SerializeField]
    private float downRayEndTraceX;
    [Header("Down Ray Trace Position Y")]
    [SerializeField]
    private float downRayStartTraceY;
    [SerializeField]
    private float downRayEndTraceY;
    [Space(2)]
    [Header("Ground Down Check Ray Trace Position X")]
    [SerializeField]
    private float groundDownStartRayTraceX;
    [SerializeField]
    private float groundDownEndRayTraceX;
    [Header("Ground Down Check Ray Trace Position Y")]
    [SerializeField]
    private float groundDownStartRayTraceY;
    [SerializeField]
    private float groundDownEndRayTraceY;

    



    // get the sign for detecting if we are going left or right
    private int GetSign(float v)
    { 
        if (Mathf.Approximately(v, 0))
        {
            return 0;
        }else 
        if (v > 0) 
        {
            return 1;
        }else
        {
            return -1;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // animator
        animator = gameObject.GetComponent<Animator>();
        // ray cast movement directions
        moveLeft = new RaycastMoveDirection(new Vector2(leftStartRayTraceX, leftStartRayTraceY), new Vector2(leftEndRayTraceX, leftEndRayTraceY), Vector2.left, platformMask, Vector2.down * paralelInsetLength, Vector2.right * perpenInsetLength);
        moveUp = new RaycastMoveDirection(new Vector2(upRayStartTraceX, upRayStartTraceY), new Vector2(upRayEndTraceX, upRayEndTraceY), Vector2.up, platformMask, Vector2.right * paralelInsetLength, Vector2.down * perpenInsetLength);
        moveRight = new RaycastMoveDirection(new Vector2(rightStartRayTraceX, rightStartRayTraceY), new Vector2(rightEndRayTraceX, rightEndRayTraceY), Vector2.right, platformMask, Vector2.up * paralelInsetLength, Vector2.left * perpenInsetLength);
        moveDown = new RaycastMoveDirection(new Vector2(downRayStartTraceX, downRayStartTraceY), new Vector2(downRayEndTraceX, downRayEndTraceY), Vector2.down, platformMask, Vector2.right * paralelInsetLength, Vector2.up * perpenInsetLength);
        groundDown = new RaycastTouchCheck(new Vector2(groundDownStartRayTraceX, groundDownStartRayTraceY), new Vector2(groundDownEndRayTraceX, groundDownEndRayTraceY), Vector2.down, platformMask, Vector2.right * paralelInsetLength, Vector2.up * perpenInsetLength, groundLength ,Color.yellow);
        pView = GetComponent<PhotonView>();
    }
    private void Update()
    {
        if (pView.isMine)
        {
            jumpStartTimer -= Time.deltaTime;
            inAirFromGroundTimer -= Time.deltaTime;

            bool jumpBtn = Input.GetButton("Jump");
            if (jumpBtn && jumpInputDown == false)
            {
                jumpStartTimer = jumpInputLeewayPeriod;
            }
            jumpInputDown = jumpBtn;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pView.isMine)
        {
            if (!downed)

            {
                bool grounded = groundDown.DoRayCast(transform.position);
                if (grounded)
                {
                    if (inAirFromGroundTimer < 0) inAirGroundTimerRunning = false;
                    jumped = false;
                }
                // jump even if not grounded and not jumping
                if (!grounded && !jumped && !inAirGroundTimerRunning)
                {
                    inAirGroundTimerRunning = true;
                    inAirFromGroundTimer = groundNotTouchJumpTimer;
                }
                // jump
                switch (jumpState)
                {
                    case JumpState.None:
                        bool checkInAirTimer = inAirFromGroundTimer > 0;

                        if ((grounded || checkInAirTimer) && jumpStartTimer > 0)
                        {
                            jumped = true;
                            inAirFromGroundTimer = 0;
                            jumpStartTimer = 0;
                            jumpState = JumpState.Holding;
                            jumpHolderTimer = 0;
                            velocity.y = jumpStartSpeed;
                        }
                        break;
                    case JumpState.Holding:
                        jumpHolderTimer += Time.deltaTime;
                        if (jumpInputDown == false || jumpHolderTimer >= jumpMaxHoldPeriod)
                        {
                            jumpState = JumpState.None;
                        }
                        break;
                }


                // velocity value calculator on input ( switching horizontal direction and giving the same velocity )
                float horizInput = Input.GetAxisRaw("Horizontal");
                int wantedDirection = GetSign(horizInput);
                int velocityDirection = GetSign(velocity.x);
                if (wantedDirection != 0 && wantedDirection != velocityDirection) { velocity.x = horizontalTurnSnapSpeed * wantedDirection; }
                else if (wantedDirection != 0) { velocity.x = Mathf.MoveTowards(velocity.x, horizTopSpeed * wantedDirection, horizontalSpeedAccel * Time.deltaTime); }
                else { velocity.x = Mathf.MoveTowards(velocity.x, 0, horizontalStopAccel * Time.deltaTime); }

                // check ground
                if (jumpState == JumpState.None)
                {
                    velocity.y -= gravity * Time.deltaTime;
                }

                Vector2 displacement = Vector2.zero;
                Vector2 wantedDisplacement = velocity * Time.deltaTime;

                // move transform horizontal
                if (velocity.x > 0) { displacement.x = moveRight.DoRayCast(transform.position, wantedDisplacement.x); }
                else if (velocity.x < 0) { displacement.x = -moveLeft.DoRayCast(transform.position, -wantedDisplacement.x); }

                // move transform vertical
                if (velocity.y > 0) { displacement.y = moveUp.DoRayCast(transform.position, wantedDisplacement.y); }
                else if (velocity.y < 0) { displacement.y = -moveDown.DoRayCast(transform.position, -wantedDisplacement.y); }

                // disable velocity on collision or clipping
                if (Mathf.Approximately(displacement.x, wantedDisplacement.x) == false) { velocity.x = 0; }
                if (Mathf.Approximately(displacement.y, wantedDisplacement.y) == false) { velocity.y = 0; }
                transform.Translate(displacement);

                // animation related
                if (jumpState == JumpState.Holding)
                {
                    if (animator.GetBool("Jumping") != true) { animator.SetBool("Jumping", true); }
                }
                else
                {
                    if (grounded)
                    {
                        animator.SetBool("Jumping", false);
                        if (wantedDirection == 0)
                        {
                            if (animator.GetBool("Moving") == true) { animator.SetBool("Moving", false); }
                        }
                        else
                        {
                            if (animator.GetBool("Moving") == false) { animator.SetBool("Moving", true); }
                        }
                    }
                    else
                    {
                        //          animator.SetBool("Jumping" , true);
                    }
                }
                if (wantedDirection != 0)
                {
                    
                    if (GetSign(transform.localScale.x) != velocityDirection)
                    {
                        animator.SetInteger("Direction", horizInput > 0 ? 1: -1 );
                        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    }

                }
            }
        }
        
    }

    public void setHandItemGameObject(GameObject handItemGObj)
    {
        Debug.Log("Set the game object" , handItemGObj);
        handItem = handItemGObj;
        //if(playerItem != null) { handItem.GetComponent<PlayerItem>().setSprite(playerItem.equipSprite); }
    }
    public void playerGotHit()
    {
        Debug.Log("I am in playerGotHit");
        downed = true;
        animator.SetBool("Downed", true);
    } 
}
