using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using UnityEngine.Networking;

public class MenuManager : MonoBehaviour
{
    [SerializeField] List<GameObject> characterList;
    [SerializeField] GameObject startWorkButton;
    [SerializeField] GameObject signatureText;
    [SerializeField] GameObject firstPanel;
    [SerializeField] GameObject secondPanel;
    [SerializeField] GameObject CreateRoomButton;


    public string namePlayer;
    public GameObject characterSelected;


    private void Start()
    {

        startWorkButton.GetComponent<Button>().onClick.AddListener(() => {
            StartWork();
        });
        CreateRoomButton.GetComponent<Button>().onClick.AddListener(() => {
            CreateRoom();
        });
       
    }

    public void StartWork()
    {
        namePlayer = signatureText.GetComponent<TextMeshProUGUI>().text;

        foreach(GameObject character in characterList)
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
        firstPanel.SetActive(false);


    }

    string url = "";

    public void CreateRoom()
    {
        RoomConfig config = new RoomConfig();
        config.visibility = "public";
        config.region = "Sao_Paulo";

        string jsonConfig = JsonUtility.ToJson(config);
        var bytes = Encoding.UTF8.GetBytes(jsonConfig);

        using (var request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler =  (UploadHandler)new UploadHandlerRaw(bytes);
            request.downloadHandler =  new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "INSERt TOKEN here");

        }


    }


}
