using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public Door door;
    public MovingWall wall;

    public bool isCorrectButton = false;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Button pressed!");

            if (isCorrectButton)
            {
                Debug.Log("Correct button!");

                if (rend != null)
                    rend.material.color = Color.green;

                if (door != null)
                    door.OpenDoor();

                if (wall != null)
                    wall.StopWall();
            }
            else
            {
                Debug.Log("Wrong button!");

                if (rend != null)
                    rend.material.color = Color.red;
            }
        }
    }
}