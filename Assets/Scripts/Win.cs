using UnityEngine;
using UnityEngine.SceneManagement;
public class Win : MonoBehaviour
{
    public GameObject winPanel;
    public Timer timer;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached end!");

            // Show win screen
            winPanel.SetActive(true);

            // Stop the game
            Time.timeScale = 0f;

            //unlock cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}