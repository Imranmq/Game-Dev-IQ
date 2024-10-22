using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;
using Sfs2X.Entities.Variables;
using Sfs2X.Requests;
using Sfs2X.Logging;
public class GameManager : MonoBehaviour
{
    public GameObject[] playerModels;


    private SmartFox sfs;

    public GameObject localPlayerPrefab;
    public GameObject remotePlayerPrefab;
    public GameObject remoteProjectilePrefab;
    private GameObject localPlayerInstance;
   // private PlayerController localPlayerController;
    private Dictionary<SFSUser, GameObject> remotePlayers = new Dictionary<SFSUser, GameObject>();

    private OurMovementActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new OurMovementActions();
        playerInputActions.gameActions.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!SmartFoxConnection.IsInitialized)
        {
           SceneManager.LoadScene("MultiplayerScreen");
            return;
        }

        sfs = SmartFoxConnection.Connection;
      //  sfs.AddEventListener(SFSEvent.OBJECT_MESSAGE, OnObjectMessage);
        sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
        sfs.AddEventListener(SFSEvent.USER_VARIABLES_UPDATE, OnUserVariableUpdate);
        sfs.AddEventListener(SFSEvent.USER_EXIT_ROOM, OnUserExitRoom);
        sfs.AddEventListener(SFSEvent.USER_ENTER_ROOM, OnUserEnterRoom);

        SpawnLocalPlayer(localPlayerPrefab);

    }
    public void OnUserVariableUpdate(BaseEvent evt)
    {
       // Debug.Log("User Update Received");
        List<string> changedVars = (List<string>)evt.Params["changedVars"];

        SFSUser user = (SFSUser)evt.Params["user"];

        if (user == sfs.MySelf) return;

        if (!remotePlayers.ContainsKey(user))
        {       // New client just started transmitting - lets create remote player
            Vector3 pos = new Vector3(0, 1, 0);
            if (user.ContainsVariable("t_x") && user.ContainsVariable("t_y") && user.ContainsVariable("t_z"))
            {
                pos.x = (float)user.GetVariable("t_x").GetDoubleValue();
                pos.y = (float)user.GetVariable("t_y").GetDoubleValue();
                pos.z = (float)user.GetVariable("t_z").GetDoubleValue();
            }
            float rotAngle = 0;
            if (user.ContainsVariable("t_rot"))
            {
                rotAngle = (float)user.GetVariable("t_rot").GetDoubleValue();
            }            
            SpawnRemotePlayer(user, pos, Quaternion.Euler(0, rotAngle, 0));

        }

        if (changedVars.Contains("ms_x")){
            GameObject remotePlayer = remotePlayers[user];
            MovementScripto mScript = remotePlayer.GetComponent<MovementScripto>();           
            mScript.SetTankRotateStates((float)user.GetVariable("ms_x").GetDoubleValue(), out _);
        }
        if (changedVars.Contains("ms_y"))
        {
            GameObject remotePlayer = remotePlayers[user];
            MovementScripto mScript = remotePlayer.GetComponent<MovementScripto>();            
            mScript.SetMoveStates((float)user.GetVariable("ms_y").GetDoubleValue(), out _);
        }
        if (changedVars.Contains("ch_r"))
        {
            GameObject remotePlayer = remotePlayers[user];
            MovementScripto mScript = remotePlayer.GetComponent<MovementScripto>();                          
            mScript.CannonHead.transform.localRotation = Quaternion.Euler(0, (float)user.GetVariable("ch_r").GetDoubleValue(), 0);
        }
        if (changedVars.Contains("c_m"))
        {
            GameObject remotePlayer = remotePlayers[user];
            MovementScripto mScript = remotePlayer.GetComponent<MovementScripto>();
            mScript.SetTankHeadUpDownState((float)user.GetVariable("c_m").GetDoubleValue(), out _);
        }
        if (changedVars.Contains("M_c1"))
        {
            GameObject remotePlayer = remotePlayers[user];
            MovementScripto mScript = remotePlayer.GetComponent<MovementScripto>();
            mScript.SetTankShoot((float)user.GetVariable("M_c1").GetDoubleValue(), out _);
        }

    }
    
    public void OnUserEnterRoom(BaseEvent evt)
    {
        Debug.Log("User joined");
        // User joined - and we might be standing still (not sending position updates); so let's send him our position
        if (localPlayerInstance != null)
        {

            SendInitialState();
        }
    }
    public void SendInitialState()
    {
        List<UserVariable> userVariables = new List<UserVariable>();
        userVariables.Add(new SFSUserVariable("t_x", (double)localPlayerInstance.transform.position.x));
        userVariables.Add(new SFSUserVariable("t_y", (double)localPlayerInstance.transform.position.y));
        userVariables.Add(new SFSUserVariable("t_z", (double)localPlayerInstance.transform.position.z));
        userVariables.Add(new SFSUserVariable("t_rot", (double)localPlayerInstance.transform.rotation.eulerAngles.y));
        sfs.Send(new SetUserVariablesRequest(userVariables));
    }
    public void SpawnLocalPlayer(GameObject player)
    {
        if(player != null)
        {
            Debug.Log("Initialize Local Player");
            localPlayerInstance = Instantiate(player);
            localPlayerInstance.GetComponent<MovementScripto>().PlayerCheck = true;
        }

    }
    private void SpawnRemotePlayer(SFSUser user, Vector3 pos, Quaternion rot)
    {
        // See if there already exists a model so we can destroy it first
        if (remotePlayers.ContainsKey(user) && remotePlayers[user] != null)
        {
            Destroy(remotePlayers[user]);
            remotePlayers.Remove(user);
        }

        // Lets spawn our remote player model
        GameObject remotePlayer = Instantiate(remotePlayerPrefab, pos, rot);
        MovementScripto movScript = remotePlayer.GetComponent<MovementScripto>();
        movScript.PlayerCheck = false;
        movScript.bullet = remoteProjectilePrefab;
        remotePlayer.layer = 6;
        // Lets track the dude
        remotePlayers.Add(user, remotePlayer);
    }
    public void OnUserExitRoom(BaseEvent evt)
    {
        // Someone left - lets make certain they are removed if they didn't nicely send a remove command
        SFSUser user = (SFSUser)evt.Params["user"];
        RemoveRemotePlayer(user);
    }
    public void OnConnectionLost(BaseEvent evt)
    {
        // Reset all internal states so we kick back to login screen
        sfs.RemoveAllEventListeners();
        SceneManager.LoadScene("Connection");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (sfs != null)
        {
            sfs.ProcessEvents();
            
        }
        float spaceKey = playerInputActions.gameActions.SpawnPlayer.ReadValue<float>();
        if (spaceKey != 0) {
            if (localPlayerInstance != null && localPlayerInstance.activeSelf == false)
            {
                localPlayerInstance.SetActive(true);
                localPlayerInstance.GetComponent<healthScript>().health = 50f;
                SendInitialState();
            }
        }
    }

    private void RemoveRemotePlayer(SFSUser user)
    {
        if (user == sfs.MySelf) return;

        if (remotePlayers.ContainsKey(user))
        {
            Destroy(remotePlayers[user]);
            remotePlayers.Remove(user);
        }
    }
}
