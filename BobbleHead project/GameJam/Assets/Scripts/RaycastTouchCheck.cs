using UnityEngine;
using System.Collections;

public class RaycastTouchCheck 
{
    private Vector2 raycastDirection;
    private Vector2[] offsetPoints;
    private LayerMask mask;
    private float raycastCheckLength;
    private Color color;

    public RaycastTouchCheck(Vector2 start, Vector2 end, Vector2 direction, LayerMask mask, Vector2 parlelInset, Vector2 perpenInset,float checkLength , Color color)
    {
        this.raycastDirection = direction;

        this.offsetPoints = new Vector2[]
        {
            start +parlelInset + perpenInset,
            end - parlelInset + perpenInset,
        };
        this.raycastCheckLength = perpenInset.magnitude   + checkLength;
        this.mask = mask;
        this.color = color;
    }

    public bool DoRayCast(Vector2 origin)
    {
 
        foreach (var offset in offsetPoints)
        {
            RaycastHit2D hit = Raycast(origin + offset, raycastDirection, raycastCheckLength, mask);
            if (hit.collider != null)
            {
                return true;

            }
        }
        return false;
    }
    private RaycastHit2D Raycast(Vector2 start, Vector2 dir, float len, LayerMask mask)
    {
        // Debug.Log(string.Format("Raycast start {0} in {1} for {2}", start, dir, len));
        Debug.DrawLine(start, start + dir * len, color);
        RaycastHit2D rCast = Physics2D.Raycast(start, dir, len, mask);
        return rCast;
    }
    public RaycastHit2D DoRayCastGetCollidorObject(Vector2 origin)
    {     
        RaycastHit2D hit = Raycast(origin + offsetPoints[0], raycastDirection, raycastCheckLength, mask);
        return hit;
    }
} 
