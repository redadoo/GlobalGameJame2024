using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
namespace Fireflys
{
    [CreateAssetMenu(menuName = "Fireflys/NpcActions/StartMiniGame")]
    [System.Serializable]
    public class NpcActionStartMiniGame: NpcAction
    {
        [SerializeField] private string sceneToStart;
        
        public override void Execute()
        {
            if (string.IsNullOrEmpty(sceneToStart)) return;
            
            Sequence fadeAndStart = DOTween.Sequence();
            fadeAndStart.AppendCallback(() => { MainUIController.Instance.FadePanelShow(true); });
            fadeAndStart.AppendInterval(0.4f);
            fadeAndStart.AppendCallback(()=>{SceneManager.LoadScene(sceneToStart);});
        }
    }
}

