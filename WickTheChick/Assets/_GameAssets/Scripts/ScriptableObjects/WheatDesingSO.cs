using UnityEngine;

namespace _GameAssets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "WheatDesingSO",menuName = "ScriptableObject/WheatDesingSO")]
    public class WheatDesingSO : ScriptableObject
    {
        [SerializeField] private float _increaseDecreaseMultiplier;
        [SerializeField] private float _resetBoostDuration;
        
        public float IncreaseDecreaseMultiplier => _increaseDecreaseMultiplier;
        public float ResetBoostDuration => _resetBoostDuration;
    }
}