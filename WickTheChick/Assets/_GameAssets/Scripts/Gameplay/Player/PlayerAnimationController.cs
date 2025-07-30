using System;
using _GameAssets.Scripts.Enums;
using UnityEngine;

namespace _GameAssets.Scripts.Gameplay.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
     [SerializeField] private Animator animator;
     
     private PlayerController _playerController;
     private StateController _stateController;

     private void Awake()
     {
   _playerController = GetComponent<PlayerController>();
   _stateController = GetComponent<StateController>();
     }

     private void Start()
     {
         _playerController.OnPlayerJumped += PlayerController_OnPlayerJumped;
     }
     
     private void Update()
     {
         HandlePlayerAnimation();
     }
     
     private void PlayerController_OnPlayerJumped()
     {
         animator.SetBool(Consts.PlayerAnimations.IS_JUMPING, true);
         Invoke(nameof(ResetJumping),0.5f);
     }
     private void ResetJumping()=> animator.SetBool(Consts.PlayerAnimations.IS_JUMPING, false);

     private void HandlePlayerAnimation()
     {
         var currentState = _stateController.GetCurrentState();
         
         switch (currentState)
         {
             case PlayerState.Idle:
                 animator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                 animator.SetBool(Consts.PlayerAnimations.IS_WALKING, false);
                 break;
             
             case PlayerState.Walking:
                 animator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                 animator.SetBool(Consts.PlayerAnimations.IS_WALKING, true);
                 break;
             
             case PlayerState.Sliding:
                 animator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                 animator.SetBool(Consts.PlayerAnimations.IS_WALKING, true);
                 break;
             
             case PlayerState.SlideIdle:
                 animator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                 animator.SetBool(Consts.PlayerAnimations.IS_WALKING, false);
                 break;
             
         }
     }
     
     
     
    }
    
    
}
