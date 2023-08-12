using Hathora.Cloud.Sdk.Model;
using Hathora.Demos._1_FishNetDemo.HathoraScripts.Client.ClientMgr;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRoomsAvaiableNetwork : MonoBehaviour
{
    GameObject MainMenuPageCanvas;
   [SerializeField] GameObject LobbyRoom;
   [SerializeField] GameObject WhereSpawnLobbyRoom ;
   public List<Lobby> listOflobbies;
    void Start()
    {
        MainMenuPageCanvas = GameObject.Find("MainMenuPageCanvas");
    }

    public void SetLobbies(List<Lobby> lobbies)
    {
        listOflobbies = lobbies;
      foreach(Lobby lobby in listOflobbies)
        {
            if ( RoomIdAlreadyExist(lobby.RoomId))
            {
                GameObject lobbyRoom = Instantiate(LobbyRoom, WhereSpawnLobbyRoom.transform);
                lobbyRoom.GetComponent<LobbyRoom>().SetLobby(lobby);
            }
       
        }
    }

    private bool RoomIdAlreadyExist(string roomId)
    {
        foreach (Lobby lobby in listOflobbies)
        {

            foreach (Transform room in WhereSpawnLobbyRoom.transform)
            {
                if(room.gameObject.GetComponent<LobbyRoom>().idRoom == roomId)
                {
                    return false;
                }
            }

        }

        return true;
    }

    private void DestroyRooms()
    {
       foreach(GameObject room in WhereSpawnLobbyRoom.transform)
        {
            DestroyImmediate(room);
        }
    }

    public void RefreshLobbyList()
    {
        MainMenuPageCanvas.GetComponent<HathoraFishnetClientMgrDemoUi>().OnViewLobbiesBtnClick();
    }

    // Update is called once per frame
    void Update()
    {
   // every 5  seconds call this function     OnViewLobbiesBtnClick()
    }
}
