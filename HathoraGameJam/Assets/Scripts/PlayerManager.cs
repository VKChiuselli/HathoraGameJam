using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
 

    public void HandlePowerUp(string itemName)
    {

        switch (itemName)
        {
         
            case "SpeedBoost":
                Speed();
                break;
            case "Invisibilty":
                Speed();
                break;
            case "CoffeeBreak":
                Speed();
                break;
            case "PhoneDistraction":
                Speed();
                break;
            case "SmokeBomb":
                Speed();
                break;
            case "LunchboxBuff":
                Speed();
                break;

        }

      

      void Speed()
    {
            Debug.Log("Implement buff speed");
    }
}
}
