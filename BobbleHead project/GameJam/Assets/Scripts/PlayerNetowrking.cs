using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetowrking : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] scriptsToIgnore;
    PhotonView pView;
    // Start is called before the first frame update
    void Start()
    {
        pView = GetComponent<PhotonView>();
        Initialize();
    }

    void Initialize()
    {
        if (!pView.isMine)
        {
            foreach(MonoBehaviour item in scriptsToIgnore)
            {
                item.enabled = false;
            }
        }
    }
}
