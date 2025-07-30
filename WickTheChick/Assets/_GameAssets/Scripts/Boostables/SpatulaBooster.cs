using _GameAssets.Scripts.Gameplay.Player;
using UnityEngine;

namespace _GameAssets.Scripts.Boostables
{
   public class SpatulaBooster: MonoBehaviour, IBoostable
   { 
       [SerializeField] private Animator _spatulaAnimator;
      [SerializeField] private  float _jumpForce = 20; 
      private bool _isActivated = false;
      public void Boost(PlayerController playerController)
      {
          if (_isActivated){return;}
          PlayBoostAnimation();
        Rigidbody _rigidbody = playerController.GetRigidBody();
        _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0, _rigidbody.linearVelocity.z);
        _rigidbody.AddForce(transform.forward * _jumpForce, ForceMode.Impulse);
        _isActivated = true;
      }
      
      private void PlayBoostAnimation()
      {
          _spatulaAnimator.SetTrigger(Consts.OtherAnimations.IS_SPATULA_JUMPING);
          Invoke(nameof(ResetActivation), 0.2f);
      }

      private void ResetActivation()
      {
          _isActivated = false;
      }
   }
   
   
}
