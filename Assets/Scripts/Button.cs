using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public Door door;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("button pressed");

            if (door != null)
            {
                door.OpenDoor();
            }
            else
            {
                Debug.Log("door not assigned");
            }
        }
    }
}