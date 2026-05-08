using UnityEngine;
using UnityEngine.SceneManagement; // Required to load levels

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject levelSelectPanel;

    void Start()
    {
        //cursor enabled
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //time normal
        Time.timeScale = 1f;

        //reset UI
        ShowMainMenu();
    }

    public void ShowLevelSelect()
    {
        mainMenuPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
    }

    public void ShowMainMenu()
    {
        levelSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    //load levels (type level names in quotes:)
    public void LoadTutorial()
    {
        SceneManager.LoadScene("PuzzleGame"); 
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("PuzzleGame-level1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("PuzzleGame-XY");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("PuzzleGame-AidanLevelIndoors");
    }


    public void QuitGame()
    {
        Debug.Log("Game is quitting!"); 
        Application.Quit();
    }
}