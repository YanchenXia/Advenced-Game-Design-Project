using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float startTime = 60f; // time in seconds
    private float currentTime;

    public TextMeshProUGUI timerText;
    public GameObject gameOverPanel;

    private bool isGameOver = false;

    void Start()
    {
        currentTime = startTime;
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (isGameOver)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            GameOver();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (currentTime <= 10f)
            timerText.color = Color.red; // warning color
    }

    void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // freeze game
    }
}