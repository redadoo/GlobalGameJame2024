using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Linq;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class MemoryGameManager : MonoBehaviour
{
    public enum MemoryGameState
    {
        EnemyTurn,
        PlayerTurn
    }
    public static MemoryGameManager Instance { get; private set; }

    public event EventHandler OnEnemyTurn;
    public event EventHandler OnPlayerTurn;
    public event EventHandler OnSuccess;
    public event EventHandler OnFailure;
    public event EventHandler OnDeath;
    public event EventHandler OnMemoryMiniGameStart;

    [SerializeField] private MemoryGameState    state;
    [SerializeField] private AudioClip[]        audioClips;
    [SerializeField] private List<AudioClip>    soundMinigames;
    [SerializeField] public  int                inputLen;
    [SerializeField] private bool               isPlaying;
    [SerializeField] private BoolVariable       isSimonSaysCompleted;
    [SerializeField] public int                life;
   

    public AudioClip[] GetAudioClipArray() => audioClips;
    public List<AudioClip> GetAudioClipMinigames() => soundMinigames;
    public bool isEnemyPlaying = false;



    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        life = 3;
        OnMemoryMiniGameStart?.Invoke(this, EventArgs.Empty);
        //InteractionSystem.Instance.OnMemoryMiniGameStart += OnMemoryMiniGameStart;
        isPlaying = true;
        state = MemoryGameState.EnemyTurn;
        InitRandomMusic();
    }

    //private void OnMemoryMiniGameStart(object sender, GameObject e)
    //{
    //    isPlaying = true;
    //}

    void InitRandomMusic()
    {
        soundMinigames = new List<AudioClip>();

        for (int i = 0; i < inputLen; i++)
        {
            int randomValue = Random.Range(0, 7);
            soundMinigames.Add(audioClips[randomValue]);
        }
    }

    private void Update()
    {
        if (life == 0)
        {
            SceneManager.LoadScene("MemoryGame");
        }
        if (inputLen == 7)
            isSimonSaysCompleted.Value = true;

        if (isPlaying)
        {
            if (soundMinigames.Count == 0)
                WinMatch();

            if (state == MemoryGameState.EnemyTurn && isEnemyPlaying == false)
            {
                isEnemyPlaying = true;
                InitRandomMusic();
                OnEnemyTurn?.Invoke(this, EventArgs.Empty);
            }
           
            if (state == MemoryGameState.PlayerTurn)
            {
                OnPlayerTurn?.Invoke(this, EventArgs.Empty);
            }
        }
;
    }

    public int GetLife() => life;

    public void ChangeMemoryState(MemoryGameState newState)
    {
        state = newState;
    }

    public int GetClipPos(AudioClip ac)
    {
        for (int i = 0;i < audioClips.Length;i++)
            if (audioClips[i] == ac) return i;
        return -1;
    }

    public bool CheckClip(AudioClip ac)
    {

        if (soundMinigames[0] == ac)
        {
            soundMinigames.RemoveAt(0);
            return true;
        }
        else
        {
            LoseMatch();
            return false;
        }
    }

    public void WinMatch()
    {
        state = MemoryGameState.EnemyTurn;
        isEnemyPlaying = false;
        inputLen++;
        if (inputLen == audioClips.Length)
        {
            //inserire fine minigame
        }
    }

    public void LoseMatch()
    {
        state = MemoryGameState.EnemyTurn;
        isEnemyPlaying = false;
    }

    public void ResetValue()
    {
        isPlaying = false;
        inputLen = 4;
        state = MemoryGameState.EnemyTurn;
        GameInput.Instance.ChangeInputState(GameInput.InputState.Basic);
        OnDeath?.Invoke(this, EventArgs.Empty);

    }
}
