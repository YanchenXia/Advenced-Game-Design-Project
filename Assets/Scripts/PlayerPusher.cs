using UnityEngine;

public class PlayerPusher : MonoBehaviour
{
    public float pushStrength = 3.0f;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;

        //if no physics body or frozen, ignore
        if (rb == null || rb.isKinematic)
        {
            return;
        }

        //if HeavyBlock tag, player cant push
        if (hit.collider.CompareTag("HeavyBlock"))
        {
            return;
        }

        //dont push down on floor if player jumps on
        if (hit.moveDirection.y < -0.3f)
        {
            return;
        }

        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        rb.linearVelocity = pushDirection * pushStrength;
    }
}