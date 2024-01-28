using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform              orientation;
    [SerializeField] private Transform              player;
    [SerializeField] private Transform              playerObject;
    [SerializeField] private float                  rotationSpeed;
    [SerializeField] private CinemachineTargetGroup targetGroup;

    [Header("Camera")]
    [SerializeField] private GameObject thirdPersonCam;
    [SerializeField] private GameObject dialogueCam;
    [SerializeField] private GameObject memoryMinigameCamera;
    [SerializeField] private GameObject topDownCam;

    [SerializeField] private CameraStyle currentStyle;
    public enum CameraStyle
    {
        Basic,
        Topdown,
        Dialogue,
        MemoryMiniGame
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InteractionSystem.Instance.OnInteractEnter += OnInteractEnter;
        InteractionSystem.Instance.OnMemoryMiniGameStart += OnMemoryMiniGameStart; ;
        InteractionSystem.Instance.OnInteractExit += OnInteractExit;
        //MemoryGameManager.Instance.OnDeath += OnDeath;

    }

    private void OnDeath(object sender, System.EventArgs e)
    {
        SwitchCameraStyle(CameraStyle.Basic);
    }

    private void OnMemoryMiniGameStart(object sender, GameObject e)
    {
        memoryMinigameCamera.GetComponent<CinemachineVirtualCamera>().Follow = e.transform;
        memoryMinigameCamera.GetComponent<CinemachineVirtualCamera>().LookAt = e.transform;
        SwitchCameraStyle(CameraStyle.MemoryMiniGame);
    }

    private void OnInteractExit(object sender, System.EventArgs e)
    {
        Cinemachine.CinemachineTargetGroup.Target target;
        SwitchCameraStyle(CameraStyle.Basic);
    }

    private void OnInteractEnter(object sender,  GameObject npc)
    {
        CinemachineTargetGroup.Target[] targets = new CinemachineTargetGroup.Target[2];
        targets[0] = new CinemachineTargetGroup.Target { target = player, weight = 1 };
        targets[1] = new CinemachineTargetGroup.Target { target = npc.transform , weight = 1 };
        targetGroup.m_Targets = targets;
        SwitchCameraStyle(CameraStyle.Dialogue);
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

    public void EnableThirdPersonCamera(bool enable)
    {
        thirdPersonCam.SetActive(enable);
    }
    
    public void SwitchCameraStyle(CameraStyle newStyle)
    {
        dialogueCam.SetActive(false);
        thirdPersonCam.SetActive(false);
        topDownCam.SetActive(false);

        if (newStyle == CameraStyle.Basic) thirdPersonCam.SetActive(true);
        if (newStyle == CameraStyle.Topdown) topDownCam.SetActive(true);
        if (newStyle == CameraStyle.Dialogue) dialogueCam.SetActive(true);
        if (newStyle == CameraStyle.MemoryMiniGame) memoryMinigameCamera.SetActive(true);

        currentStyle = newStyle;
    }

}
