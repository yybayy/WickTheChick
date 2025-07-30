using System;
using _GameAssets.Scripts.Enums;
using _GameAssets.Scripts.Gameplay.Player;
using UnityEngine;

namespace _GameAssets.Scripts.Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {

        public event Action OnPlayerJumped;
        public event Action<PlayerState> OnPlayerStateChanged;
        
        [Header("Movement")]
        [SerializeField] private float _movementSpeed = 10f;
        [SerializeField] private Transform _orientation;

        [Header("Jump")]
        [SerializeField] private float _jumpForce = 100f;
        [SerializeField] private bool _canJump = true;
        [SerializeField] private float _jumpCooldown = .5f;
        [SerializeField] private float _airMultiplier;
        [SerializeField] private float _airDrag;
        //[SerializeField] private float _gravity = -9.81f;

        [Header("Slide")]
        [SerializeField] private float _slideMultiplier = 2f;
        [SerializeField] private bool _isSliding;
        [SerializeField] private float _slideDrag;
        
        [Header("Ground Check Settings")]
        [SerializeField] private float _groundDrag;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _playerHeight;


        private float _startingMovementSpeed;
        private float _startingJumpForce;
        
        private Rigidbody _rb;
        private StateController _stateController;
        private Vector3 _dir;
        private float _horizontalInput, _verticalInput;

        
        private void Awake()
        {
            _stateController = GetComponent<StateController>();
            _rb = GetComponent<Rigidbody>();
            _rb.freezeRotation = true;
            _playerHeight = transform.localScale.y;
            
            _startingMovementSpeed = _movementSpeed;
            _startingJumpForce = _jumpForce;
        }
        

        private void Update()
        {
            HandleInputs();
            HandleStates();
            HandleSlide();
            if (Input.GetKeyDown(KeyCode.Space) && _canJump && IsGrounded()) {HandleJump();}
            LimitPlayerSpeed();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleInputs()
        {
            _horizontalInput = Input.GetAxisRaw("Horizontal");
            _verticalInput = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown(KeyCode.C))
            {
                _isSliding = true;
                Debug.Log("Sliding");
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _isSliding = false;
                Debug.Log("Not Sliding");
            }
            else if (Input.GetKeyDown(KeyCode.Space) && _canJump && IsGrounded())
            {
                _canJump = false;
                HandleJump();
                Invoke(nameof(ResetJumping), _jumpCooldown);
            }
            /*else if(!IsGrounded())
            {
              _rb.AddForce(0,gravity,0 , ForceMode.Impulse);
            }*/
        }

        private void HandleStates()
        {
            
            Debug.Log($"_stateController: {_stateController}");
    
            if (_stateController == null)
            {
                    Debug.LogError("HATA: _stateController nesnesi NULL!");
            }
            else
            {
                Debug.Log("Sorun yok");
            }
            
            Vector3 movementDirection = GetMovementDirection();
            bool isGrounded = IsGrounded();
            bool isSliding = IsSliding();  
            var currentState = _stateController.GetCurrentState();
            
            
            

            var newState = currentState switch
            {
                _ when movementDirection == Vector3.zero && isGrounded && !isSliding => PlayerState.Idle,
                _ when movementDirection != Vector3.zero && isGrounded && !isSliding => PlayerState.Walking,
                _ when !_canJump && !isGrounded => PlayerState.Jumping,
                _ when movementDirection != Vector3.zero && isGrounded && isSliding => PlayerState.Sliding,
                _ when movementDirection == Vector3.zero && isGrounded && isSliding => PlayerState.SlideIdle,
                _ => currentState 
            };

            if (newState != currentState)
            {
                _stateController.ChangeState(newState);
                OnPlayerStateChanged?.Invoke(newState);
            }

        }
        
        private void HandleMovement()
        {
            _dir = _orientation.forward * _verticalInput + _orientation.right * _horizontalInput;
            float forceMultiplier = _stateController.GetCurrentState() switch
            {
                PlayerState.Walking => 1f,
                PlayerState.Sliding => _slideMultiplier,
                PlayerState.Jumping => _airMultiplier,
                _ => 1f

            };
            _rb.AddForce(_dir.normalized * _movementSpeed * forceMultiplier, ForceMode.Force);
         
        }

        private void HandleJump()
        {
            OnPlayerJumped?.Invoke();
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        private void HandleSlide()
        {
            _rb.linearDamping = _stateController.GetCurrentState() switch
            {
                
                PlayerState.Walking => _groundDrag,
                PlayerState.Sliding => _slideDrag,
                PlayerState.Jumping => _airDrag,
                _ => _rb.linearDamping

            };
        }

        private void LimitPlayerSpeed()
        {
            Vector3 flatVelocity = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);

            if (flatVelocity.magnitude > _movementSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
                _rb.linearVelocity = new Vector3(limitedVelocity.x, _rb.linearVelocity.y, _rb.linearVelocity.z);
            }
        }

        private void ResetJumping()
        {
            _canJump = true;
        }

        #region Helper Functions

        private bool IsGrounded() => Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
        private Vector3 GetMovementDirection() => _dir.normalized;
        
        public Rigidbody GetRigidBody() => _rb;
        private bool IsSliding() => _isSliding;

        public void SetMovementSpeed(float speed, float duration)
        {
            _movementSpeed += speed;
            Invoke(nameof(ResetMovementSpeed), duration);
        }

        private void ResetMovementSpeed()
        {
            _movementSpeed = _startingMovementSpeed;
        }
            
        
        public void SetJumpForce(float force, float duration)
        {
            _jumpForce += force;
            Invoke(nameof(ResetJumpForce), duration);
        }

        private void ResetJumpForce()
        {
            _jumpForce = _startingJumpForce;
        }
        
        #endregion
        
    }
}