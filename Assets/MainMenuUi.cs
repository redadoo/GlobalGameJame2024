using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenuUi : MonoBehaviour
{
    [SerializeField] private Button         playButton;
    [SerializeField] private Button         quitButton;
    [SerializeField] private VideoPlayer    videoPlayer;
    [SerializeField] private RawImage       raw;


    private void Start()
    {
        playButton.onClick.AddListener(() => SceneManager.LoadScene("MAIN"));
        quitButton.onClick.AddListener(() => Application.Quit());

        videoPlayer.loopPointReached += VideoPlayer_loopPointReached;
        videoPlayer.started += VideoPlayer_started; ;

    }

    private void VideoPlayer_started(VideoPlayer source)
    {
        playButton.interactable = false;
        quitButton.interactable = false;
    }

    private void VideoPlayer_loopPointReached(VideoPlayer source)
    {
        Destroy(raw);
        playButton.interactable = true;
        quitButton.interactable = true;
    }



}
