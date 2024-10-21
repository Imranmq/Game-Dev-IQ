using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    GameObject mainParent;
    // Start is called before the first frame update
    void Start()
    {
        
        mainParent = transform.parent.parent.parent.gameObject;
      
        if(mainParent != null)
        {
        
            mainParent.GetComponent<RaycastService>().setHandItemGameObject(gameObject);
        }        
    }
    public void setSprite(Sprite sprite )
    {
        Debug.Log("Set Sprite", sprite);
        SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
        spr.sprite = sprite;
    }
}
