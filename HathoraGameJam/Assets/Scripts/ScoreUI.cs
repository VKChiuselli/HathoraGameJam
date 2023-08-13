using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    public static ScoreUI Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
}
