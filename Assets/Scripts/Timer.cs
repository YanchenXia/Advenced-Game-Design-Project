using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float startTime = 60f; // time in seconds
    private float currentTime;

    [Header("UI References")]
    public TextMeshProUGUI timerText;   // assign your TimerText UI
    public GameObject gameOverPanel;    // assign your GameOverPanel UI

    private bool isGameOver = false;
    private bool timerStarted = false; // only start counting when true

    void Start()
    {
        currentTime = startTime;
        gameOverPanel.SetActive(false); // hide Game Over panel initially
    }

    void Update()
    {
        if (!timerStarted || isGameOver)
            return;

        // countdown
        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            GameOver();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // warning color
        if (currentTime <= 10f)
            timerText.color = Color.red;
        else
            timerText.color = Color.white;
    }

    void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // freeze game
    }

    // Call this function from the trigger when player leaves
    public void StartTimer()
    {
        timerStarted = true;
    }
}