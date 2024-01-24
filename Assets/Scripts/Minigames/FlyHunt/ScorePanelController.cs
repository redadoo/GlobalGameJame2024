using System;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Fireflys
{
    public class ScorePanelController : MonoBehaviour
    {
        [SerializeField] private DoubleVariable score;
        [SerializeField] private DoubleVariable highscore;
        [SerializeField] private TMP_Text scoreLabel;
        [SerializeField] private TMP_Text highscoreLabel;
        
        private void OnEnable()
        {
            RefreshValues();
        }

        private void RefreshValues()
        {
            scoreLabel.text = $"Score {score.Value.ToString("0000000")}";
            highscoreLabel.text = $"Highscore {highscore.Value.ToString("0000000")}";
        }
    }
}

