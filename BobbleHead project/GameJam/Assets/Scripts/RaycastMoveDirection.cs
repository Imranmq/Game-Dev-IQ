using UnityEngine;
using System.Collections;

public class RaycastMoveDirection 
{
    private Vector2 raycastDirection;
    private Vector2[] offsetPoints;
    private LayerMask mask;
    private float addLength;
    public RaycastMoveDirection(Vector2 start , Vector2 end , Vector2 direction  , LayerMask mask , Vector2 parlelInset , Vector2 perpenInset) {
        this.raycastDirection = direction;

        this.offsetPoints = new Vector2[]
        {
            start +parlelInset + perpenInset,
            end - parlelInset + perpenInset,
        };
        this.addLength = perpenInset.magnitude;
        this.mask = mask;
    }
   
    public float DoRayCast(Vector2 origin ,float distance){
        float minDist = distance;
        foreach (var offset in offsetPoints) {
            RaycastHit2D hit = Raycast(origin+offset, raycastDirection, distance + addLength, mask);
            if (hit.collider != null){             
                minDist = Mathf.Min(minDist, hit.distance - addLength);

            }            
        }
        return minDist;
    }
    private RaycastHit2D Raycast(Vector2 start , Vector2 dir ,  float len , LayerMask mask)
    {
       // Debug.Log(string.Format("Raycast start {0} in {1} for {2}", start, dir, len));
        Debug.DrawLine(start, start + dir * len, Color.blue);
        return Physics2D.Raycast(start, dir, len, mask);
    }
}
