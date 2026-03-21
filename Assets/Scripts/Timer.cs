using UnityEngine;
using TMPro;


public class Timer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float startTime = 30f;
    private float currentTime;

    [Header("UI References")]
    public TextMeshProUGUI timerText;   
    public GameObject gameOverPanel;    

    public MovingWall wall;

    private bool isGameOver = false;
    private bool timerStarted = false;

    void Start()
    {
        currentTime = startTime;
        gameOverPanel.SetActive(false); 
    }

    void Update()
    {
        if (!timerStarted || isGameOver)
            return;

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

        if (currentTime <= 10f)
            timerText.color = Color.red;
        else
            timerText.color = Color.white;
    }

    void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void StartTimer()
    {
        timerStarted = true;

        if (wall != null)
        wall.StartWall();
    }

    public void TriggerGameOver()
    {
       GameOver();
    }
}