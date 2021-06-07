using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionStatusPrint : MonoBehaviour
{
    private Text _connectionStatusText;

    void Awake()
    {
        _connectionStatusText = GetComponent<Text>();
    }
    void Update()
    {
        _connectionStatusText.text = "Your Network Status: " + PhotonNetwork.NetworkClientState;
    }
}
