using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MainUIController : MonoBehaviour
{
    [SerializeField] private CanvasGroup defaultUI;
    [SerializeField] private CanvasGroup dialogueUI;
    [SerializeField] private CanvasGroup interactionUI;
    [SerializeField] private CanvasGroup fadePanel;
    [SerializeField] private TMP_Text dialogueBox;
    
    public static MainUIController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        dialogueUI.alpha = 0;
        interactionUI.alpha = 0;
        fadePanel.alpha = 0;
    }

    public enum State
    {
        Default, Dialogue, Interaction
    }

    public void WriteText(string textToWrite)
    {
        dialogueBox.text = textToWrite;
    }

    public void ShowInteractionUI(bool show)
    {
        interactionUI.DOFade( show?1:0, 0.3f);
    }

    public void ShowDialogueUI(bool show)
    {
        dialogueUI.DOFade( show?1:0, 0.3f); 
    }

    public void FadePanelShow(bool show)
    {
        fadePanel.DOFade(1, 0.3f);
    }
    
}
