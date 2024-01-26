using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Fireflys
{
    public class ScoreHandler : MonoBehaviour
    {
        [SerializeField] private DoubleVariable score;
        [SerializeField] private DoubleEvent onScoreChanged;
        [SerializeField] private TMP_Text scoreLabel;
        private void Start()
        {
            onScoreChanged.Register(OnScoreChanged);
            scoreLabel.text = score.Value.ToString("0000000");
        }

        private void OnScoreChanged(double newScore)
        {
            scoreLabel.text = newScore.ToString("0000000");
        }
    }
}

