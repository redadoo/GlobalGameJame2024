using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnJumpAction;
    public event EventHandler OnMoveAction;
    public event EventHandler OnRunAction;

    public event EventHandler OnMoveCanceled;
    public event EventHandler OnRunCanceled;

    public static GameInput Instance { get; private set; }

    private PlayerInputAction playerInputAction;
    private void Awake()
    {
        Instance = this;
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
        playerInputAction.Player.Jump.performed += Jump_performed;
        playerInputAction.Player.Move.performed += Move_performed;
        playerInputAction.Player.Run.performed += Run_performed;

        playerInputAction.Player.Run.canceled += Run_canceled;
        playerInputAction.Player.Move.canceled += Move_canceled;

    }

    private void Move_canceled(InputAction.CallbackContext obj) => OnMoveCanceled?.Invoke(this, EventArgs.Empty);

    private void Run_canceled(InputAction.CallbackContext obj) => OnRunCanceled?.Invoke(this, EventArgs.Empty);

    private void Run_performed(InputAction.CallbackContext obj) => OnRunAction?.Invoke(this, EventArgs.Empty);

    private void Move_performed(InputAction.CallbackContext obj) => OnMoveAction?.Invoke(this, EventArgs.Empty);
    
    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) => OnJumpAction?.Invoke(this, EventArgs.Empty);

    public Vector2 GetMovementVectorNormalized() => playerInputAction.Player.Move.ReadValue<Vector2>().normalized;

    public Vector2 GetMovementVector() => playerInputAction.Player.Move.ReadValue<Vector2>();
    
}
