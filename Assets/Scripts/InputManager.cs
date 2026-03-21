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
    void Awake()
    {  
        playerInput = new PlayerInput();
        onFoot = playerInput.Player;

        motor = GetComponent<PlayerMotor>();
        looker = GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Crouch.performed += ctx => motor.Crouch();
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        moveInput = onFoot.Move.ReadValue<Vector2>();
        lookInput = onFoot.Look.ReadValue<Vector2>();

        motor.ProcessMove(moveInput);
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
