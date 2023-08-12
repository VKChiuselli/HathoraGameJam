
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class TimerUI : NetworkBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;

    private NetworkVariable<float> timeRemaining = new NetworkVariable<float>(3 * 60);
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            timeRemaining.Value -= Time.deltaTime;
        }
        System.TimeSpan time = System.TimeSpan.FromSeconds(timeRemaining.Value);
        timerText.text = time.ToString(@"mm\:ss");
    }
}
