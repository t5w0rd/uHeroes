﻿using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

public class RoomUI : MonoBehaviour, INetworkable<GamePlayerController> {

    public List<GameObject> m_playerSlots;

    public Button m_start;

    // Use this for initialization
    void Start () {
        if (isServer)
        {
            m_start.enabled = true;
            m_start.GetComponentInChildren<Text>().text = "Start";
        }
        else
        {
            m_start.enabled = false;
            m_start.GetComponentInChildren<Text>().text = "Waiting for server";
        }
    }

    public void OnStartClick()
    {
        Debug.Assert(isServer);
        GameNetworkDiscovery.singleton.StopBroadcast();
        GameController.instance.ServerResetPlayersReady();
        localClient.RpcStart();
    }

    // INetworkable
    public GamePlayerController localClient
    {
        get
        {
            return GamePlayerController.localClient;
        }
    }

    public bool isServer
    {
        get
        {
            return GameController.instance.isServer;
        }
    }
}
