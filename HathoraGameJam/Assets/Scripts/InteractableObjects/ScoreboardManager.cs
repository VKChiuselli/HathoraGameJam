using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;
public class ScoreboardManager : NetworkBehaviour
{


    private Dictionary<ulong, int> playerPointsDictionary;
   // [SerializeField] TextMeshProUGUI ScoreBoardUIText;
    [SerializeField] TextMeshProUGUI FinalScoreBoardText;
    [SerializeField] GameObject FinalScorePanel;
    [SerializeField] GameObject ScoreBoardPanel;

    private PlayerScoreUI[] playerScoreUIs = new PlayerScoreUI[4];


    public static ScoreboardManager Instance { get; private set; }

 

    private void Awake()
    {
        Instance = this;

        playerPointsDictionary = new Dictionary<ulong, int>();
    }

     
    public void SetPlayerPoints(int updatedAmount)
    {
        SetPlayerPointsServerRpc(updatedAmount);
    }

    public void ShowFinalScore()
    {
        ShowFinalScoreClientRpc();

    }

    [ClientRpc]
    private void ShowFinalScoreClientRpc()
    {
        FinalScorePanel.SetActive(true);
        //FinalScoreBoardText.text = ScoreBoardUIText.text;
        ScoreBoardPanel.SetActive(false);
        //TODO create a function that orders the highest score to the lowest
    }

    [ServerRpc(RequireOwnership =false)]
    private void SetPlayerPointsServerRpc(int updatedAmount, ServerRpcParams serverRpcParams = default)
    {
        Debug.Log("SetPlayerPointsServerRPC");
        UpdateScoreBoardUIClientRpc(updatedAmount, serverRpcParams.Receive.SenderClientId);
    }

    [ClientRpc]
    private void UpdateScoreBoardUIClientRpc(int updatedAmount, ulong senderClientId)
    {
        playerPointsDictionary[senderClientId] = updatedAmount;
        //string result= "";
        foreach (KeyValuePair<ulong, int> entry in playerPointsDictionary)
        {
            //result += $"Player: {entry.Key}, Points: {entry.Value}\n";

            //check if UI for this player's score is active. Activate it if its not
            if (ScoreBoardPanel.transform.GetChild((int)entry.Key).gameObject.activeInHierarchy == false)
            {
                ScoreBoardPanel.transform.GetChild((int)entry.Key).gameObject.SetActive(true);

                //add the PlayerScoreUI script to the array
                playerScoreUIs[(int)entry.Key] = ScoreBoardPanel.transform.GetChild((int)entry.Key).GetComponent<PlayerScoreUI>();

                //update the player name (using number for now)
                playerScoreUIs[(int)entry.Key].UpdateName("Player " + (int)entry.Key);
            }

            playerScoreUIs[(int)entry.Key].UpdateScore(entry.Value);




        }

        //ScoreBoardUIText.text = result;


    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
