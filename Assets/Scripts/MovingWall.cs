using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public float speed = 1f;
    private bool isMoving = false;

    public Timer timer;

    // 👇 NEW: set this in Inspector
    public float bottomLimit = 0f;

    void Update()
    {
        if (!isMoving) return;

        transform.Translate(Vector3.back * speed * Time.deltaTime);

        // 👇 CHECK POSITION INSTEAD OF COLLISION
        if (transform.position.y <= bottomLimit)
        {
            Debug.Log("Wall reached limit!");

            if (timer != null)
                timer.TriggerGameOver();
        }
    }

    public void StartWall()
    {
        isMoving = true;
    }

    public void StopWall()
    {
        isMoving = false;
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (timer != null)
                timer.TriggerGameOver();
        }
    }
}