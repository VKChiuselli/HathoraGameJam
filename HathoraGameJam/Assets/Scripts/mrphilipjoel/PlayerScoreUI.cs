using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScoreUI : MonoBehaviour
{
    public TextMeshProUGUI playerName, score;

    public void UpdateName(string pName)
    {
        playerName.text = pName;
    }

    public void UpdateScore(int s)
    {
        score.text = s.ToString();
    }

}
