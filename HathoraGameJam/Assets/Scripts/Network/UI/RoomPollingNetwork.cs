using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;

public class RoomPollingNetwork : NetworkBehaviour
{
    // Start is called before the first frame update

    [SerializeField] TextMeshProUGUI OfficeNameText;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetOfficeName(string officeName)
    {
        OfficeNameText.text = officeName;
    }

}
