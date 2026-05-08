using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.PlayerActions onFoot;

    private PlayerMotor motor;
    private PlayerLook looker;
    private Grappling grapple;
    private Vector2 moveInput;
    private Vector2 lookInput;

    private bool jumpPressed;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Awake()
    {  
        playerInput = new PlayerInput();
        onFoot = playerInput.Player;

        motor = GetComponent<PlayerMotor>();
        looker = GetComponent<PlayerLook>();
        grapple = GetComponent<Grappling>();

        onFoot.Jump.performed += ctx => jumpPressed = true;
        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Grapple.performed += ctx => grapple.StartGrapple();
        onFoot.Grapple.canceled += ctx => grapple.StopGrapple();
    }

    void Update()
    {
        moveInput = onFoot.Move.ReadValue<Vector2>();
        lookInput = onFoot.Look.ReadValue<Vector2>();

        motor.ProcessMove(moveInput);
        if (jumpPressed)
        {
            motor.Jump();
            jumpPressed = false;
        }
    }

    void LateUpdate()
    {
        looker.ProcessLook(lookInput);
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
    }
}
