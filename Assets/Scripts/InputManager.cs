using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    //Regular movement named onFoot since anti gravity might require a different input system.
    // Remove the this note if that ends up not being the case.
    private PlayerInput.PlayerActions onFoot;

    private PlayerMotor motor;
    private PlayerLook looker;
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

        onFoot.Jump.performed += ctx => jumpPressed = true;
        onFoot.Crouch.performed += ctx => motor.Crouch();
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
