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
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.Player;

        motor = GetComponent<PlayerMotor>();
        looker = GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => motor.Jump();
    }

    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Move.ReadValue<Vector2>());
    }

    void LateUpdate()
    {
        looker.ProcessLook(onFoot.Look.ReadValue<Vector2>());
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
