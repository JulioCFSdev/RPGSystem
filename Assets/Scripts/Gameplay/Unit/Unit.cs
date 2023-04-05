using System;
using Gameplay.Mouse;
using UnityEngine;

namespace Gameplay.Unit
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private Vector3 targetPosition;

        [SerializeField] private float moveSpeed = 4f;
        private readonly int _mouseKeyCode = 0;
        private readonly String _isWalkingAnimationParameter = "IsWalking";
        private Animator _unitAnimator;

        private void Awake()
        {
            _unitAnimator = gameObject.GetComponentInChildren<Animator>();
        }

        void Update()
        {
            // Acceptance Margin for the new distance that the unit
            float stoppingDistance = 0.1f;
            if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                gameObject.transform.position += moveDirection * (Time.deltaTime * moveSpeed);
                
                // Smooth rotation of the character towards the new selected position.
                float rotateSpeed = 10f;
                transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
                
                // Assigning the value of "true" to the running animation parameter.
                _unitAnimator.SetBool(_isWalkingAnimationParameter,true);
            }
            else
            {
                // Assigning the value of "false" to the running animation parameter.
                _unitAnimator.SetBool(_isWalkingAnimationParameter,false);
            }
            
            if (Input.GetMouseButtonDown(_mouseKeyCode))
            {
                var mousePosition = MouseWorld.GetRaycastPoint();
                Move(mousePosition);
            }
        }
        
        // Delegate New Position To Unit
        private void Move(Vector3 newTargetPosition)
        {
            targetPosition = newTargetPosition;
        }
    }
}
