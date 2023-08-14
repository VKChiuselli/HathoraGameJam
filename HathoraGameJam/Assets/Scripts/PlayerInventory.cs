using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerInventory : NetworkBehaviour
{

  public string itemName;
  public bool isHoldingItem;

    void Start()
    {
        isHoldingItem = false;
        itemName = "";
    }

    void Update()
    {

 
    }

    public void AddItem(string itemPassed)
    {
        isHoldingItem = true;
        itemName = itemPassed;
    }

    public void RemoveItem(string itemPassed)
    {
        isHoldingItem = false;
        if (itemPassed == itemName)
        {
            itemName = "";
        }
        else
        {
            Debug.LogError("trying to remove an object that is not holded");
        }
    }


}
