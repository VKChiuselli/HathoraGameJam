using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerInventory : NetworkBehaviour
{

    GameObject itemHolding;
    private KeyCode keyToPress = KeyCode.E;
    bool isHoldingItem;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (!isHoldingItem)
        {
            if (Input.GetKeyDown(keyToPress))
            {

            }
        }
    }

    public void AddItem(GameObject itemPassed)
    {
        isHoldingItem = true;
        //TODO show item
        PickUpItem item = itemPassed.GetComponent<PickUpItem>();
        //active attack action

        //Destroy item
        DestroyThisObjectServerRpc(item.NetworkObject);
    }

    [ServerRpc(RequireOwnership = false)]
    private void DestroyThisObjectServerRpc(NetworkObjectReference itemToDestroy)
    {

        itemToDestroy.TryGet(out NetworkObject item);

        if (item == null)
        {
            return;
        }

        PickUpItem pickUpItem = item.GetComponent<PickUpItem>();

        pickUpItem.DestroySelf();

    }
}
