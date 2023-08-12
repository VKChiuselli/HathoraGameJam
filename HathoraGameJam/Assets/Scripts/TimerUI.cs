using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;

    private float timeRemaining = 3*60;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(timeRemaining);
        timerText.text = time .ToString(@"mm\:ss");
    }
}
