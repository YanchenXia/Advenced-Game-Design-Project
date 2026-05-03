using UnityEngine;

public class CustomPlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    [Header("UI Connection")]
    public GravityUI uiManager; 
    public HintUI hintManager;

    [Header("Camera Settings")]
    public Transform playerCamera; 
    public float mouseSensitivity = 2f;
    private float xRotation = 0f; 
    public float interactDistance = 4f;

    [Header("Movement Settings")]
    public float speed = 8f; 
    public float jumpHeight = 1.5f;
    public float playerGravity = -15f; 

    [Header("Crouch Settings")]
    private bool crouching = false;
    private float crouchTimer = 0f;
    private bool lerpCrouch = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Physics.gravity = new Vector3(0, playerGravity, 0); 

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //hint off start
        if (hintManager != null)
        {
            //shows the message for 10 seconds after start
            hintManager.ShowHint("For hints, press 'R' on the yellow blocks", 10f);
        }
    }

    void Update()
    {
        //mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        //gravity flip (G)
        if (Input.GetKeyDown(KeyCode.G))
        {
            Physics.gravity = -Physics.gravity;
            if (uiManager != null) uiManager.ToggleGravityText();
        }

        //change freeze/anchor state (F)
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (uiManager != null) uiManager.ToggleFreezeText();
        }

        //raycast for interacting with hints (r)
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            RaycastHit hit;
            //make laser from center of character
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, interactDistance))
            {
                //sees if lazer hit hint block
                InteractableHint foundHint = hit.collider.GetComponent<InteractableHint>();
                if (foundHint != null)
                {
                    //show hint if yes
                    if (hintManager != null) hintManager.ShowHint(foundHint.hintMessage);
                }
            }
        }

        //crouch (C or CTRL)
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C))
        {
            crouching = !crouching;
            crouchTimer = 0;
            lerpCrouch = true;
        }

        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1f; 
            p *= p; 
            
            if (crouching)
                controller.height = Mathf.Lerp(controller.height, 1f, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2f, p);
            
            if (p > 1f)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }

        //movement and ground check
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal"); 
        float z = Input.GetAxis("Vertical");   

        Vector3 move = transform.right * x + transform.forward * z;
        
        float currentSpeed = crouching ? speed * 0.5f : speed;
        controller.Move(move * currentSpeed * Time.deltaTime);

        //player gravity and jumping
        if (Input.GetButtonDown("Jump") && isGrounded && !crouching)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * playerGravity);
        }

        velocity.y += playerGravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}