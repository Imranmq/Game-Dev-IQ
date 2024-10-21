using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonManager : Photon.MonoBehaviour
{
  
    private float timer;
    private float resetPingTimer = 3;
    // Start is called before the first frame update
    void Start()
    {
        timer = resetPingTimer;
        if (PhotonNetwork.IsConnected)
        {
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings("1.0");
        
            Debug.Log("Connecting");
        }

    }
    void OnJoinedLobby()
    {
        Debug.Log("JoinedLobby");
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions() { MaxPlayers = 2 }, TypedLobby.Default);
    }
    void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        GameObject gameObject = GameObject.FindGameObjectWithTag("Catcher");
        Debug.Log(gameObject);
        if (gameObject != null)
        {
            PhotonNetwork.Instantiate("Runner", new Vector3(0f, 10f, 0f), Quaternion.identity, 0);
            
        }
        else
        {
            PhotonNetwork.Instantiate("Catcher", new Vector3(0f, 10f, 0f), Quaternion.identity, 0);
        }
        //Debug.Log("Joined Room");
        //GameObject gameObject = GameObject.Find("Catcher Main");
        //if (gameObject != null)
        //{
        //    GameObject catcher = PhotonNetwork.Instantiate("Catcher", new Vector3(0f, 10f, 0f), Quaternion.identity, 0);
        //    catcher.name = "Catcher Main";
        //}
        //else
        //{
        //    PhotonNetwork.Instantiate("Runner", new Vector3(0f, 10f, 0f), Quaternion.identity, 0);
        //}

    }
    void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions() { MaxPlayers = 2 }, TypedLobby.Default);
    }
    private void Update()
    {
        if (timer < 0)
        {
            Debug.Log(PhotonNetwork.networkingPeer.RoundTripTime);
            timer = resetPingTimer;
        }
        timer -= Time.deltaTime;
    }
}
