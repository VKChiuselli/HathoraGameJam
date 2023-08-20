using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreboardManager : NetworkBehaviour
{


    private Dictionary<ulong, int> playerPointsDictionary;
    // [SerializeField] TextMeshProUGUI ScoreBoardUIText;
    [SerializeField] TextMeshProUGUI FinalScoreBoardText;
    [SerializeField] GameObject FinalScorePanel;
    [SerializeField] GameObject ScoreBoardPanel;
    [SerializeField] Button PlayAgainButton;

    private PlayerScoreUI[] playerScoreUIs = new PlayerScoreUI[8];


    public static ScoreboardManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        PlayAgainButton.onClick.AddListener(PlayAgain);
        playerPointsDictionary = new Dictionary<ulong, int>();
    }

    void PlayAgain()
    {
        System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe")); //new program

        Application.Quit(); //kill current process
    }

    public void SetPlayerPoints(int updatedAmount)
    {
        string playerNameFiltered = PlayerPrefs.GetString("Name").Replace(" is Ready", "");
        SetPlayerPointsServerRpc(updatedAmount, playerNameFiltered);
    }

    public void ShowFinalScore()
    {
        Time.timeScale = 0;
        ShowFinalScoreClientRpc();

    }

    [ClientRpc]
    private void ShowFinalScoreClientRpc()
    {
        FinalScorePanel.SetActive(true);
        //FinalScoreBoardText.text = ScoreBoardUIText.text;
        ScoreBoardPanel.SetActive(false);
        Time.timeScale = 0;
        //TODO create a function that orders the highest score to the lowest
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetPlayerPointsServerRpc(int updatedAmount, string playerName, ServerRpcParams serverRpcParams = default)
    {
        Debug.Log("SetPlayerPointsServerRPC");
        UpdateScoreBoardUIClientRpc(updatedAmount, playerName, serverRpcParams.Receive.SenderClientId);
    }

    [ClientRpc]
    private void UpdateScoreBoardUIClientRpc(int updatedAmount, string playerName, ulong senderClientId)
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
                UpdateNameBoard(entry, playerName);
            }

            playerScoreUIs[(int)entry.Key].UpdateScore(entry.Value);




        }

        //ScoreBoardUIText.text = result;


    }

    private void UpdateNameBoard(KeyValuePair<ulong, int> entry, string playerName)
    {
        playerScoreUIs[(int)entry.Key].UpdateName(playerName + (int)entry.Key);
    }

}