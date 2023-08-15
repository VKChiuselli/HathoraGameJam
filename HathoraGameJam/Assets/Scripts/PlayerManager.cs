using HathoraGameJam.CubicleEscape;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using static HathoraGameJam.CubicleEscape.RollingChairMovement;

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
                SpeedBonus();
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

    private void SpeedBonus()
    {
        StartCoroutine(Speed());
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
            GetComponent<RollingChairMovement>().moveState  = MoveState.DoubleSpeed;
            yield return new WaitForSeconds(speedDuration);
        GetComponent<RollingChairMovement>().moveState = MoveState.Normal;
        BonusActiveText.gameObject.SetActive(false);
    }
} 
