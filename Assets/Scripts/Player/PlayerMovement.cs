using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float      moveSpeed;

    [Header("Jump")]
    [SerializeField] private float      airMultiplier;
    [SerializeField] private float      jumpForce;
    [SerializeField] private float      jumpCooldown;
    [SerializeField] private bool       readyToJump;


    [Header("Ground check")]
    [SerializeField] private float      playerHeight;
    [SerializeField] private LayerMask  whatIsGround;
    [SerializeField] private bool       grounded;
    [SerializeField] private float      groundDrag;

    [SerializeField] private Transform  orientation;

    [SerializeField] private Vector2    playerMovementDir;
    [SerializeField] private Vector3    moveDir;

    [SerializeField] private Rigidbody  rb;




    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
        GameInput.Instance.OnJumpAction += OnJumpAction;
    }

    private void OnJumpAction(object sender, System.EventArgs e)
    {
        if (grounded)
        {
            readyToJump = false;

            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void Update()
    {
        Vector3 startRaycast = new(transform.position.x, transform.position.y + playerHeight, transform.position.z);
        grounded = Physics.Raycast(startRaycast, Vector3.down, playerHeight);


        PlayerInput();

        SpeedControl();

        if (grounded)
            rb.drag = groundDrag;
        else 
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void PlayerInput()
    {
        playerMovementDir = GameInput.Instance.GetMovementVector();
    }
    private void MovePlayer()
    {
        // calculate movement direction
        moveDir = orientation.forward * playerMovementDir.y + orientation.right * playerMovementDir.x;

        if (grounded)
            rb.AddForce(10f * moveSpeed * moveDir.normalized, ForceMode.Force);
        else
            rb.AddForce(10f * moveSpeed * airMultiplier *  moveDir.normalized, ForceMode.Force);

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void ResetJump() => readyToJump = true;
}
