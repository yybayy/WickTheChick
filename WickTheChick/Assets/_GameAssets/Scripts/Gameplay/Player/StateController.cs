using System;
using _GameAssets.Scripts.Enums;
using UnityEngine;

namespace _GameAssets.Scripts.Gameplay.Player
{
    public class StateController : MonoBehaviour
    {
        private PlayerState _currentPlayerState = PlayerState.Idle;

        private void Awake()
        {
            _currentPlayerState = PlayerState.Idle;
        }

        public void ChangeState(PlayerState newPlayerState)
        {
            
            if(_currentPlayerState == newPlayerState) { return; }
           
            
            _currentPlayerState = newPlayerState;
            
            
        }

        public PlayerState GetCurrentState()
        {
            return _currentPlayerState;
        }
    }
}