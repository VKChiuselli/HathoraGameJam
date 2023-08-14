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
    [SerializeField] string itemName;
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
            if (currentPlayer != null)
            {
                if (Input.GetKeyDown(keyToPress))
                {
                    if (!currentPlayer.GetComponent<PlayerInventory>().isHoldingItem)
                    {
                        currentPlayer.GetComponent<PlayerInventory>().AddItem(itemName);
                        DestroyThisObjectServerRpc();
                    }
                 
                    //currentPlayer.GetComponent<PlayerInventory>().AddItem( gameObject );
           

                }
            }
          
        }
    }


    public static void DestroyItem()
    {

    }

    [ServerRpc(RequireOwnership =false)]
    private void DestroyThisObjectServerRpc()
    {
        gameObject.GetComponent<NetworkObject>().Despawn(true);
        Destroy(gameObject);
       
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
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
