using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using System;

public class PickUpItem : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI tooltipMessage;
    [SerializeField] GameObject pickupbar;
    GameObject currentPlayer;
    void Start()
    {
        tooltipMessage.text = "Press E";
    }

    bool canPickup;
    public KeyCode keyToPress = KeyCode.E; // Key you want to track

    private void Update()
    {
        if (canPickup)
        {
            if (Input.GetKeyDown(keyToPress))
            {

                DestroyThisObjectServerRpc();
             
            }
        }
    }


    [ServerRpc(RequireOwnership =false)]
    private void DestroyThisObjectServerRpc()
    {
        DestroyThisObjectClientRpc();
    }



    [ClientRpc]
    private void DestroyThisObjectClientRpc()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {


        if (other.tag == "Player" && other.gameObject.GetComponent<NetworkObject>().IsLocalPlayer)
        {
            currentPlayer = other.gameObject;
            canPickup = true;
            pickupbar.SetActive(true);
        } 
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.gameObject.GetComponent<NetworkObject>().IsLocalPlayer)
        {
            currentPlayer = other.gameObject;
            canPickup = true;
            pickupbar.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && other.gameObject.GetComponent<NetworkObject>().IsLocalPlayer)
        {
            currentPlayer = null;
            canPickup = false;
            pickupbar.SetActive(false);
        }
    }

}
