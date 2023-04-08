using System.Collections;
using System.Collections.Generic;
using Environment.Grid;
using UnityEngine;
using Gameplay.Characters;

namespace Action
{
    public class MoveAction : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 4f;
        [SerializeField] private Animator unitAnimator;
        [SerializeField] private int maxMoveDistance = 4;
        
        private Vector3 _targetPosition;
        private readonly string _isWalkingAnimationParameter = "IsWalking";
        private Unit _unit;
    
        private void Awake()
        {
            _unit = GetComponent<Unit>();
            _targetPosition = transform.position;
        }
        
        // Update is called once per frame
        private void Update()
        {
            // Acceptance Margin for the new distance that the unit
            float stoppingDistance = 0.1f;
            if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance)
            {
                Vector3 moveDirection = (_targetPosition - transform.position).normalized;
                gameObject.transform.position += moveDirection * (Time.deltaTime * moveSpeed);
                
                // Smooth rotation of the character towards the new selected position.
                float rotateSpeed = 10f;
                transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
                
                // Assigning the value of "true" to the running animation parameter.
                unitAnimator.SetBool(_isWalkingAnimationParameter,true);
            }
            else
            {
                // Assigning the value of "false" to the running animation parameter.
                unitAnimator.SetBool(_isWalkingAnimationParameter,false);
            }
        }
    
        // Delegate New Position To Unit
        public void Move(Vector3 newTargetPosition)
        {
            _targetPosition = newTargetPosition;
        }

        public List<GridPosition> GetValidActionGridPositionList()
        {
            List<GridPosition> validGridPositionList = new List<GridPosition>();

            GridPosition unitGridPosition = _unit.GetGridPosition();
            
            for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
            {
                for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
                {
                    GridPosition offSetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = unitGridPosition + offSetGridPosition;
                    Debug.Log(testGridPosition);
                }
            }
            
            return validGridPositionList;
        }
    }    
}
