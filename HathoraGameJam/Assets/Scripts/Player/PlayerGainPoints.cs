using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class PlayerGainPoints : MonoBehaviour
{
    private bool qKeyHeld = false;
    private float holdStartTime;
    private float holdDurationRequired = 1.0f; // 3 seconds
    ScoreboardManager scoreBoard;
    public int currentPoints;
    private bool isExhausted;

    private void Start()
    {
        isExhausted = false;
        currentPoints = 0;
      //  scoreBoard = GameObject.Find("ScoreBoardManagerCanvas").GetComponent<ScoreboardManager>();
    }

    private void Update()
    {
        if (scoreBoard == null)
        {
            GameObject scoreBoardManagerCanvas = GameObject.Find("ScoreBoardManagerCanvas") ;
            if (scoreBoardManagerCanvas != null)
            {
                if (scoreBoardManagerCanvas.GetComponent<ScoreboardManager>() != null)
                {
                    scoreBoard = scoreBoardManagerCanvas.GetComponent<ScoreboardManager>();
                }
            
             //   scoreBoard = ;

            }
        }

    }


    public void GainPoints(int amount)
    {
        currentPoints = currentPoints + amount;
        scoreBoard.SetPlayerPoints(currentPoints);
    }

 
}
