using UnityEngine;

public class ConstantGravity : MonoBehaviour
{
    //match to player gravity
    public float downwardForce = -15f; 
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        //turn off unity's gravity
        rb.useGravity = false; 
    }

    //makes pushing objects smoother
    void FixedUpdate()
    {
        //push down y axis
        rb.AddForce(new Vector3(0, downwardForce, 0), ForceMode.Acceleration);
    }
}