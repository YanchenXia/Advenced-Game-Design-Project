using UnityEngine;

public class WallStartTrigger : MonoBehaviour
{
    public MovingWall wall;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (wall != null)
                wall.StartWall();
        }
    }
}