using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform  orientation;
    [SerializeField] private Transform  player;
    [SerializeField] private Transform  playerObject;
    [SerializeField] private Transform  combatLookAt;
    [SerializeField] private float      rotationSpeed;

    [Header("Camera")]
    [SerializeField] private GameObject thirdPersonCam;
    [SerializeField] private GameObject topDownCam;

    [SerializeField] private CameraStyle currentStyle;
    public enum CameraStyle
    {
        Basic,
        Topdown
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.F1)) SwitchCameraStyle(CameraStyle.Basic);
        if (Input.GetKeyDown(KeyCode.F3)) SwitchCameraStyle(CameraStyle.Topdown);

        //rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        if (currentStyle == CameraStyle.Basic || currentStyle == CameraStyle.Topdown)
        {
            //rotate player object
            Vector2 input = GameInput.Instance.GetMovementVector();
            Vector3 inputDir = orientation.forward * input.y + orientation.right * input.x;

            if (inputDir != Vector3.zero)
                playerObject.forward = Vector3.Slerp(playerObject.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        thirdPersonCam.SetActive(false);
        topDownCam.SetActive(false);

        if (newStyle == CameraStyle.Basic) thirdPersonCam.SetActive(true);
        if (newStyle == CameraStyle.Topdown) topDownCam.SetActive(true);

        currentStyle = newStyle;
    }

}
