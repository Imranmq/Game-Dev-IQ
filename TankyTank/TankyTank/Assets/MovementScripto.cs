using Sfs2X.Entities.Data;
using Sfs2X.Entities.Variables;
using Sfs2X.Requests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum MovementStates
{
    NoMove = 0,
    MoveAhead = 1,
    MoveBehind = 2,
}
public enum RotationStates
{
    NoRotate = 0,
    RotateLeft = 1,
    RotateRight = 2,

}

public enum CannonHeadMoveUpDown
{
    MoveUp,
    MoveDown,
    NoMove
}


public class MovementScripto : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    // Start is called before the first frame update
    private OurMovementActions playerInputActions;
    private int bulletCoolDown = 0;
    private float timer = 3f;

    public GameObject CannonHead;
    public GameObject Cannon;
    public GameObject bullet;

    private Vector3 mouseWorldPos;

    public float smooth = 3.0f;
    public float tiltDownAngle = 5.0f;
    public float tiltUpAngle = 20.0f;
    bool allowToothniMovement = true;

    public float CannonHeadSpeed = 0.2f;
    public float shootForce = 1f;

    public bool PlayerCheck;

    public MovementStates movementState = MovementStates.NoMove;
    public RotationStates tankRotate = RotationStates.NoRotate;
    public CannonHeadMoveUpDown cannonUpDown = CannonHeadMoveUpDown.NoMove;
    public bool bulletShoot = false;
    public float angleAdjuster;
    #region input variables
    Vector2 inputVector;
    float inputToothni;
    float mouseClick;
    Vector3 mousePos;
    #endregion
    private void Awake()
    {
        playerInputActions = new OurMovementActions();
        playerInputActions.movement.Enable();
        playerInputActions.movement.ToothniMovement.Enable();
        playerInputActions.movement.MouseEvents.Enable();
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (PlayerCheck)
        {
            playerMovementMethod();
        }
        MoveTank(); RotateTank(); ToothniUpDown(); CannonHeadRotation(angleAdjuster); CheckMouseClick(); CheckTimer();

    }
    void playerMovementMethod()
    {
        // Inputs from Input system
        inputVector = playerInputActions.movement.Move.ReadValue<Vector2>();
        inputToothni = playerInputActions.movement.ToothniMovement.ReadValue<float>();
        mouseClick = playerInputActions.movement.MouseEvents.ReadValue<float>();
        mousePos = Mouse.current.position.ReadValue();

        // States for Multiplayer variables
        List<UserVariable> toUpdateVars = new List<UserVariable>();
        SetMoveStates(inputVector.y, out bool yValChanged);
        SetTankRotateStates(inputVector.x, out bool xValChanged);
        MouseMoveAngleCalculate(mousePos, out bool angleChanged);
        SetTankHeadUpDownState(inputToothni, out bool headChangedMove);
        SetTankShoot(mouseClick, out bool shootVal);
        // if states are change send the variables/store value to be used later
        if (xValChanged == true) toUpdateVars.Add(new SFSUserVariable("ms_x", (double)inputVector.x));
        if (yValChanged == true) toUpdateVars.Add(new SFSUserVariable("ms_y", (double)inputVector.y));
        if (angleChanged == true) toUpdateVars.Add(new SFSUserVariable("ch_r", (double)CannonHead.transform.localRotation.eulerAngles.y));
        if (headChangedMove == true) toUpdateVars.Add(new SFSUserVariable("c_m", (double)inputToothni));
        if(shootVal == true) toUpdateVars.Add(new SFSUserVariable("M_c1", (double)mouseClick));
        if (SmartFoxConnection.Connection != null && toUpdateVars.Count != 0) SmartFoxConnection.Connection.Send(new SetUserVariablesRequest(toUpdateVars));
      
    }

    private void CheckTimer()
    {
        if (bulletCoolDown > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0f)
            {
                bulletCoolDown = 0;
                timer = 3f;
            }
        }
    }
    private void RotateTank()
    {
        if (tankRotate != RotationStates.NoRotate)
        {
            float rotateSpeed = 35f * Time.deltaTime;
            int direction = tankRotate == RotationStates.RotateLeft ? 1 : -1;
            transform.Rotate(new Vector3(0, direction, 0) * rotateSpeed);
        }
    }
    private void MoveTank()
    {
        if (movementState != MovementStates.NoMove)
        {
            float moveSpeed = 5f * Time.deltaTime;
            transform.position += movementState == MovementStates.MoveAhead ? transform.forward * moveSpeed : transform.forward * -1 * moveSpeed;

        }
    }
    private void ToothniUpDown()
    {

        if (cannonUpDown != CannonHeadMoveUpDown.NoMove && allowToothniMovement)
        {
            float smoothDir = cannonUpDown == CannonHeadMoveUpDown.MoveUp ? smooth : -smooth;
            Cannon.transform.Rotate(Vector3.right, smoothDir * Time.deltaTime);
            Vector3 currentRotation = Cannon.transform.localRotation.eulerAngles;
            float value = currentRotation.x > 300f && currentRotation.x < 360f ? currentRotation.x - 360f : currentRotation.x;
            float newVal = Mathf.Clamp(value, -20f, 5f);
            if (newVal == -20f) currentRotation.x = 340f;
            else if (newVal == 5f) currentRotation.x = 5f;
            Cannon.transform.localRotation = Quaternion.Euler(currentRotation);
        }
    }


    private void CannonHeadRotation(float angleAdjuster)
    {
        //Vector3 headPos = Camera.main.WorldToScreenPoint(CannonHead.transform.position);
      
    }
    private void CheckMouseClick()
    {

        if (bulletShoot == true )
        {           
            bulletShoot = false;
            if (bullet != null)
            {
                GameObject projectile = Instantiate(bullet);
                projectile.transform.position = Cannon.transform.position + Cannon.transform.forward * 4.6f;
                Rigidbody projectileRBody = projectile.GetComponent<Rigidbody>();
                projectileRBody.velocity = Cannon.transform.forward * shootForce;
            }

        }
    }

    public void SetMoveStates(float inputY, out bool yValChanged)
    {
        if (inputY > 0 && movementState != MovementStates.MoveAhead)
        {
            movementState = MovementStates.MoveAhead;
            yValChanged = true;
        }
        else if (inputY < 0 && movementState != MovementStates.MoveBehind)
        {
            movementState = MovementStates.MoveBehind;
            yValChanged = true;
        }
        else if (movementState != MovementStates.NoMove)
        {
            movementState = MovementStates.NoMove;
            yValChanged = true;
        }
        else
        {
            yValChanged = false;
        }


    }
    public void SetTankRotateStates(float inputX, out bool xValChanged)
    {
        if (inputX > 0 && tankRotate != RotationStates.RotateLeft)
        {
            tankRotate = RotationStates.RotateLeft;
            xValChanged = true;
        }
        else if (inputX < 0 && tankRotate != RotationStates.RotateRight)
        {
            tankRotate = RotationStates.RotateRight;
            xValChanged = true;

        }
        else if (tankRotate != RotationStates.NoRotate)
        {
            tankRotate = RotationStates.NoRotate;
            xValChanged = true;
        }
        else
        {
            xValChanged = false;
        }
    }
    private void MouseMoveAngleCalculate(Vector3 mousePos, out bool angleChanged)
    {
        angleChanged = false;
        float locAngleAdjuster = CannonHead.transform.localRotation.eulerAngles.y;
        Ray screenRayCast = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(screenRayCast, out hit))
        {
            mouseWorldPos = hit.point;
        }
        if (mouseWorldPos != null)
        {
            float angle = Mathf.Atan2(mouseWorldPos.x - CannonHead.transform.position.x, mouseWorldPos.z - CannonHead.transform.position.z) * Mathf.Rad2Deg;
            //  Debug.DrawLine(CannonHead.transform.position, mouseWorldPos, Color.yellow);
            locAngleAdjuster = angle - transform.rotation.eulerAngles.y;
        }
        if (locAngleAdjuster != angleAdjuster)
        {
            angleChanged = true;
            angleAdjuster = locAngleAdjuster;
        }
        Vector3 cannonHeadRotEuler = CannonHead.transform.localRotation.eulerAngles;
        Quaternion currentRotation = Quaternion.Euler(new Vector3(cannonHeadRotEuler.x, cannonHeadRotEuler.y, cannonHeadRotEuler.z));
        Quaternion targetRotation = Quaternion.Euler(new Vector3(cannonHeadRotEuler.x, angleAdjuster, cannonHeadRotEuler.z));
        CannonHead.transform.localRotation = Quaternion.Lerp(currentRotation, targetRotation, CannonHeadSpeed * Time.deltaTime);

    }
    public void SetTankHeadUpDownState(float inputToothni, out bool headChangedMove)
    {
        if (inputToothni > 0 && cannonUpDown != CannonHeadMoveUpDown.MoveUp)
        {
            cannonUpDown = CannonHeadMoveUpDown.MoveUp;

            headChangedMove = true;
        }
        else if (inputToothni < 0 && cannonUpDown != CannonHeadMoveUpDown.MoveDown)
        {
            cannonUpDown = CannonHeadMoveUpDown.MoveDown;
            headChangedMove = true;
        }
        else if (cannonUpDown != CannonHeadMoveUpDown.NoMove)
        {
            cannonUpDown = CannonHeadMoveUpDown.NoMove;
            headChangedMove = true;
        }
        else
        {
            headChangedMove = false;
        }
    }

    public void SetTankShoot(float mouseClick , out bool shootVal)
    {
        shootVal = false;
        if (mouseClick != 0 && bulletCoolDown == 0)
        {
            shootVal = true;
            bulletCoolDown = 1;
            bulletShoot = true;


        }
    }
}
