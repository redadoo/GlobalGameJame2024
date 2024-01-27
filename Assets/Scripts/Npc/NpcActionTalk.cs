using UnityEngine;

namespace Fireflys
{
    [CreateAssetMenu(menuName = "Fireflys/NpcActions/Talk")]
    public class NpcActionTalk : NpcAction
    {
        [SerializeField] private string dialogueLine;
        
        public override void Execute()
        {
            MainUIController.Instance.ShowDialogueUI(true);
            MainUIController.Instance.WriteText(dialogueLine);
        }
    }
}

