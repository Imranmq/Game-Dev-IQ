using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RotateAroundTarget : MonoBehaviour
{
    [SerializeField]
    private float RotateSpeed = 1f;
    private float Radius =2f;
    private float currentAngleFromCenter= 0f;
    private Vector2 _centre;
    private float _angle;
    public bool Debugger;
    public Text textComp;
    public bool startRotating = false;

    public void setCenter(Vector2 center) { _centre = center; }
    public void setCurrentAngle(float rad)    
    {
        //currentAngleFromCenter = rad * 180f / Mathf.PI;
        _angle = rad;
        Debug.Log("Initial Angle of" + textComp.text + ": " + _angle);

    }
    public void setCurrentAngleDegree(float angle)
    {        

        //_angle = angle;
      

    }
    public void setText(string label ,int val)
    {
        textComp.text = label + val.ToString();
    }
    private void Update()   
    {
        if (startRotating)
        {

            _angle -= RotateSpeed * Time.deltaTime ;

            var offset = new Vector2(Mathf.Sin(_angle  ), Mathf.Cos(_angle)) * Radius ;
            if (Debugger)
            {
                Debug.Log("Adjusted Angle " + textComp.text + ": " + _angle);
                Debug.Log("offset of " + textComp.text + ": " + offset);
            }
         
            transform.position = _centre + offset;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_centre, 0.1f);
        Gizmos.DrawLine(_centre, transform.position);
    }
}
