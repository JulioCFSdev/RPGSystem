using Gameplay.Mouse;
using UnityEngine;

namespace Gameplay.Unit
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private Vector3 targetPosition;

        [SerializeField] private float moveSpeed = 4f;
        [SerializeField] private int mouseKeyCode = 0; 
        
        void Update()
        {
            float stoppingDistance = 0.1f;
            // Acceptance Margin for the new distance that the unit
            if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                gameObject.transform.position += moveDirection * (Time.deltaTime * moveSpeed);    
            }
            
            if (Input.GetMouseButtonDown(mouseKeyCode))
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
