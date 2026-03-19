using UnityEngine;

public class EndZone : MonoBehaviour
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
        }
    }
}