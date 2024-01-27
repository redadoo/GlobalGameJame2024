using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fireflys
{
    public class ButtonBlink : MonoBehaviour
    {
        public void Blink()
        {
            TMP_Text targetText = GetComponent<TMP_Text>();

            Sequence blink = DOTween.Sequence();

            blink.Append(targetText.DOFade(0f, 0.1f));
            blink.Append(targetText.DOFade(1f, 0.1f));
            blink.Append(targetText.DOFade(0f, 0.1f));
            blink.Append(targetText.DOFade(1f, 0.1f));
        }
    }
}

