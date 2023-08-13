using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;
public class ScoreboardManager : NetworkBehaviour
{


    private Dictionary<ulong, int> playerPointsDictionary;
    [SerializeField] TextMeshProUGUI ScoreBoardUIText;
    
    public static ScoreboardManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        playerPointsDictionary = new Dictionary<ulong, int>();
    }


    //player call this function when score points with some action
    public void SetPlayerPoints(int updatedAmount)
    {
        SetPlayerPointsServerRpc(updatedAmount);
    }


    [ServerRpc(RequireOwnership =false)]
    private void SetPlayerPointsServerRpc(int updatedAmount, ServerRpcParams serverRpcParams = default)
    {

        playerPointsDictionary[serverRpcParams.Receive.SenderClientId] = updatedAmount;


        UpdateScoreBoardUIClientRpc( );
    }

    [ClientRpc]
    private void UpdateScoreBoardUIClientRpc( )
    {

        string result="";
        foreach (KeyValuePair<ulong, int> entry in playerPointsDictionary)
        {
            result += $"Player: {entry.Key}, Points: {entry.Value}\n";
        }

        ScoreBoardUIText.text = result;
         

    }

  
    // Update is called once per frame
    void Update()
    {
        
    }
}
