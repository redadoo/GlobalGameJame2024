using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnJumpAction;

    public static GameInput Instance { get; private set; }

    private PlayerInputAction playerInputAction;
    private void Awake()
    {
        Instance = this;
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
        playerInputAction.Player.Jump.performed += Jump_performed;
    }


    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) => OnJumpAction?.Invoke(this, EventArgs.Empty);

    public Vector2 GetMovementVectorNormalized() => playerInputAction.Player.Move.ReadValue<Vector2>().normalized;

    public Vector2 GetMovementVector() => playerInputAction.Player.Move.ReadValue<Vector2>();
    
}
