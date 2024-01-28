using UnityAtoms.BaseAtoms;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Fireflys
{
    public class BackToMainScene : MonoBehaviour
    {
        [SerializeField] private BoolVariable condition;
        [SerializeField] private CanvasGroup fadePanel;
        private bool isFading = false;

        private void Update()
        {
            if (condition.Value == true && !isFading)
            {
                isFading = true;
                Sequence fadeAndStart = DOTween.Sequence();
                fadeAndStart.Append(fadePanel.DOFade(1f,0.4f));
                fadeAndStart.AppendCallback(() => { SceneManager.LoadScene("MAIN"); });
            }
        }
    }
}

