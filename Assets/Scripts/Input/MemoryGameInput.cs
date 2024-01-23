using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameInput : MonoBehaviour
{
    private PlayerInputAction playerInputAction;
    //private event   
    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.PlayerMemory.Enable();
        playerInputAction.PlayerMemory.ValidInputs.ReadValue<KeyCode>();
    }

}
