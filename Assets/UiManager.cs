using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private bool                       isOnMinigame;
    [SerializeField] private GameObject                 DialogueNoticeUI;
    [SerializeField] private GameObject                 memoryMinigame;
    [SerializeField] private List<TextMeshProUGUI>      keyText;
    [SerializeField] private List<Image>                backGroundImageKeyContainer;


    void Start()
    {
        isOnMinigame = false;
        DialogueNoticeUI.SetActive(false);
        memoryMinigame.SetActive(false);

        InteractionSystem.Instance.OnInteractExit += OnInteractExit;
        InteractionSystem.Instance.OnInteractRange += OnInteractRange;
        InteractionSystem.Instance.OnInteractEnter += OnInteractEnter;
        InteractionSystem.Instance.OnMemoryMiniGameStart += OnMemoryMiniGameStart;

        GameInput.Instance.playerMemoryInput.PlayerMemory.ValidInputs.performed += ValidInputs_performed;
        GameInput.Instance.playerMemoryInput.PlayerMemory.ValidInputs.canceled += ValidInputs_canceled; ;

        for (int i = 0; i < keyText.Count; i++)
            keyText[i].text = GameInput.Instance.GetKeyText().ToList()[i];
    }

    private void ValidInputs_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        int index = GameInput.Instance.GetButtonIndex();
        if (index < 0 || index > backGroundImageKeyContainer.Count) return;
    }

    private void ValidInputs_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!memoryMinigame.activeSelf) return;
        int index = GameInput.Instance.GetButtonIndex();
    }

    private void OnInteractRange(object sender, System.EventArgs e)
    {
        if(!isOnMinigame)
            DialogueNoticeUI.SetActive(true);
    }

    private void OnMemoryMiniGameStart(object sender, GameObject e)
    {
        isOnMinigame = true;

        DialogueNoticeUI.SetActive(false);
        memoryMinigame.SetActive(true);
    }

    private void OnInteractEnter(object sender, GameObject e)
    {

    }

    private void OnInteractExit(object sender, System.EventArgs e)
    {
        isOnMinigame = false;
        DialogueNoticeUI.SetActive(false);
    }
}
