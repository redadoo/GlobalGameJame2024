using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMemoryGame : MonoBehaviour
{
    [SerializeField] private int                index;
    [SerializeField] private int                life;
    [SerializeField] private AudioSource        adSource;
    [SerializeField] private List<AudioClip>    PlayedClip;
    [SerializeField] private AudioClip          failedAttempt;
    bool playerTurn = false;

    public static PlayerMemoryGame Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        MemoryGameManager.Instance.OnPlayerTurn += MemoryGameManager_OnPlayerTurn;
        MemoryGameManager.Instance.OnEnemyTurn += OnEnemyTurn; ;
    }

    private void OnEnemyTurn(object sender, EventArgs e)
    {
        playerTurn = false;
    }

    private void MemoryGameManager_OnPlayerTurn(object sender, EventArgs eventArgs)
    {
        playerTurn = true;
    }

    private void Update()
    {
        if (playerTurn && GameInput.Instance.playerMemoryInput.PlayerMemory.ValidInputs.WasPressedThisFrame())
        {
            PlayClip(GameInput.Instance.GetButtonIndex());
        }
    }

    private void PlayClip(int index)
    {
        adSource.clip = MemoryGameManager.Instance.GetAudioClipArray()[index];
        adSource.Play();
        UiManager.Instance.AnimBar(true, MemoryGameManager.Instance.GetClipPos(adSource.clip));
        UiManager.Instance.AnimBar(false, MemoryGameManager.Instance.GetClipPos(adSource.clip));
        if(!MemoryGameManager.Instance.CheckClip(adSource.clip))
        {
            adSource.clip = failedAttempt;
            adSource.Play();
            MemoryGameManager.Instance.life--;
        }
    }

    public int GetLife() => MemoryGameManager.Instance.life;

}