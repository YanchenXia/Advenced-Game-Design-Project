using UnityEngine;

public class StartTimerOnExit : MonoBehaviour
{
    public Timer timerScript; // Drag your Timer GameObject here

    void OnTriggerExit(Collider other)
    {
        // Check if the player leaves
        if (other.CompareTag("Player"))
        {
            timerScript.StartTimer(); // start the timer
            gameObject.SetActive(false); // optional: disable trigger
        }
    }
}