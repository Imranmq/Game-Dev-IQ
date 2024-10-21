using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTopDown : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    public bool gridCheckOnMovement = true;
    private void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        movement = movement.normalized;
    }

    private void FixedUpdate()
    {
        if (movement.x != 0 || movement.y !=0 ) {
         //   Debug.Log(movement.x + " , " +  movement.y);
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            if(gridCheckOnMovement == true)
            {
                PlayerGridManager pGm = this.GetComponent<PlayerGridManager>();
                if (AstropolyUtils.CheckForNotNull(pGm))
                {
                    
                    pGm.playerMoveGridCheck();
                }
            }
        }       
    }
}
