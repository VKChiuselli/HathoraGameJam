using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class PlayerGainPoints : MonoBehaviour
{
    private bool qKeyHeld = false;
    private float holdStartTime;
    private float holdDurationRequired = 3.0f; // 3 seconds
    ScoreboardManager scoreBoard;
    public int currentPoints;
    private bool isExhausted;

    private void Start()
    {
        isExhausted = false;
        currentPoints = 0;
        scoreBoard = GameObject.Find("ScoreBoardManagerCanvas").GetComponent<ScoreboardManager>();
    }

    private void Update()
    {
        if (!isExhausted)
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

                    MyMethodToCall();
                }
            }
            else
            {
                qKeyHeld = false;
            }
        }
      
    }

    private void MyMethodToCall()
    {
        isExhausted = true;
        Debug.Log("Q key held for 3 seconds!");
        currentPoints = currentPoints + 10;
        scoreBoard.SetPlayerPoints(currentPoints);

        //after 3 seconds put    isExhausted = false;
    }
}
