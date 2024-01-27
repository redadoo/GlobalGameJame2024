using UnityEngine;
using DG.Tweening;

namespace Fireflys
{
    public class AnimateBlendShape : MonoBehaviour
    {
        // Inspector
        [SerializeField] private SkinnedMeshRenderer targetMeshRenderer;
        
        
        // MonoBehaviour
        private void Start()
        {
            Mesh sharedSkinnedMeshRenderer = targetMeshRenderer.sharedMesh;

            GrowNeck(0.5f).SetLoops(-1);
        }

        private Sequence GrowNeck(float duration)
        {
            Sequence neckSequence = DOTween.Sequence();
            neckSequence.Append(targetMeshRenderer.DoBlendShapeWeight(0, 100, duration / 2).SetEase(Ease.OutCubic));
            neckSequence.Append(targetMeshRenderer.DoBlendShapeWeight(0, 0, duration / 2).SetEase(Ease.InCubic));
            return neckSequence;
        }
    }
    

    public static class TargetMeshRendererExtension
    {
        public static Tween DoBlendShapeWeight(this SkinnedMeshRenderer smr, int index, float value, float duration)
        {
            return DOTween.To(() => smr.GetBlendShapeWeight(index), (x) => smr.SetBlendShapeWeight(index, x), value, duration);
        }
    }
}

