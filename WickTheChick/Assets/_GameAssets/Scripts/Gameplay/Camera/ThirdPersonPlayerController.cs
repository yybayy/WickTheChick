using System;
using UnityEngine;

namespace _GameAssets.Scripts.Gameplay.Camera
{
    public class ThirdPersonPlayerController : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform orientationTransform;
        [SerializeField] private Transform playerVisualTransform;
        [SerializeField] private float rotationSpeed = 5f;
        private void Update()
        {
            Vector3 viewDirection = playerTransform.position - new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);
            orientationTransform.forward = viewDirection.normalized;
            
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            
            Vector3 dir = orientationTransform.forward * v + orientationTransform.right * h;
            
            if (dir != Vector3.zero)
                playerVisualTransform.forward = Vector3.Slerp(playerVisualTransform.forward, dir.normalized, Time.deltaTime * rotationSpeed);
            
            
        }
        
    }
}