using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class GainPointsPressingSeveralTimes : NetworkBehaviour, IHasProgress
{
    private bool qKeyHeld = false;
    private float holdStartTime;
    private float holdDurationRequired = 1.0f; // 3 seconds
    ScoreboardManager scoreBoard;
    public int howMuchPointGiveThisObject;
    NetworkVariable<bool> isExhausted = new NetworkVariable<bool>();

    public Material enabledItemMaterial;
    public Material disableItemMaterial;


    private float timer = 0f;
    private float interval = 10f; // 10 seconds interval



    public float resetTime = 5f;
    private bool oneTime;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;


    public override void OnNetworkSpawn()
    {
        isExhausted.OnValueChanged += isExhausted_OnValueChanged; 
    }

    private void isExhausted_OnValueChanged(bool previousValue, bool newValue)
    {
        Debug.Log("isExhausted_OnValueChanged changed! several time");
        //if (newValue)
        //{
        //    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        //    {
        //        progressNormalized = 1f
        //    });
        //}
    }

    private void Start()
    {
        isExhausted.Value = false;
        if (howMuchPointGiveThisObject == 0)
        {
            howMuchPointGiveThisObject = 20;
        }
        scoreBoard = GameObject.Find("ScoreBoardManagerCanvas").GetComponent<ScoreboardManager>();
    }


    public int keyPressCount = 0;
    public int targetKeyPresses = 5; // Number of times 'R' key needs to be pressed
    public KeyCode keyToPress = KeyCode.R; // Key you want to track



    private void OnTriggerStay(Collider other)
    {

        if (!oneTime)
        {
            if (other.tag == "Player")
            {
                if (!isExhausted.Value)
                {
                    if (Input.GetKeyDown(keyToPress))
                    {
                        keyPressCount++;

                        if (keyPressCount >= targetKeyPresses)
                        {
                            oneTime = true;
                            IncreasePlayerPoints(other);
                            keyPressCount = 0; // Reset the count after firing the function
                        }
                    }
                }
            }
        }


    }



    public void IncreasePlayerPoints(Collider other)
    {
        SetObjectStateServerRpc(true);
        Debug.Log("R pressed 5 times AND POINTS ARE GAINED!");

        other.gameObject.GetComponent<PlayerGainPoints>().GainPoints(howMuchPointGiveThisObject);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetObjectStateServerRpc(bool setVariable)
    {
        isExhausted.Value = setVariable;
        SetObjectStateClientRpc(setVariable);
    }

    [ClientRpc]
    private void SetObjectStateClientRpc(bool setVariable)
    {
        if (setVariable)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = disableItemMaterial;
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().material = enabledItemMaterial;
        }
    }
}
