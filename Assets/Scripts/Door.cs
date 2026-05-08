using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    public void TryOpen()
    {
        if (GameManager.instance.CanOpenDoor())
        {
            Debug.Log("Door opened!");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Need more correct buttons!");
        }
    }
}