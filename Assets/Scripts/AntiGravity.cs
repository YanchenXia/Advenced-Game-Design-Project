using UnityEngine;

public class AntiGravity : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        //turn off default gravity
        rb.useGravity = false; 
    }

    void FixedUpdate()
    {
        //reverse gravity and make it fall at the same speed as other blocks
        rb.AddForce(-Physics.gravity, ForceMode.Acceleration);
    }
}