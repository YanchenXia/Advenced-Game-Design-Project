using UnityEngine;

public class StartZone : MonoBehaviour
{
    public Timer timer;
    private bool hasStarted = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasStarted)
        {
            hasStarted = true; 
            Debug.Log("Player left the room. timer started.");
            
            if (timer != null)
            {
                timer.StartTimer();
            }
        }
    }
}