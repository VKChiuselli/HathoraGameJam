using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
        isHoldingItem = true;
        itemName = itemPassed;
        Sprite loadedSprite =   Resources.Load<Sprite>($"Illustration/Weapons/{itemPassed}");

        if (loadedSprite != null)
        {
        

            if (WeaponHoldedImage.GetComponent<Image>() != null)
            {
                // Assign the loaded sprite to the Image component
                WeaponHoldedImage.GetComponent<Image>().sprite = loadedSprite;
            }
            else
            {
                Debug.LogError("Image component not found on the GameObject.");
            }
        }
        else
        {
            Debug.LogError("Image not found in Resources folder.");
        }

        WeaponNameText.GetComponent<TextMeshProUGUI>().text = itemName;
        WeaponNameText.GetComponent<TextMeshProUGUI>().text = itemName;
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
