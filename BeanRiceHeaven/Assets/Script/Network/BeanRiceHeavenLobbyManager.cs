using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Random = UnityEngine.Random;

public class BeanRiceHeavenLobbyManager : MonoBehaviourPunCallbacks
{
    private static BeanRiceHeavenLobbyManager instance = null;

    public static BeanRiceHeavenLobbyManager Instance
    {
        get
        {
            if (!instance)
                return null;
            return instance;
        }
    }
    
    private string _gameVersion = "1.00";
    private RoomInfo[] _rooms;

    [Header("- Lobby Panel")] 
    public GameObject lobbyPanel;
    public GameObject joinButton;
    public InputField nickNameInputField;
    
    [Header("- Inside Room Panel")] 
    public GameObject roomPanel;
    public GameObject readyButton;
    public GameObject startGameButton;
    public GameObject insideRoomPlayerPrefab;

    private Dictionary<int, GameObject> _insideRoomPlayerEntries;
    public Dictionary<int, GameObject> InsideRoomPlayerEntries => _insideRoomPlayerEntries;


    private void Awake()
    {
        if (!joinButton)
            throw new Exception("Public Objects Link Error !!!");

        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        PhotonNetwork.GameVersion = _gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    void Update()
    {
        if (!joinButton)
            return;
        
        if (nickNameInputField.text.Length == 0)
        {
            joinButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            joinButton.GetComponent<Button>().interactable = true;
        }
    }
    
    public override void OnConnectedToMaster() 
    {     
        Debug.Log("Player has connected to the Photon Master Server"); 
        joinButton.GetComponent<Button>().interactable = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnJoinButtonClicked()
    {
        PhotonNetwork.LocalPlayer.NickName = nickNameInputField.text;
        
        int randomRoomName = 1;
        RoomOptions roomOptions = new RoomOptions() { IsOpen = true, IsVisible = true, MaxPlayers = 4};

        PhotonNetwork.JoinOrCreateRoom("Room" + randomRoomName, roomOptions, TypedLobby.Default);
    }

    public void OnExitRoomButtonClicked()
    {
        PhotonNetwork.LeaveRoom();
    }
    
    #region ROOM_FUNCTION
    
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create a new room but failed, there must already be a room with the same name");
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions() { IsOpen = true, IsVisible = true, MaxPlayers = 4};
        PhotonNetwork.JoinOrCreateRoom("Room" + randomRoomName, roomOptions, TypedLobby.Default);
    }
    
    public override void OnJoinedRoom()
    {
        roomPanel.SetActive(true);
        lobbyPanel.SetActive(false);

        if (_insideRoomPlayerEntries == null)
        {
            _insideRoomPlayerEntries = new Dictionary<int, GameObject>();
        }
        
        
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            GameObject enterPlayer = Instantiate(insideRoomPlayerPrefab, roomPanel.transform, false);
            InsideRoomPlayerInfo entryPlayerInfo = enterPlayer.GetComponent<InsideRoomPlayerInfo>();
            
            enterPlayer.transform.localPosition = new Vector3(-1000 + (p.ActorNumber) * 400, 0, 0);  
            entryPlayerInfo.InitRoomPlayer(p.ActorNumber, p.NickName);

            object readyData;
            if (p.CustomProperties.TryGetValue("ReadyStatus", out readyData))
            {
                entryPlayerInfo.SetPlayerReady((bool)readyData);
            }

            _insideRoomPlayerEntries.Add(p.ActorNumber, enterPlayer);
        }
    }

    public override void OnLeftRoom()
    {

        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);

        foreach (GameObject p in _insideRoomPlayerEntries.Values)
        {
            Destroy(p.gameObject);
        }
        
        _insideRoomPlayerEntries.Clear();
        _insideRoomPlayerEntries = null;
    }
    
    public void PressGameStart()
    {
        SceneManager.LoadScene("BackgroundTest");
    }
    
    private void PlayerNumberingCheck(Player p)
    {
        Player[] pp = PlayerNumbering.SortedPlayers;
    }
    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GameObject enterPlayer = Instantiate(insideRoomPlayerPrefab, roomPanel.transform, false);
        InsideRoomPlayerInfo entryPlayerInfo = enterPlayer.GetComponent<InsideRoomPlayerInfo>();
        Debug.Log(newPlayer.NickName + " : " + newPlayer.GetPlayerNumber());
        enterPlayer.transform.localPosition = new Vector3(-1000 + (newPlayer.ActorNumber) * 400, 0, 0);  
        entryPlayerInfo.InitRoomPlayer(newPlayer.ActorNumber, newPlayer.NickName);
        
        _insideRoomPlayerEntries.Add(newPlayer.ActorNumber, enterPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Destroy(_insideRoomPlayerEntries[otherPlayer.ActorNumber].gameObject);
        _insideRoomPlayerEntries.Remove(otherPlayer.ActorNumber);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (_insideRoomPlayerEntries == null)
        {
            _insideRoomPlayerEntries = new Dictionary<int, GameObject>();
        }
        
        GameObject targetPlayerInfo;
        if (_insideRoomPlayerEntries.TryGetValue(targetPlayer.ActorNumber, out targetPlayerInfo))
        {
            object readyData;
            if (changedProps.TryGetValue("ReadyStatus", out readyData))
            {
                targetPlayerInfo.GetComponent<InsideRoomPlayerInfo>().SetPlayerReady((bool)readyData);
            }
        }
    }

    #endregion
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
