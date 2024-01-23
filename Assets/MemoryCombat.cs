using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCombat : MonoBehaviour
{

    //meglio in struct o scriptable object
    [SerializeField] private AudioClip[]    audioClips;
    [SerializeField] private AudioClip[]    soundMinigames;
    [SerializeField] private int            buttonLenght;
    [SerializeField] private int            index;
    [SerializeField] private int            inputLen;


    void Start()
    {
        buttonLenght = GameInput.Instance.GetButtonLenght() - 1;
        
        InitRandoMusic();
    }

    private void Update()
    {
        if (GameInput.Instance.playerMemoryInput.PlayerMemory.ValidInputs.IsPressed())
        {
            CheckMusic();
        }
    }

    void InitRandoMusic()
    {
        soundMinigames = new AudioClip[inputLen];

        for (int i = 0; i < inputLen; i++)
        {
            int randomValue = Random.Range(0, buttonLenght);
            soundMinigames[i] = audioClips[randomValue];
        }
    }

    void CheckMusic()
    {
        print(GameInput.Instance.GetButtonIndex());
        AudioClip audioClip = audioClips[GameInput.Instance.GetButtonIndex()];
        if (soundMinigames[index] == audioClip)
        {
            print("vaaaaa");
        }
        else { 
            print("non va");
        }
    }
}
