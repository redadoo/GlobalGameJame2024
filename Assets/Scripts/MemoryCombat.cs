using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCombat : MonoBehaviour
{
    [SerializeField] private AudioClip[]    audioClips;
    [SerializeField] private AudioClip[]    soundMinigames;

    public static MemoryCombat Instance { get; private set; }


    private void Update()
    {
        
    }


    private void Start()
    {
        Instance = this;
    }


    void Playesoidn(int index)
    {

    }

    void generassa()
    {

    }



}
