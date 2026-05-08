using UnityEngine;

public class StartTimerOnExit : MonoBehaviour
{
    public Timer timerScript;

    void OnTriggerExit(Collider other)
    {
        //check if player leaves
        if (other.CompareTag("Player"))
        {
            timerScript.StartTimer(); //start timer
            gameObject.SetActive(false); //disable trigger
        }
    }
}