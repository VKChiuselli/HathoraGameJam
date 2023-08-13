using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class GainPointsKeepPressed : NetworkBehaviour
{
    private bool qKeyHeld = false;
    private float holdStartTime;
    private float holdDurationRequired = 1.0f; // 3 seconds
    ScoreboardManager scoreBoard;
    public int howMuchPointGiveThisObject;
    NetworkVariable< bool> isExhausted = new NetworkVariable<bool>();

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

        if(other.tag=="Player")
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

                        StartCoroutine(IncreasePlayerPoints(other));
                    }
                }
                else
                {
                    qKeyHeld = false;
                }
            }
        }
  
    }

    

    public float resetTime = 5f;

    IEnumerator IncreasePlayerPoints(Collider other)
    {
        SetObjectStateServerRpc(true);
        Debug.Log("Q key held for 1 second AND POINTS ARE GAINED!");

        other.gameObject.GetComponent<PlayerGainPoints>().GainPoints(howMuchPointGiveThisObject);

        yield return new WaitForSeconds(resetTime);


        SetObjectStateServerRpc(false); 

    }

    [ServerRpc(RequireOwnership =false)]
    private void SetObjectStateServerRpc(bool setVariable)
    {
        SetObjectStateClientRpc(setVariable);
    }

    [ClientRpc]
    private void SetObjectStateClientRpc(bool setVariable)
    {
        isExhausted.Value = setVariable;
    }
}
