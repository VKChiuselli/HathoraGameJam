using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyRoom : MonoBehaviour
{
    public string namePlayerRoom;
    public int howManyPlayers;

    [SerializeField] TextMeshProUGUI nameRoom;
    [SerializeField] TextMeshProUGUI sizeRoom;

   
    public void SetNameAndSize(string player, int playerSize = 0)
    {
        namePlayerRoom = player + "'Office";
        nameRoom.text = namePlayerRoom;

        howManyPlayers = 1 + playerSize;
        sizeRoom.text = howManyPlayers +  "/6";
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
