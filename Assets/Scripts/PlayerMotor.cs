using System;

using NUnit.Framework;

using UnityEngine;



public class PlayerMotor : MonoBehaviour

{
    // Calls/References
    public Transform orientation;

    //Movement
    public float speed = 8f;
    public float acceleration = 20f;
    public float deceleration = 25f;
    public float airControl = 0.7f;

    //Jump Movement
    private bool isGrounded => controller.isGrounded;
    public float gravity = -9.8f;
    public float jumpHeight = 2.3f;
    public float fallMultiplier = 1.6f;



    //crouch variables

    public bool crouching = false;
    public float crouchTimer = 0f;
    public float crouchLerpSpeed = 6f;
    public bool lerpCrouch = false;

    //Stopping movement for grapple
    private bool grappling;
    private Vector3 grappleVelocity;
    private bool exitingGrapple;
    private float exitTimer;

    private CharacterController controller;
    private Vector3 currentVelocity;
    private float verticalVelocity;

    // Awake is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {

        controller = GetComponent<CharacterController>();

    }

    // Simplified to prevent confusion
    void Update()

    {
        controlGravity();
        controlCrouch();
    }



    //get inputs from input manager

    public void ProcessMove(Vector2 input)

    {
        Vector3 forward = orientation.forward;
        Vector3 right = orientation.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 targetDirection = forward * input.y + right * input.x;
        if (targetDirection.magnitude > 1f){
            targetDirection.Normalize();
        }
        float accel = isGrounded ? acceleration : acceleration * airControl;
        float decel = isGrounded ? deceleration : deceleration * 0.2f;
        float control = isGrounded ? 1f : airControl;

        Vector3 targetVelocity = targetDirection * speed * control;

        if (targetDirection.magnitude > 0)
        {
            currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, accel * Time.deltaTime);
        }
        else
        {
            currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, decel * Time.deltaTime);
        }

        //prevent drift
        if (currentVelocity.magnitude < 0.05f)
        {
            currentVelocity = Vector3.zero;
        }

        Vector3 finalVelocity;

        if (grappling)
        {
            finalVelocity = grappleVelocity;
        }
        else
        {
            finalVelocity = currentVelocity + Vector3.up * verticalVelocity;
        }

        Vector3 flatVel = new Vector3(finalVelocity.x, 0f, finalVelocity.z);

        if (flatVel.magnitude > speed)
        {
            flatVel = flatVel.normalized * speed;
        }
        finalVelocity = new Vector3(flatVel.x, finalVelocity.y, flatVel.z);

        controller.Move(finalVelocity * Time.deltaTime);
    }



    public void Jump()

    {

        if (isGrounded)

        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

            verticalVelocity *= 1.1f;
        }

    }



    public void Crouch()

    {

        crouching = !crouching;

        crouchTimer = 0;

        lerpCrouch = true;

    }

    private void controlCrouch()
    {
        if (!lerpCrouch) return;

        crouchTimer += Time.deltaTime * crouchLerpSpeed;
        float p = Mathf.Clamp01(crouchTimer);
        p *= p;
        if(crouching)
            controller.height = Mathf.Lerp(controller.height, 1f, p);
        else
            controller.height = Mathf.Lerp(controller.height, 2f, p);    
        if (p >= 1f)
        {
            lerpCrouch = false;
        }
    }

    //Used for grapple pull
    public void JumpToPosition(Vector3 targetPosition, float speed)
    {
        grappling = true;

        Vector3 dir = (targetPosition - transform.position).normalized;
        grappleVelocity = dir * speed;
    }
    public void StopGrappleMovement()
    {
        grappling = false;

        exitingGrapple = true;
        exitTimer = 0.25f;

        // preserve momentum
        currentVelocity += new Vector3(grappleVelocity.x, 0f, grappleVelocity.z) * 0.6f;

        verticalVelocity += 2.5f;
    }

    private void controlGravity()
    {
        //Prevents gravity from slinging player down after grappling
        if (grappling)
        {
            if (verticalVelocity < -2f)
            {
                verticalVelocity = -2f;
            }

            return;
        }
       float gMultiplier = 1f;

        if (exitingGrapple)
        {
            gMultiplier = 0.4f;

            exitTimer -= Time.deltaTime;
            if (exitTimer <= 0f)
                exitingGrapple = false;
        }

        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * gMultiplier * Time.deltaTime;

        if (verticalVelocity < 0f)
        {
            verticalVelocity += gravity * (fallMultiplier - 1f) * gMultiplier * Time.deltaTime;
        }
    }
}

