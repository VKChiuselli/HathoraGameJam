using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
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
}
