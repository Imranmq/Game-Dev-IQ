using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Sfs2X;
using Sfs2X.Logging;
using Sfs2X.Util;
using Sfs2X.Core;
using Sfs2X.Entities;
using UnityEngine.SceneManagement;
using Sfs2X.Requests;

public class Connector : MonoBehaviour
{

	//----------------------------------------------------------
	// UI elements
	//----------------------------------------------------------

	public InputField hostInput;
	public InputField portInput;

	public Button button;
	public Text buttonLabel;
	public Text DebugInfo;

	//----------------------------------------------------------
	// Private properties
	//----------------------------------------------------------

	private string defaultHost = "127.0.0.1";   // Default host
	private int defaultTcpPort = 9933;          // Default TCP port
	private int defaultWsPort = 8080;           // Default WebSocket port

	private SmartFox sfs;



	//----------------------------------------------------------
	// Unity calback methods
	//----------------------------------------------------------

	void Start()
	{
		// Initialize UI
		hostInput.text = defaultHost;

#if !UNITY_WEBGL
		//	portInput.text = defaultTcpPort.ToString();
#else
		 defaultTcpPort = defaultWsPort;
#endif
		//OnButtonClick();


	}

	void Update()
	{
		// As Unity is not thread safe, we process the queued up callbacks on every frame
		if (sfs != null)
			sfs.ProcessEvents();
	}

	void OnApplicationQuit()
	{
		// Always disconnect before quitting
		if (sfs != null && sfs.IsConnected)
			sfs.Disconnect();
	}

	//----------------------------------------------------------
	// Public interface methods for UI
	//----------------------------------------------------------

	public void OnButtonClick()
	{
		Debug.Log("Clicked");
		if (sfs == null || !sfs.IsConnected)
		{

			// CONNECT

			// Enable interface
			enableInterface(false);

			// Clear console
		

			trace("Now connecting...");

			// Initialize SFS2X client and add listeners
			// WebGL build uses a different constructor
			#if !UNITY_WEBGL
			sfs = new SmartFox();
			#else
			sfs = new SmartFox(UseWebSocket.WS_BIN);
			#endif

			sfs.AddEventListener(SFSEvent.CONNECTION, OnConnection);
			sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
			sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
			sfs.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
			sfs.AddEventListener(SFSEvent.ROOM_JOIN, OnRoomJoin);
			sfs.AddEventListener(SFSEvent.ROOM_JOIN_ERROR, OnRoomJoinError);
			sfs.AddLogListener(LogLevel.INFO, OnInfoMessage);
			sfs.AddLogListener(LogLevel.WARN, OnWarnMessage);
			sfs.AddLogListener(LogLevel.ERROR, OnErrorMessage);

			// Set connection parameters
			ConfigData cfg = new ConfigData();
			cfg.Host = hostInput.text;
			cfg.Port = Convert.ToInt32(defaultTcpPort);
			cfg.Zone = "BasicExamples";
			cfg.Debug = true;

			// Connect to SFS2X
			sfs.Connect(cfg);
		}
		else
		{

			// DISCONNECT

			// Disable button
			button.interactable = false;

			// Disconnect from SFS2X
			sfs.Disconnect();
		}
	}

	//----------------------------------------------------------
	// Private helper methods
	//----------------------------------------------------------

	private void enableInterface(bool enable)
	{
		hostInput.interactable = enable;
		//portInput.interactable = enable;
		

		button.interactable = enable;
		buttonLabel.text = "CONNECT";
	}

	private void trace(string msg)
	{
		Debug.Log(msg);
		DebugInfo.text = msg;
		Canvas.ForceUpdateCanvases();		
	}

	private void reset()
	{
		// Remove SFS2X listeners
		sfs.RemoveAllEventListeners();
		sfs = null;

		// Enable interface
		enableInterface(true);
	}

	//----------------------------------------------------------
	// SmartFoxServer event listeners
	//----------------------------------------------------------

	private void OnConnection(BaseEvent evt)
	{
		if ((bool)evt.Params["success"])
		{
			SmartFoxConnection.Connection = sfs;
			trace("Connection established successfully");
			trace("SFS2X API version: " + sfs.Version);
			trace("Connection mode is: " + sfs.ConnectionMode);
			// Login
			sfs.Send(new Sfs2X.Requests.LoginRequest("Imran" + new System.Random().Next(0,50).ToString() ));
		

			// Enable disconnect button
			button.interactable = true;
			buttonLabel.text = "DISCONNECT";
		}
		else
		{
			trace("Connection failed; is the server running at all?");

			// Remove SFS2X listeners and re-enable interface
			reset();
		}
	}

	private void OnConnectionLost(BaseEvent evt)
	{
		trace("Connection was lost; reason is: " + (string)evt.Params["reason"]);

		// Remove SFS2X listeners and re-enable interface
		reset();
	}
	private void OnLogin(BaseEvent evt)
	{
		string roomName = "Game Room";

		// We either create the Game Room or join it if it exists already
		if (sfs.RoomManager.ContainsRoom(roomName))
		{
			sfs.Send(new JoinRoomRequest(roomName));
		}
		else
		{
			RoomSettings settings = new RoomSettings(roomName);
			settings.MaxUsers = 40;
			sfs.Send(new CreateRoomRequest(settings, true));
		}
	}

	private void OnLoginError(BaseEvent evt)
	{
		// Disconnect
		sfs.Disconnect();

		// Remove SFS2X listeners and re-enable interface
		reset();

		// Show error message
		string message = "Login failed: " + (string)evt.Params["errorMessage"];
		ShowLogMessage("ERROR", message);
	}

	private void OnRoomJoin(BaseEvent evt)
	{
		// Remove SFS2X listeners and re-enable interface before moving to the main game scene
		reset();

		// Go to main game scene
		SceneManager.LoadScene("GameScene");
	}

	private void OnRoomJoinError(BaseEvent evt)
	{
		string message = "Room join failed: " + (string)evt.Params["errorMessage"];
		ShowLogMessage("ERROR", message);	 
	}



	//----------------------------------------------------------
	// SmartFoxServer log event listeners
	//----------------------------------------------------------

	public void OnInfoMessage(BaseEvent evt)
	{
		string message = (string)evt.Params["message"];
		ShowLogMessage("INFO", message);
	}

	public void OnWarnMessage(BaseEvent evt)
	{
		string message = (string)evt.Params["message"];
		ShowLogMessage("WARN", message);
	}

	public void OnErrorMessage(BaseEvent evt)
	{
		string message = (string)evt.Params["message"];
		ShowLogMessage("ERROR", message);
	}

	private void ShowLogMessage(string level, string message)
	{
		message = "[SFS > " + level + "] " + message;
		trace(message);
		Debug.Log(message);
	}
}
