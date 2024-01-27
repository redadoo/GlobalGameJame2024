using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    private const string IS_IDLE    = "IsIdle";
    private const string IS_WALKING = "isWalking";
    private const string IS_RUNNING = "IsRunning";


    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameInput.Instance.OnJumpAction += OnJumpAction;
        GameInput.Instance.OnMoveAction += OnMoveAction;
        GameInput.Instance.OnRunAction += OnRunAction;

        GameInput.Instance.OnMoveCanceled += OnMoveCanceled; ;
        GameInput.Instance.OnRunCanceled += OnRunCanceled; ; ;
    }

    private void OnRunCanceled(object sender, System.EventArgs e)
    {
        playerAnimator.SetBool(IS_RUNNING, false);
    }

    private void OnMoveCanceled(object sender, System.EventArgs e)
    {
        playerAnimator.SetBool(IS_WALKING, false);
    }

    private void OnRunAction(object sender, System.EventArgs e)
    {
        playerAnimator.SetBool(IS_RUNNING, true);
    }

    private void OnMoveAction(object sender, System.EventArgs e)
    {

        playerAnimator.SetBool(IS_WALKING, true);
    }

    private void OnJumpAction(object sender, System.EventArgs e)
    {

    }

}
