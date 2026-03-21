using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public Door door;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Button pressed!");

            if (door != null)
            {
                door.OpenDoor();
            }
            else
            {
                Debug.Log("Door not assigned!");
            }
        }
    }
}