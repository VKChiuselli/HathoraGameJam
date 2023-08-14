using HathoraGameJam.CubicleEscape;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI BonusActiveText;

    public void HandlePowerUp(string itemName)
    {

        switch (itemName)
        {

            case "SpeedBoost":
                StartCoroutine(Speed());
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
    }




        public float speedDuration=5f;

     IEnumerator Speed()
    {
        BonusActiveText.gameObject.SetActive(true);
        BonusActiveText.text = "Speed boost activated!!!";
            GetComponent<RollingChairMovement>().hasBonusSpeed=true;
            yield return new WaitForSeconds(speedDuration);
        GetComponent<RollingChairMovement>().hasBonusSpeed = false;
        BonusActiveText.gameObject.SetActive(false);
    }
} 
