using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using System.Drawing;
using Color = UnityEngine.Color;
using System;

public class UiManager : MonoBehaviour
{

    [SerializeField] private bool                       isOnMinigame;
    [SerializeField] private GameObject                 DialogueNoticeUI;
    [SerializeField] private GameObject                 memoryMinigame;
    [SerializeField] private TextMeshProUGUI            lifePlayer;
    [SerializeField] private List<TextMeshProUGUI>      keyText;
    [SerializeField] private List<GameObject>           container;
    [SerializeField] private List<Color>                colorList;


    private const string VAR_ANIMATOR = "IsClicked";

    public static UiManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

        //InteractionSystem.Instance.OnInteractExit += OnInteractExit;
        //InteractionSystem.Instance.OnInteractRange += OnInteractRange;
        //InteractionSystem.Instance.OnInteractEnter += OnInteractEnter;
        //InteractionSystem.Instance.OnMemoryMiniGameStart += OnMemoryMiniGameStart;

        MemoryGameManager.Instance.OnDeath += OnDeath;


        for (int i = 0; i < keyText.Count; i++)
            keyText[i].text = GameInput.Instance.GetKeyText().ToList()[i];

        foreach (GameObject container in container)
        {
            colorList.Add(container.transform.GetChild(0).GetComponent<Image>().color);
        }
        isOnMinigame = false;
        DialogueNoticeUI.SetActive(false);
        memoryMinigame.SetActive(false);
        isOnMinigame = true;

        DialogueNoticeUI.SetActive(false);
        memoryMinigame.SetActive(true);
        GameInput.Instance.ChangeInputState(GameInput.InputState.MemoryMinigame);
    }

    private void OnDeath(object sender, EventArgs e)
    {
        isOnMinigame = false;
        memoryMinigame.SetActive(false);
    }

    private void Update()
    {
        lifePlayer.text = (PlayerMemoryGame.Instance.GetLife()).ToString();
    }

    //private void OnInteractRange(object sender, System.EventArgs e)
    //{
    //    if(!isOnMinigame)
    //        DialogueNoticeUI.SetActive(true);
    //}

    //private void OnMemoryMiniGameStart(object sender, GameObject e)
    //{
    //    isOnMinigame = true;

    //    DialogueNoticeUI.SetActive(false);
    //    memoryMinigame.SetActive(true);
    //    GameInput.Instance.ChangeInputState(GameInput.InputState.MemoryMinigame);
    //}

    //private void OnInteractEnter(object sender, GameObject e)
    //{

    //}

    //private void OnInteractExit(object sender, System.EventArgs e)
    //{
    //    isOnMinigame = false;
    //    DialogueNoticeUI.SetActive(false);
    //}

    static public Color ChangeColorValue(Color color)
    {
        float H, S, V;

        Color.RGBToHSV(color, out H, out S, out V);
        return Color.HSVToRGB(H, 1, 1);
    }

    public void AnimBar(bool b, int index)
    {
        if(b)
        {
            container[index].transform.GetChild(0).GetComponent<Image>().color = ChangeColorValue(container[index].transform.GetChild(0).GetComponent<Image>().color);
            container[index].GetComponent<Animator>().SetTrigger(VAR_ANIMATOR);
        }
        else
        {
            container[index].transform.GetChild(0).GetComponent<Image>().color = colorList[index];
        }
    }
}
