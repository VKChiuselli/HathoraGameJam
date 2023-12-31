using TMPro;
using Unity.Netcode;
using UnityEngine;

public class TimerUI : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject ScoreBoardManagerCanvas;

    private float gameTime = 2 * 60;
    private NetworkVariable<int> timeRemaining = new NetworkVariable<int>(2 * 60);
    public bool isGameStarted;

    private void Start()
    {
        isGameStarted = false;
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            timeRemaining.OnValueChanged += (_, newValue) =>
            {
                ShowTimerOnText(newValue);
            };
        }

    }

    private void ShowTimerOnText(int newValue)
    {
        System.TimeSpan time = System.TimeSpan.FromSeconds(newValue);
        timerText.text = time.ToString(@"mm\:ss");
    }

    void Update()
    {
        if (!isGameStarted)
        {
            return;
        }
        if (IsServer)
        {

            if (timeRemaining.Value == 0)
            {
                ScoreBoardManagerCanvas.GetComponent<ScoreboardManager>().ShowFinalScore();
                return;
            }

            gameTime -= Time.deltaTime;
            var intTime = (int)gameTime;

            if (timeRemaining.Value != intTime)
            {
                timeRemaining.Value = intTime;
            }

        }

        if (IsClient)
        {
            ShowTimerOnText(timeRemaining.Value);

        }


    }


}
