using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Respawn Settings")]
    public Transform respawnPoint;

    //player dies to wall/floor
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Hazard"))
        {
            DieAndRespawn();
        }
    }

    //player dies to pits or invisible zones
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hazard"))
        {
            DieAndRespawn();
        }
    }

    public void DieAndRespawn()
    {
        CharacterController cc = GetComponent<CharacterController>();
        
        if (cc != null) cc.enabled = false; //turn physics off

        //teleport to respawn point
        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;

        if (cc != null) cc.enabled = true; //turn physics back on
    }

    //checkpoint functionality
    public void SetRespawnPoint(Transform newPoint)
    {
        respawnPoint = newPoint;
    }
}

