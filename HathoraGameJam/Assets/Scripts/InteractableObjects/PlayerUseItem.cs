using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class PlayerUseItem : MonoBehaviour
{

    PlayerInventory playerInventory;
    bool canAttack;
    private KeyCode keyToPress = KeyCode.Space;

    void Start()
    {
        canAttack = false;
        playerInventory = GetComponent<PlayerInventory>();
    }
     
    void Update()
    {
        if (playerInventory.isHoldingItem)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

        if (canAttack)
        {
            if (Input.GetKeyDown(keyToPress))
            {

                Debug.Log("Perform Attack");

                PerformAttack(playerInventory.itemName);

            }
        }


    }

    private void PerformAttack(string itemName)
    {
        switch (itemName)
        {

            case "RubberBand":
                Attack();
                break;
            case "PaperAirplane":
                Attack();
                break;
            case "StickyNote":
                Attack();
                break;
            case "DeskDrawerBarricade":
                Attack();
                break;
            case "ConfettiCannon":
                Attack();
                break;
            case "OfficeSupplyFrenzy":
                Attack();
                break;

        }
    }

    private void Attack()
    {
        Debug.Log("ImplementAttack");
    }
}
