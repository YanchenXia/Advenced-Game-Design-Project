using UnityEngine;

public class FinalDoorTrigger : MonoBehaviour
{
    public FinalDoor door;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.TryOpen();
        }
    }
}