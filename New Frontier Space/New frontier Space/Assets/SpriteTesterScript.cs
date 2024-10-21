using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTesterScript : MonoBehaviour
{
    public Sprite Earth;
    // Start is called before the first frame update
    void Start()
    {
        Sprite spr =Resources.Load<Sprite>("Earth") as Sprite;
        gameObject.GetComponent<SpriteRenderer>().sprite = spr;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
