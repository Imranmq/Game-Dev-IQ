using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunButton : MonoBehaviour
{
    // variables
    bool moveCycle;
    bool rotateOncePressed = false;
    bool oppositeMoveCycle = false;
    [SerializeField]
    float distance = 3f;
    float coveredDistance = 0f;
    [SerializeField]
    float moveSpeed = 1f;
    [Header("Stamina Values")]
    [SerializeField]
    float maxStamina = 100f;
    [SerializeField]
    float maxStaminaNextBracketProgression = 150f;
    [SerializeField]
    float stamina = 100f;
    [SerializeField]
    float staminaRegain = 10f;
    [SerializeField]
    float staminaLoss = 5f;
    [SerializeField]
    float baseGrowthPoints =3f;
    [SerializeField]
    float growthPoints = 0f;
    [SerializeField]
    float staminaMinRegainLimit = 25f;
    [SerializeField]
    float exertionlevel = 90f;
    bool regenStaminaStatus = false;
    float growthApplySpeed = 3f;    
    // # factors of values 
    [SerializeField]
    float maxStaminaNextBracketFactor = 0.3f;
    [SerializeField]
    float growthPointsFactor = 0.1f;
    [SerializeField]
    float exertionFactor = 0.3f;
    [SerializeField]
    float staminaRegainFactor = 0.1f;
    [SerializeField]
    float staminaLossFactor = 0.17f;
    [SerializeField]
    float staminaMinRegainLimitFactor = 0.2f;
    // bound realatedx
    SpriteRenderer m_SpriteRenderer;
    [SerializeField]
    GameObject spriteObject;
    private Camera cam;
    float xBoundOffset;
    float yBoundOffset;
    float width;
    float height;
    // methods

    [SerializeField]
    UISliderManager growthBarUI;
    [SerializeField]
    UITextScript levelTextUI;
    private void Start()
    {




        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.color = Color.green;
        cam = Camera.main;

        float width = GetComponent<SpriteRenderer>().bounds.size.x;
        float height = GetComponent<SpriteRenderer>().bounds.size.y;
        setInitialStaminaStats();

    }
    bool checkIfHitScreen()
    {
        bool hitScreen = false;      
        {
            Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
            bool outOfX = (screenPos.x < 0f || screenPos.x > cam.pixelWidth);
            bool outOfY = (screenPos.y < 0f || screenPos.y > cam.pixelHeight);
            if (outOfX || outOfY)
            {
                hitScreen = true;
            }
        }
        return hitScreen;
    }

    void setInitialStaminaStats()
    {
        // TODO : get data from player profile local db
        bool fileExist = false;
        if (fileExist)
        {
            applyProgressionVals();
        }else
        {
            growthBarUI.SetMaxValue(maxStaminaNextBracketProgression - maxStamina);
            levelTextUI.setTextValue("1");


        }
    }

    void applyProgressionVals()
    {
        //stamina = maxStamina;
        //staminaRegain = maxStamina * 15 / 100;
        //staminaLoss = maxStamina * 15 / 100;
        //staminaMinRegainLimit = maxStamina * 25 / 100;
        //exertionlevel = maxStamina * exertionPercentage;
    }

    private void Update()
    {
        bool newJumpPress = Input.GetButtonDown("Jump");

        if (newJumpPress == true && regenStaminaStatus == false)
        {
            rotateOncePressed = false;
            moveCycle = true;
        }
    }
    private void FixedUpdate()
    {
        if (checkIfHitScreen())
        {
            oppositeMoveCycle = !oppositeMoveCycle;
            coveredDistance = distance - 0.5f;
        }
        if (stamina < 0f)
        {
            m_SpriteRenderer.color = Color.red;
            regenStaminaStatus = true;
            moveCycle = false;
        }
        if (moveCycle)
        {
            m_SpriteRenderer.color = Color.blue;
            MoveRandomDirection();
        }
        if (regenStaminaStatus || (moveCycle == false && stamina < maxStamina))
        {
            RegainStaminaStatus();
            if (regenStaminaStatus && stamina > staminaMinRegainLimit)
            {
                m_SpriteRenderer.color = Color.green;
                regenStaminaStatus = false;
            }
        }
        progressionManager();
    }

    private void RegainStaminaStatus()
    {
        stamina += staminaRegain * Time.deltaTime;
        stamina = stamina > maxStamina ? maxStamina : stamina;

    }

    private void MoveRandomDirection()
    {
        if (stamina > 0f)
        {
            rotateMethod();
            float moveDirection = oppositeMoveCycle ? -1 : 1;
 
            float toMove = moveDirection * distance * moveSpeed * stamina / exertionlevel * Time.deltaTime;
            transform.position += transform.up * toMove;
            stamina -= staminaLoss * Time.deltaTime;
            coveredDistance = oppositeMoveCycle ? coveredDistance - toMove : coveredDistance + toMove;
        }
        if (coveredDistance > distance)
        {
            moveCycle = false;
            m_SpriteRenderer.color = Color.green;
            coveredDistance = 0f;
            oppositeMoveCycle = false;
        }
    }

    void rotateMethod()
    {
        if (rotateOncePressed == false)
        {
            float randomVal = Random.Range(0f, 360f);
        transform.Rotate(0f, 0f, randomVal );
            rotateOncePressed = true;
            spriteObject.transform.Rotate(0f,0f, -randomVal);
        }
    }

    void progressionManager()
    {
       
        if (moveCycle && stamina < exertionlevel) {     
            if(growthPoints < 1f) { growthPoints = 1f; }
            growthPoints = growthPoints + baseGrowthPoints * Time.deltaTime;
            growthBarUI.SetValue(growthPoints);


        } else if (!moveCycle && !regenStaminaStatus && growthPoints > 0f)
        {
            if (growthPoints > 1f)
            {
                //// apply points
                //float gPoints = growthPoints * growthApplySpeed * Time.deltaTime;
                //growthPoints -= gPoints;
                //maxStamina += gPoints;
                // change progression
                if(maxStamina + growthPoints >= maxStaminaNextBracketProgression)
                {
                    
                    applyNextProgression();
                }
            }
        }
    }

    void applyNextProgression()
    {
        stamina += growthPoints;
        maxStamina = maxStaminaNextBracketProgression;
        growthPoints = 0f;
        maxStaminaNextBracketProgression = maxStaminaNextBracketProgression + (maxStaminaNextBracketProgression *maxStaminaNextBracketFactor);
        baseGrowthPoints = baseGrowthPoints + (baseGrowthPoints *  growthPointsFactor);
        staminaRegain = staminaRegain + (staminaRegain * staminaRegainFactor);
        staminaLoss = staminaLoss+ ( staminaLoss * staminaLossFactor);
        exertionlevel = exertionlevel+ ( exertionlevel * exertionFactor);
        staminaMinRegainLimit = staminaMinRegainLimit + (staminaMinRegainLimit * staminaMinRegainLimitFactor);
        growthBarUI.SetMaxValue(maxStaminaNextBracketProgression - maxStamina);
        growthBarUI.SetValue(growthPoints);
        string level = levelTextUI.getTextValue();
        levelTextUI.setTextValue((int.Parse(level) + 1).ToString());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("I collided");
        oppositeMoveCycle = !oppositeMoveCycle;
        coveredDistance = distance - 0.5f;

    }
    
    void OnGUI()
    {
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();
    }

}
