using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public float speed = 2f;
    private bool isMoving = false;

    public Timer timer; 

    void Update()
    {
        if (!isMoving)
            return;

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void StartWall()
    {
        isMoving = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player caught by wall!");

            if (timer != null)
                timer.TriggerGameOver(); 
        }
    }
}