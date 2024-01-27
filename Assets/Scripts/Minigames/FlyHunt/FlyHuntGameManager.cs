using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Fireflys;
using TMPro;
using Unity.Mathematics;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;

public class FlyHuntGameManager : MonoBehaviour
{
    [SerializeField] private double pointsToWin = 0;


    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject gameplayScreen;
    [Space] [SerializeField] private float gameDurationSeconds = 45f;
    [Space] [SerializeField] private FlyController flyPrefab;
    [SerializeField] private List<GameObject> spawnPoints;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip titleScreenMusic;
    [SerializeField] private AudioClip gameplayMusic;
    [SerializeField] private AudioClip endGameMusic;
    [SerializeField] private AudioClip startSound;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private TMP_Text highscoreLabel;
    [Space]
    [SerializeField] private DoubleVariable score;
    [SerializeField] private DoubleVariable highscore;
    [Space]
    [SerializeField] private BoolVariable isFlyHuntCompleted;
    
    private GameState state = GameState.Title;

    private float timeBetweenSpawn = 0f;
    private float timer = 0f;
    
    enum GameState
    {
        Title,Gameplay,End,Score
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        highscoreLabel.text = $"Highscore {highscore.Value.ToString("0000000")}";
        titleScreen.SetActive(true);
        gameplayScreen.SetActive(false);
        audioSource.clip = titleScreenMusic;
        audioSource.Play();
        timer = 0f;
    }
    
    
    private void Update()
    {
        if (state == GameState.Gameplay)
        {
            timeBetweenSpawn -= Time.deltaTime;
            timer += Time.deltaTime;
            
            if (timeBetweenSpawn <= 0)
            {
                SpawnFly();
                timeBetweenSpawn = 2f;
            }

            if (timer >= gameDurationSeconds)
            {
                state = GameState.End;
            }
        }

        if (state == GameState.End)
        {
            var flys = gameplayScreen.GetComponentsInChildren<FlyController>();
            if (flys.Length == 0)
            {
                state = GameState.Score;
                ShowScorePanel();
            }
        }

    }
    
    
    public void StartGame()
    {
        scorePanel.SetActive(false);
        Sequence startGameSequence = DOTween.Sequence();
        startGameSequence.Append(audioSource.DOFade(0, 0.1f));
        startGameSequence.AppendCallback(() => { audioSource.Stop();
            audioSource.volume = 0.069f; audioSource.PlayOneShot(startSound); });
        startGameSequence.AppendInterval(2f);

        startGameSequence.AppendCallback(() =>
        {
            titleScreen.SetActive(false);
            gameplayScreen.SetActive(true);
            audioSource.clip = gameplayMusic;
            audioSource.Play();
            state = GameState.Gameplay;
        });
        
    }

    public void RestartGame()
    {
        scorePanel.SetActive(false);
        state = GameState.Gameplay;
        timer = 0f;
        audioSource.clip = gameplayMusic;
        audioSource.Play();
    }

    public void ShowScorePanel()
    {
        audioSource.clip = endGameMusic;
        audioSource.Play();
        if (score.Value > highscore.Value)
            highscore.Value = score.Value;
        ShowScore();
    }
    
    public void QuitGame()
    {
        //
    }

    public void SpawnFly()
    {
        GameObject selectedSpawn = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];
        Instantiate(flyPrefab, selectedSpawn.transform.position, quaternion.identity,gameplayScreen.transform);
    }

    private void ShowScore()
    {
        scorePanel.SetActive(true);
    }
}
