using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class RoomPollingNetwork : NetworkBehaviour
{
    // Start is called before the first frame update

    [SerializeField] TextMeshProUGUI OfficeNameText;

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

        //if (IsClient && IsOwner)
        //{
        //SetName();
        //}
    }

    public void SetName()
    {
        SetNameServerRpc(PlayerPrefs.GetString("Name"));
    }

    public void Try()
    {
        TryServerRpc( );
    }

    [ServerRpc(RequireOwnership =false)]
    private void TryServerRpc()
    {
        TryClientRpc();
    }

    [ClientRpc]
    private void TryClientRpc()
    {
        Debug.Log("try");
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }

    public override void OnNetworkSpawn()
    {
        //if (IsClient && IsOwner)
        //{
        //    SetNameServerRpc(PlayerPrefs.GetString("Name"));
        //}
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
        Time.timeScale = 1;
    }

  

    public void ReadyGameButton()
    {
        ReadyGameServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void ReadyGameServerRpc(ServerRpcParams serverRpcParams = default)
    {
        playerReadyDictionary[serverRpcParams.Receive.SenderClientId] = true;
        SetNameReadyServerRpc(PlayerPrefs.GetString("Name"));
    }


    [ServerRpc(RequireOwnership = false)]
    private void SetNameServerRpc(string playername, ServerRpcParams serverRpcParams = default)
    {
        string namePlayer = playername + " " + serverRpcParams.Receive.SenderClientId;  //TODO = PlayerPrefs.GetString("Name);
        playerNameDictionary[serverRpcParams.Receive.SenderClientId] = namePlayer;
        SetNameClientRpc(namePlayer, serverRpcParams.Receive.SenderClientId);
    }


    [ServerRpc(RequireOwnership = false)]
    private void SetNameReadyServerRpc(string playername, ServerRpcParams serverRpcParams = default)
    {
        string namePlayer = playername + " " + serverRpcParams.Receive.SenderClientId + " is Ready";  //TODO =;
        playerNameDictionary[serverRpcParams.Receive.SenderClientId] = namePlayer;
        SetNameClientRpc(namePlayer, serverRpcParams.Receive.SenderClientId);
    }


    [ClientRpc]
    private void SetNameClientRpc(string namePlayer, ulong playerId)
    {
        playerNameDictionary[playerId] = namePlayer; //TODO = PlayerPrefs.GetString("Name);
        UpdateListOfPlayer();
    }

    private void UpdateListOfPlayer()
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
 
    public void SetOfficeName(string officeName)
    {
      OfficeNameText.text = officeName; //Playerprefs.getstring("Name");
    }

}
