using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class GainPointsKeepPressed : NetworkBehaviour
{
    private bool qKeyHeld = false;
    private float holdStartTime;
    private float holdDurationRequired = 1.0f; // 3 seconds
    ScoreboardManager scoreBoard;
    public int howMuchPointGiveThisObject;
    NetworkVariable< bool> isExhausted = new NetworkVariable<bool>();

  public  Material enabledItemMaterial;
  public  Material disableItemMaterial;

    private void Start()
    {
        isExhausted.Value = false;
        if (howMuchPointGiveThisObject == 0)
        {
        howMuchPointGiveThisObject = 20;
        }
        scoreBoard = GameObject.Find("ScoreBoardManagerCanvas").GetComponent<ScoreboardManager>();
    }


    private void OnTriggerStay(Collider other)
    {

        if (!oneTime)
        {
            if (other.tag == "Player")
            {
                if (!isExhausted.Value)
                {
                    if (Input.GetKey(KeyCode.Q))
                    {
                        if (!qKeyHeld)
                        {
                            qKeyHeld = true;
                            holdStartTime = Time.time;
                        }

                        if (qKeyHeld && Time.time - holdStartTime >= holdDurationRequired)
                        {
                          oneTime = true ;
                            IncreasePlayerPoints(other);
                            //  StartCoroutine(IncreasePlayerPoints(other));
                        }
                    }
                    else
                    {
                        qKeyHeld = false;
                    }
                }
            }
        }
    
  
    }


    private float timer = 0f;
    private float interval = 10f; // 10 seconds interval

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            // Call your function here
            ResetObject();

            // Reset the timer
            timer = 0f;
        }
    }

    private void ResetObject()
    {
        if (oneTime)
        {
            oneTime = false;
       //     SetObjectStateServerRpc(oneTime);
        }
    }

    public float resetTime = 5f;
    private bool oneTime;

    //IEnumerator IncreasePlayerPoints(Collider other)
    //{
    //    SetObjectStateServerRpc(true);
    //    Debug.Log("Q key held for 1 second AND POINTS ARE GAINED!");

    //    other.gameObject.GetComponent<PlayerGainPoints>().GainPoints(howMuchPointGiveThisObject);

    //    yield return new WaitForSeconds(resetTime);


    //    SetObjectStateServerRpc(false); 

    //} 

    public void IncreasePlayerPoints(Collider other)
    {
        SetObjectStateServerRpc(true);
        Debug.Log("Q key held for 1 second AND POINTS ARE GAINED!");

        other.gameObject.GetComponent<PlayerGainPoints>().GainPoints(howMuchPointGiveThisObject);
 

    }

    [ServerRpc(RequireOwnership =false)]
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
