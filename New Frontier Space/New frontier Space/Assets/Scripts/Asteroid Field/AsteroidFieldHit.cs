using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsteroidFieldHit : MonoBehaviour
{
    private void OnTriggerEnter2D()
    {
        Debug.Log("Asteroid Field Triggered");
        SceneManager.LoadScene(1);
    }
}
