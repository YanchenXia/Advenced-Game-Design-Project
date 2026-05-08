using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using TMPro;

public class PauseMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pauseMenuUI;
    public GameObject settingsUI;

    [Header("Settings Connections")]
    public CustomPlayerController playerController;
    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityText;
    public PlayerRespawn playerRespawn;

    [Header("Keybind Button Texts")]
    public TextMeshProUGUI gravityBtnText;
    public TextMeshProUGUI freezeBtnText;
    public TextMeshProUGUI interactBtnText;
    public TextMeshProUGUI crouchBtnText;

    private bool GameIsPaused = false;

    void Start()
    {
        if (sensitivitySlider != null && playerController != null)
        {
            sensitivitySlider.value = playerController.mouseSensitivity;
            //set initial text
            if (sensitivityText != null) sensitivityText.text = sensitivitySlider.value.ToString("F1");
            
            sensitivitySlider.onValueChanged.AddListener(UpdateSensitivity);
        }

        //set initial keybind text
        if (playerController != null)
        {
            if (gravityBtnText != null) gravityBtnText.text = playerController.gravityKey.ToString();
            if (freezeBtnText != null) freezeBtnText.text = playerController.freezeKey.ToString();
            if (interactBtnText != null) interactBtnText.text = playerController.interactKey.ToString();
            if (crouchBtnText != null) crouchBtnText.text = playerController.crouchKey.ToString();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        settingsUI.SetActive(false);
        Time.timeScale = 1f; 
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; 
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetBlocksAndRespawn()
    {
        ResetableBlock[] allBlocks = FindObjectsOfType<ResetableBlock>();
        foreach (ResetableBlock block in allBlocks) block.ResetToStart();

        BlockButton[] allButtons = FindObjectsOfType<BlockButton>();
        foreach (BlockButton button in allButtons) button.ForceReset(); 

        if (playerRespawn != null) playerRespawn.DieAndRespawn();
        Resume();
    }

    public void OpenSettings()
    {
        pauseMenuUI.SetActive(false);
        settingsUI.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void UpdateSensitivity(float newSensitivity)
    {
        if (playerController != null)
        {
            playerController.mouseSensitivity = newSensitivity;
            if (sensitivityText != null) sensitivityText.text = newSensitivity.ToString("F1");
        }
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); 
    }

    //keybind system
    private bool isWaitingForKey = false;
    private string actionToRebind = "";
    private TextMeshProUGUI buttonTextToChange;

    public void RebindGravity() { StartRebind("Gravity", gravityBtnText); }
    public void RebindFreeze() { StartRebind("Freeze", freezeBtnText); }
    public void RebindInteract() { StartRebind("Interact", interactBtnText); }
    public void RebindCrouch() { StartRebind("Crouch", crouchBtnText); }

    private void StartRebind(string actionName, TextMeshProUGUI buttonText)
    {
        isWaitingForKey = true;
        actionToRebind = actionName;
        buttonTextToChange = buttonText;
        if (buttonTextToChange != null) buttonTextToChange.text = "Press any key...";
    }

    void OnGUI()
    {
        if (isWaitingForKey)
        {
            Event e = Event.current;
            if (e.isKey && e.type == EventType.KeyDown)
            {
                if (actionToRebind == "Gravity") playerController.gravityKey = e.keyCode;
                else if (actionToRebind == "Freeze") playerController.freezeKey = e.keyCode;
                else if (actionToRebind == "Interact") playerController.interactKey = e.keyCode;
                else if (actionToRebind == "Crouch") playerController.crouchKey = e.keyCode;

                if (buttonTextToChange != null) buttonTextToChange.text = e.keyCode.ToString();
                isWaitingForKey = false; 
            }
        }
    }
}