using UnityEngine;

public class ResetableBlock : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;
    private Rigidbody rb;

    void Start()
    {
        //store block locations
        startPos = transform.position;
        startRot = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    public void ResetToStart()
    {
        //teleport it back
        transform.position = startPos;
        transform.rotation = startRot;

        //remove momentum
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}