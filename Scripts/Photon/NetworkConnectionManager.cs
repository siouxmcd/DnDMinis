using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class NetworkConnectionManager : MonoBehaviourPunCallbacks {

    public static NetworkConnectionManager netMan;

    public Button CancelButton;
    public Button ConnectRoom;

    private void Awake()
    {
        netMan = this; //creates a singleton, lives within the Menu scene
    }

    // Use this for initialization
    void Start () {
        PhotonNetwork.ConnectUsingSettings();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void OnClickCancel()
    {
        Debug.Log("Cancelling connection");
        ConnectRoom.gameObject.SetActive(true);
        CancelButton.gameObject.SetActive(false);

        PhotonNetwork.LeaveRoom();
    }

    public void OnClickConnectToRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        Debug.Log("Trying to connect to room");
        ConnectRoom.gameObject.SetActive(false);
        CancelButton.gameObject.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }



    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to joing random room. Creating new room.");
        base.OnJoinRandomFailed(returnCode, message);
        //no room available
        //create a room (null as a name means 'does not matter', fine for debugging)

        CreateRoom();
    }

    void CreateRoom()
    {
        Debug.Log("Trying to create room");
        int randRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)MultiplayerSettings.multiplayerSettings.maxPlayers };

        PhotonNetwork.CreateRoom("Room " + randRoomName, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room. Trying again.");
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log(message);

        CreateRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log(cause);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        ConnectRoom.gameObject.SetActive(true);
        Debug.Log("Connected to Master");

        PhotonNetwork.AutomaticallySyncScene = true;
    }
}
