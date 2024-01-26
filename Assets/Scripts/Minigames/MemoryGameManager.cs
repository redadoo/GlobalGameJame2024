using UnityEngine;
using System;
using Random = UnityEngine.Random;

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

    [SerializeField] private MemoryGameState state;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioClip[] soundMinigames;
    [SerializeField] private int buttonLenght;
    [SerializeField] public int inputLen;
    [SerializeField] private float timer = 1f;
    public AudioClip[] GetAudioClipArray() => audioClips;
    public AudioClip[] GetAudioClipMinigames() => soundMinigames;
    public bool isEnemyPlaying = false;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        buttonLenght = GameInput.Instance.GetButtonLenght() - 1;
        state = MemoryGameState.EnemyTurn;
    }

    void InitRandomMusic()
    {
        soundMinigames = new AudioClip[inputLen];

        for (int i = 0; i < inputLen; i++)
        {
            int randomValue = Random.Range(0, buttonLenght);
            soundMinigames[i] = audioClips[randomValue];
        }
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (state == MemoryGameState.EnemyTurn && isEnemyPlaying == false)
        {
            timer = 20f;
            isEnemyPlaying = true;
            InitRandomMusic();
            OnEnemyTurn?.Invoke(this, EventArgs.Empty);
            
        }
           
        if (state == MemoryGameState.PlayerTurn)
        {
            OnPlayerTurn?.Invoke(this, EventArgs.Empty);

        }

        //  inputLen++;
        //InitRandomMusic();
    }

    public void ChangeMemoryState(MemoryGameState newState)
    {
        state = newState;
    }
}
