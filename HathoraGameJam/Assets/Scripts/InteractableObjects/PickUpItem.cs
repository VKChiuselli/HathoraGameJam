using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using System;

public class PickUpItem : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI tooltipMessage;
    [SerializeField] GameObject pickupui;
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
                    if (this.gameObject.tag == "Weapon")
                    {
                        if (!currentPlayer.GetComponent<PlayerInventory>().isHoldingItem)
                        {
                            currentPlayer.GetComponent<PlayerInventory>().AddItem(itemName);
                            DestroyThisObjectServerRpc();
                        }
                    }
                    else if (this.gameObject.tag == "Powerup")
                    {
                        currentPlayer.GetComponent<PlayerManager>().HandlePowerUp(itemName);
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
            pickupui.SetActive(true);
        } 
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.gameObject.GetComponent<NetworkObject>().IsLocalPlayer)
        {
            currentPlayer = other.gameObject;
            canPickup = true;
            pickupui.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && other.gameObject.GetComponent<NetworkObject>().IsLocalPlayer)
        {
            currentPlayer = null;
            canPickup = false;
            pickupui.SetActive(false);
        }
    }

}
