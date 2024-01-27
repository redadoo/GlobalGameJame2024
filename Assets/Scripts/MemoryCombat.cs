using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCombat : MonoBehaviour
{
    [SerializeField] private AudioClip[]    audioClips;
    [SerializeField] private AudioClip[]    soundMinigames;

    public static MemoryCombat Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        MemoryGameManager.Instance.OnEnemyTurn += OnEnemyTurn;
    }

    private void OnEnemyTurn(object sender, System.EventArgs e)
    {

    }


}
