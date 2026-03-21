using UnityEngine;

public class Door : MonoBehaviour
{
    public void OpenDoor()
    {
        Debug.Log("Door opened!");
        gameObject.SetActive(false);
    }
}