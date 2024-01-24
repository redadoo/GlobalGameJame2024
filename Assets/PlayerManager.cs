using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public enum PlayerState
    {
        Basic,
        InDialogue,
    }

    [SerializeField] private PlayerState myState;

    private void Awake()
    {
        myState = PlayerState.Basic;
    }



}
