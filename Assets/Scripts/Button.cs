using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public MovingWall wall;
    public bool isCorrectButton = false;

    private Renderer rend;
    private bool pressed = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !pressed)
        {
            pressed = true;

            if (isCorrectButton)
            {
                Debug.Log("Correct button!");

                rend.material.color = Color.green;

                GameManager.instance.AddCorrectButton();

                if (wall != null)
                    wall.StopWall();
            }
            else
            {
                Debug.Log("Wrong button!");

                rend.material.color = Color.red;
            }
        }
    }
}