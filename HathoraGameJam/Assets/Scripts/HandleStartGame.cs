using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HandleStartGame : NetworkBehaviour
{
    [SerializeField] GameObject TimerUICanvas;
    [SerializeField] TextMeshProUGUI PlayerJoinedText;
    [SerializeField] Button readyGameButton;
    Dictionary<ulong, bool> playerReadyDictionary;
    Dictionary<ulong, string> playerNameDictionary;

    void Start()
    {
        readyGameButton.onClick.AddListener(ReadyGameButton);
   //     Time.timeScale = 0;
        playerReadyDictionary = new Dictionary<ulong, bool>();
        playerNameDictionary = new Dictionary<ulong, string>();
      
    }

    public override void OnNetworkSpawn()
    {
        if (IsClient && IsOwner)
        {
            SetNameServerRpc();
        }
    }


    [ServerRpc]
    public void StartGameServerRpc()
    {
        StartGame();
        StartGameClientRpc();
    }

    [ClientRpc]
    private void StartGameClientRpc()
    {
        StartGame();
    }

    public void StartGame()
    {
        ActiveTimer();
        Time.timeScale = 1;
    }

    public void ActiveTimer()
    {
        TimerUICanvas.SetActive(true);
    }

    public void ReadyGameButton()
    {
        ReadyGameServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void ReadyGameServerRpc(ServerRpcParams serverRpcParams=default)
    {
        playerReadyDictionary[serverRpcParams.Receive.SenderClientId] = true;
        SetNameReadyServerRpc();
    }
    

    [ServerRpc(RequireOwnership = false)]
    private void SetNameServerRpc(ServerRpcParams serverRpcParams = default)
    {
        string namePlayer = "Player" + serverRpcParams.Receive.SenderClientId;  //TODO = PlayerPrefs.GetString("Name);
        playerNameDictionary[serverRpcParams.Receive.SenderClientId] = namePlayer;
        SetNameClientRpc(namePlayer, serverRpcParams.Receive.SenderClientId);
    }


    [ServerRpc(RequireOwnership = false)]
    private void SetNameReadyServerRpc(ServerRpcParams serverRpcParams = default)
    {
        string namePlayer = "Player" + serverRpcParams.Receive.SenderClientId + "is Ready";  //TODO = PlayerPrefs.GetString("Name);
        playerNameDictionary[serverRpcParams.Receive.SenderClientId] = namePlayer;
        SetNameClientRpc(namePlayer, serverRpcParams.Receive.SenderClientId);
    }


    [ClientRpc]
    private void SetNameClientRpc(string namePlayer, ulong playerId)
    {
        playerNameDictionary[playerId] = namePlayer; //TODO = PlayerPrefs.GetString("Name);
        UpdateListOfPlayer( );
    }

    private void UpdateListOfPlayer( )
    {
        List<string> playerNames = new List<string>();
        //string result= "";
        foreach (KeyValuePair<ulong, string> entry in playerNameDictionary)
        {


            playerNames.Add(entry.Value);

        }

        PlayerJoinedText.text = string.Join("\n", playerNames);
    }


    //[ServerRpc(RequireOwnership = false)]
    //private void NotReadyGameServerRpc(ServerRpcParams serverRpcParams)
    //{
    //    playerReadyDictionary[serverRpcParams.Receive.SenderClientId] = false;
    //}

    // Update is called once per frame
    void Update()
    {

    }
}
