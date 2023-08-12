using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Hathora.Cloud.Sdk.Model;

public class LobbyRoom : MonoBehaviour
{
    public string namePlayerRoom;
    public int howManyPlayers;
    public string idRoom;
    [SerializeField] TextMeshProUGUI nameRoom;
    [SerializeField] TextMeshProUGUI sizeRoom;
    Lobby lobby;

    public void SetNameAndSize(string player, int playerSize = 0)
    {
        namePlayerRoom = player + "'Office";
        nameRoom.text = namePlayerRoom;

        howManyPlayers = 1 + playerSize;
        sizeRoom.text = howManyPlayers + "/6";
    }

    public void SetLobby(Lobby lobbyFromUpdater)
    {
        lobby = lobbyFromUpdater;
        idRoom = lobby.RoomId;
    }


    // Update is called once per frame
    void Update()
    {
        if (lobby != null)
        {
            //update current room/lobby every 5 seconds
        }
    }
}
