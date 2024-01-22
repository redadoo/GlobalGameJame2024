using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private PlayerInputAction playerInputAction;
    private void Awake()
    {
        Instance = this;
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized() => playerInputAction.Player.Move.ReadValue<Vector2>().normalized;

    public Vector2 GetMovementVector() => playerInputAction.Player.Move.ReadValue<Vector2>();

}
