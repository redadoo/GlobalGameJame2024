using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform  orientation;
    [SerializeField] private Transform  player;
    [SerializeField] private Transform  playerObject;
    [SerializeField] private Rigidbody  rb;
    [SerializeField] private float      rotationSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        //rotate player object
        Vector2 input = GameInput.Instance.GetMovementVector();
        Vector3 inputDir = orientation.forward * input.y + orientation.right * input.x;

        if (inputDir != Vector3.zero)
            playerObject.forward = Vector3.Slerp(playerObject.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
    }

}
