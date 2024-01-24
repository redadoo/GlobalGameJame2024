using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMemoryGame : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private AudioSource adSource;
    bool playerTurn = false;

    private void Start()
    {
        MemoryGameManager.Instance.OnPlayerTurn += MemoryGameManager_OnPlayerTurn;
    }

    private void MemoryGameManager_OnPlayerTurn(object sender, EventArgs eventArgs)
    {
        playerTurn = true;

    }

    private void Update()
    {
        if (playerTurn && GameInput.Instance.playerMemoryInput.PlayerMemory.ValidInputs.WasPressedThisFrame())
        {
            PlayerTurnManager();
        }
    }

    private void PlayerTurnManager()
    {
        print(GameInput.Instance.GetButtonIndex());
        if (index == MemoryGameManager.Instance.GetAudioClipMinigames().Length - 1)
        {
            index = 0;
            MemoryGameManager.Instance.inputLen++;
            playerTurn = false;
            MemoryGameManager.Instance.isEnemyPlaying = false;
            MemoryGameManager.Instance.ChangeMemoryState(MemoryGameManager.MemoryGameState.EnemyTurn);
        }
        AudioClip audioClip = MemoryGameManager.Instance.GetAudioClipArray()[GameInput.Instance.GetButtonIndex()];
        adSource.clip = audioClip;
        adSource.Play();
        if (MemoryGameManager.Instance.GetAudioClipMinigames()[index] == audioClip)
        {
            print("vaaaaa");
            index++;
        }
        else
        {
            print("non va");
            index = 0;
            playerTurn = false;
            MemoryGameManager.Instance.isEnemyPlaying = false;
            MemoryGameManager.Instance.ChangeMemoryState(MemoryGameManager.MemoryGameState.EnemyTurn);
            //playLives --;
        }

    }

}