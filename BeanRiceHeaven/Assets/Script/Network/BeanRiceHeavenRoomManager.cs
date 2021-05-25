using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class BeanRiceHeavenRoomManager : MonoBehaviourPunCallbacks
{
    [Header("Panel")] 
    public GameObject roomPanel;
    
    [Header("Room Info Text")]
    public Text currRoomNameText;
    public Text maxPlayersText;

    [Header("Player Prefab")] 
    public GameObject roomPlayerPrefab;
    
    void Awake()
    {
        currRoomNameText.text = PhotonNetwork.CurrentRoom.Name;
        
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            GameObject entryPlayer = Instantiate(roomPlayerPrefab, roomPanel.transform, false);
            Text entryPlayerNickname = entryPlayer.transform.Find("NickName Text").GetComponent<Text>();
            GameObject entryPlayerRenderObject = entryPlayer.transform.Find("Player Render Object").gameObject;
            
            entryPlayer.transform.localPosition += new Vector3(-1000 + p.ActorNumber * 400, 0, 0);  
            entryPlayerNickname.text = p.NickName;
            
            if (!Equals(p, PhotonNetwork.LocalPlayer))
            {
                entryPlayerNickname.color = new Color32(176, 59, 69, 255);
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        maxPlayersText.text = PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("call");
        int i = 0;
    }
}
