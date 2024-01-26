using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueUI;

    void Start()
    {
        dialogueUI.SetActive(false);

        InteractionSystem.Instance.OnInteractExit += OnInteractExit;
        InteractionSystem.Instance.OnInteractEnter += OnInteractEnter;
        InteractionSystem.Instance.OnMemoryMiniGameStart += OnMemoryMiniGameStart; ;
    }

    private void OnMemoryMiniGameStart(object sender, GameObject e)
    {

    }

    private void OnInteractEnter(object sender, GameObject e)
    {

    }

    private void OnInteractExit(object sender, System.EventArgs e)
    {

    }

    void Update()
    {
        
    }
}
