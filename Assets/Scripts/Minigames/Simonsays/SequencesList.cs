using System.Collections.Generic;
using UnityEngine;

namespace Fireflys
{
    [CreateAssetMenu(menuName ="Fireflys/Minigames/SimonsaysData")]
    public class SequencesList : ScriptableObject
    {
        [SerializeField] private List<SequenceData> Sequences;
    }
    
    [System.Serializable]
    public class SequenceData
    {
        public string uniqueSymbols;
        public bool isRandom = false;
        public string sequence;
    }
}

