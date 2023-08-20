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
    [SerializeField] Button StartGameButton;
    [SerializeField] Button refreshGameButton;
    [SerializeField] GameObject thirdPanel;
    [SerializeField] GameObject ScenePrefabManager;
    Dictionary<ulong, bool> playerReadyDictionary;
  public  Dictionary<ulong, string> playerNameDictionary;

    void Start()
    {
        readyGameButton.onClick.AddListener(ReadyGameButton);
        StartGameButton.onClick.AddListener(StartGame);
        refreshGameButton.onClick.AddListener(RefreshNames);
        //     Time.timeScale = 0;
        playerReadyDictionary = new Dictionary<ulong, bool>();
        playerNameDictionary = new Dictionary<ulong, string>();


    }

    private void RefreshNames()
    {
        SetNameServerRpc(PlayerPrefs.GetString("Name"));
    }
  

    [ServerRpc(RequireOwnership = false)]
    public void StartGameServerRpc()
    {
        bool areAllPlayerReady = CheckDictionary();

      
        if (areAllPlayerReady)
        {
            StartGameOfficially();
            StartGameClientRpc();
        }
        else
        {
            CantStartGameClientRpc();
        }
    }

    [ClientRpc]
    private void CantStartGameClientRpc()
    {
        Debug.Log("Not every player are ready");
    }

    private bool CheckDictionary()
    {
        bool allTrue = true;

        foreach (var kvp in playerReadyDictionary)
        {
            if (!kvp.Value)
            {
                allTrue = false;
                break;
            }
        }

        return allTrue;
    }

    [ClientRpc]
    private void StartGameClientRpc()
    {
        StartGameOfficially();
    }

    private void StartGameOfficially()
    {
        ScenePrefabManager.GetComponent<ScenePrefabManager>().StartGame();
    }

    public void StartGame()
    {
        //      Time.timeScale = 1;
        Debug.Log("TODO: implement change scene but check if every player is ready");
        StartGameServerRpc();
    }



    public void ReadyGameButton()
    {
        if (PlayerPrefs.GetString("Name").Contains("is Ready"))
        {
            ReadyGameServerRpc(PlayerPrefs.GetString("Name"));

        }
        else
        {
            PlayerPrefs.SetString("Name", PlayerPrefs.GetString("Name") + " is Ready");
            ReadyGameServerRpc(PlayerPrefs.GetString("Name"));
        }

    }

    [ServerRpc(RequireOwnership = false)]
    private void ReadyGameServerRpc(string playerName, ServerRpcParams serverRpcParams = default)
    {
        playerNameDictionary[serverRpcParams.Receive.SenderClientId] = playerName;
        string listOfPlayer = UpdateListOfPlayer();
        SetNameClientRpc(listOfPlayer, serverRpcParams.Receive.SenderClientId, playerName);
    }


    [ServerRpc(RequireOwnership = false)]
    private void SetNameServerRpc(string playername, ServerRpcParams serverRpcParams = default)
    {
        string namePlayer = playername + " " + serverRpcParams.Receive.SenderClientId;  //TODO = PlayerPrefs.GetString("Name);
        playerNameDictionary[serverRpcParams.Receive.SenderClientId] = namePlayer;
        string listOfPlayer = UpdateListOfPlayer();
        SetNameClientRpc(listOfPlayer, serverRpcParams.Receive.SenderClientId, namePlayer);
    }

    [ClientRpc]
    private void SetNameClientRpc(string listOfPlayer, ulong senderClientId, string namePlayer)
    {
        playerNameDictionary[senderClientId] = namePlayer;
        PlayerJoinedText.text = listOfPlayer;
    }

    private string UpdateListOfPlayer()
    {
        List<string> playerNames = new List<string>();
        //string result= "";
        foreach (KeyValuePair<ulong, string> entry in playerNameDictionary)
        {


            playerNames.Add(entry.Value);

        }
        PlayerJoinedText.text = string.Join("\n", playerNames);
        return PlayerJoinedText.text;
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
