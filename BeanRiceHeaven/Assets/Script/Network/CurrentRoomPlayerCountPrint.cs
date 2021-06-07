using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class CurrentRoomPlayerCountPrint : MonoBehaviour
{
    private Text _playerCountText;

    private void Awake()
    {
        _playerCountText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerCountText.text = PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }
}
