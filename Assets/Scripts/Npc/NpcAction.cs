using UnityEngine;

namespace Fireflys
{
    [System.Serializable]
    public abstract class NpcAction : ScriptableObject
    {
        public int animatorStateValue = 0;
        public abstract void Execute();
    
    }
}

