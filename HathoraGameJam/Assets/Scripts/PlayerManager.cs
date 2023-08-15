using HathoraGameJam.CubicleEscape;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI BonusActiveText;
   // [SerializeField] GameObject PlayerCanvasHUD;
    [SerializeField] GameObject ConfettiBlackScreen;

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
    public bool blackScreen;

   

    float blackScreenResetTimer=3f;

    public void SetBlackScreen()
    {
        StartCoroutine(ResetAfterTime());
    }

   IEnumerator ResetAfterTime()
    {
        ConfettiBlackScreen.SetActive(true);
        yield return new WaitForSeconds(blackScreenResetTimer);
        ConfettiBlackScreen.SetActive(false);
    }

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
