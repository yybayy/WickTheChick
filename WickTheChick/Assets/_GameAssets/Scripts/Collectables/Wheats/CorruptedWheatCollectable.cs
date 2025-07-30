using _GameAssets.Scripts.Gameplay.Player;
using _GameAssets.Scripts.ScriptableObjects;
using UnityEngine;

namespace _GameAssets.Scripts.Collectables.Wheats
{
    public class CorruptedWheatCollectable : MonoBehaviour, ICollectable
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private WheatDesingSO _wheatDesingSo;

        public void Collect()
        {
            _playerController.SetJumpForce(_wheatDesingSo.IncreaseDecreaseMultiplier, _wheatDesingSo.ResetBoostDuration);
            Destroy(this.gameObject);
        }
    }
}