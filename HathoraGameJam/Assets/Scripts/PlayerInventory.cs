using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;

public class PlayerInventory : NetworkBehaviour
{

  public string itemName;
  public bool isHoldingItem;
    [SerializeField] GameObject WeaponHoldedImage;
    [SerializeField] GameObject WeaponNameText;

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
        WeaponHoldedImage.SetActive(true);
        WeaponNameText.GetComponent<TextMeshProUGUI>().text = itemName;
        isHoldingItem = true;
        itemName = itemPassed;
    }

    public void RemoveItem(string itemPassed)
    {
        WeaponHoldedImage.SetActive(false);
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
