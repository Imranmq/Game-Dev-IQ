using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerObj;
    GameObject playerInstance;
    public int numLives = 4;
    float respawntimer;
    int health;
    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();

    }
    void SpawnPlayer()
    {
        numLives--;
        playerInstance = (GameObject)Instantiate(playerObj, transform.position, Quaternion.identity);
        playerInstance.name = "PlayerShip";
        respawntimer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInstance == null && numLives > 0)
        {
            respawntimer -= Time.deltaTime;
            if (respawntimer <= 0)
            {
                SpawnPlayer();
            } 
        }
        if (Input.GetButton("Submit"))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetButton("Cancel"))
        {
            Application.Quit();
        }
    }
    public bool setHealth(int healthLeft)
    {
        health = healthLeft;
        return true;
    }
    private void OnGUI()
    {
        if (numLives > 0 || playerInstance != null) {
            GUI.Label(new Rect(0, 0, 100, 50), "Lives Left : " + numLives);
            GUI.Label(new Rect(0, 50, 100, 50), "Health : " + health);
        }
        else
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 150, 100), "Game Over. Press Enter to Restart. Press Escape To Quit");
        }
    }
}
