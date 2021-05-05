using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BeanRiceHeavenLobbyManager : MonoBehaviourPunCallbacks
{
    private string _gameVersion = "1.00";
    private RoomInfo[] _rooms;

    public GameObject joinButton;
    public Text connectionInfoText;

    private void Awake()
    {
        if (!joinButton || !connectionInfoText)
            throw new Exception("Public Objects Link Error !!!");
        ChangeStateJoinButton(false);
        connectionInfoText.text = "NULL";
        connectionInfoText.color = Color.red;
    }

    void Start()
    {
        PhotonNetwork.GameVersion = _gameVersion;
        PhotonNetwork.ConnectUsingSettings();
        
        connectionInfoText.text = "접속 시도중";
        connectionInfoText.color = Color.yellow;
    }

    void ChangeStateJoinButton(bool value)
    {
        Color32 c = ((value) ? (new Color32(86, 135, 225, 255)) : (new Color32(95, 95, 95, 130)));
        joinButton.GetComponent<Image>().color = c;
        joinButton.GetComponent<Button>().interactable = value;
    }

    public override void OnConnectedToMaster() 
    {     
        Debug.Log("Player has connected to the Photon Master Server"); 
        ChangeStateJoinButton(true);
        
        
        connectionInfoText.text = "접속 완료";
        connectionInfoText.color = Color.green;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnJoinButtonClicked()
    {
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions() { IsOpen = true, IsVisible = true, MaxPlayers = 4};
        PhotonNetwork.JoinOrCreateRoom("Room" + randomRoomName, roomOptions, TypedLobby.Default);
    }

    // public override void OnJoinRandomFailed(short returnCode, string message)
    // {
    //     Debug.Log("Tried to join a random game but failed. There must be no open games available");
    //     int randomRoomName = Random.Range(0, 10000);
    //     RoomOptions roomOptions = new RoomOptions() { IsOpen = true, IsVisible = true, MaxPlayers = 4};
    // }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create a new room but failed, there must already be a room with the same name");
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions() { IsOpen = true, IsVisible = true, MaxPlayers = 4};
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Call OnJoinedRoom()");
        
        connectionInfoText.text = "방 접속";
        connectionInfoText.color = Color.green;
        
        PhotonNetwork.LoadLevel("Light");
    }
}
