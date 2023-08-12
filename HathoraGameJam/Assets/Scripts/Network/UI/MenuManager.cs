using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using UnityEngine.Networking;
using Unity.Netcode;

public class MenuManager : NetworkBehaviour
{
    [SerializeField] List<GameObject> characterList;
    [SerializeField] GameObject startWorkButton;
    [SerializeField] GameObject signatureText;
    [SerializeField] GameObject firstPanel;
    [SerializeField] GameObject secondPanel;
    [SerializeField] GameObject CreateRoomButton;
    [SerializeField] GameObject LobbyRoom;
    [SerializeField] GameObject WhereSpawnLobbyRoom;


    public string namePlayer;
    public GameObject characterSelected;


    private void Start()
    {

        startWorkButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            StartWork();
        });
        CreateRoomButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            CreateLobbyRoom(namePlayer);
            //        StartCoroutine( CreateRoom());
        });

    }

    public void StartWork()
    {
        namePlayer = signatureText.GetComponent<TextMeshProUGUI>().text;

        foreach (GameObject character in characterList)
        {
            if (character.activeSelf)
            {
                characterSelected = character;
            }
        }

        if (characterSelected == null)
        {
            Debug.LogError("No character selected!");
        }

        namePlayer = signatureText.GetComponent<TextMeshProUGUI>().text;

        if (namePlayer == "")
        {
            namePlayer = "LazyWorker";
        }
        secondPanel.SetActive(true);
        firstPanel.SetActive(false);


    }

    string url = "";

    IEnumerator CreateRoom()
    {
        CreateLobbyRoomServerRpc(namePlayer);
 //       CreateLobbyRoom(namePlayer);
        RoomConfig config = new RoomConfig();
        config.visibility = "public";
        config.region = "Sao_Paulo";

        string jsonConfig = JsonUtility.ToJson(config);
        var bytes = Encoding.UTF8.GetBytes(jsonConfig);

        using (var request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "INSERt TOKEN here");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {

                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }

        }


    }

    [ServerRpc(RequireOwnership =false)]
    private void CreateLobbyRoomServerRpc(string namePlayer)
    {
        CreateLobbyRoomClientRpc(namePlayer);
    }

    [ClientRpc]
    private void CreateLobbyRoomClientRpc(string namePlayer)
    {
        CreateLobbyRoom(namePlayer);
    }

    private void CreateLobbyRoom(string namePlayer)
    {
        Instantiate(LobbyRoom, WhereSpawnLobbyRoom.transform);
        LobbyRoom.GetComponent<LobbyRoom>().SetNameAndSize(namePlayer);
    }
}
