using System;
using _GameAssets.Scripts.Boostables;
using _GameAssets.Scripts.Collectables;
using _GameAssets.Scripts.Collectables.Wheats;
using UnityEngine;

namespace _GameAssets.Scripts.Gameplay.Player
{
    public class PlayerInteractionController:MonoBehaviour
    {

        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<ICollectable>(out ICollectable collectable))
            {
                collectable.Collect();
            }
        }


        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<IBoostable>(out IBoostable boostable))
            {
                boostable.Boost(_playerController);
            }
        }
    }
}