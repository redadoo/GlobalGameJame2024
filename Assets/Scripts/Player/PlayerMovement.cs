using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    [SerializeField] private Vector2    playerInput;
    [SerializeField] private Vector3    moveDir;

    [SerializeField] private Rigidbody  rb;




    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + .2f, whatIsGround);

        playerInput = GameInput.Instance.GetMovementVector();
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

    private void MovePlayer()
    {
        // calculate movement direction
        moveDir = orientation.forward * playerInput.y + orientation.forward * playerInput.x; 

        rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new (rb.velocity.x, 0, rb.velocity.y);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump() => readyToJump = true;
}
