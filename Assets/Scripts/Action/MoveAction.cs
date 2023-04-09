using System.Collections;
using System.Collections.Generic;
using Environment.Grid;
using UnityEngine;
using Gameplay.Characters;

namespace Action
{
    public class MoveAction : BaseAction
    {
        [SerializeField] private float moveSpeed = 4f;
        [SerializeField] private Animator unitAnimator;
        [SerializeField] private int maxMoveDistance = 4;
        
        private Vector3 _targetPosition;
        private readonly string _isWalkingAnimationParameter = "IsWalking";
    
        protected override void Awake()
        {
            base.Awake();
            _targetPosition = transform.position;
        }
        
        // Update is called once per frame
        private void Update()
        {
            if (!isActive)
            {
                return;
            }
            // Acceptance Margin for the new distance that the unit
            float stoppingDistance = 0.1f;
            Vector3 moveDirection = (_targetPosition - transform.position).normalized;
            
            if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance)
            {
                gameObject.transform.position += moveDirection * (Time.deltaTime * moveSpeed);

                // Assigning the value of "true" to the running animation parameter.
                unitAnimator.SetBool(_isWalkingAnimationParameter,true);
            }
            else
            {
                // Assigning the value of "false" to the running animation parameter.
                unitAnimator.SetBool(_isWalkingAnimationParameter,false);
                isActive = false;
            }
            
            // Smooth rotation of the character towards the new selected position.
            float rotateSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
        }
    
        // Delegate New Position To Unit
        public void Move(GridPosition gridPosition)
        {
            this._targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
            isActive = true;
        }

        public bool IsValidActionGridPosition(GridPosition gridPosition)
        {
            List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
            return validGridPositionList.Contains(gridPosition);
        }

        public List<GridPosition> GetValidActionGridPositionList()
        {
            List<GridPosition> validGridPositionList = new List<GridPosition>();

            GridPosition unitGridPosition = unit.GetGridPosition();
            
            // Mapping All Potential possible grid positions within the maximum range
            for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
            {
                for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
                {
                    GridPosition offSetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = unitGridPosition + offSetGridPosition;
                    
                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                    {
                        // Test if it is inside the actual grid bounds
                        continue;
                    }

                    if (unitGridPosition == testGridPosition)
                    {
                        // Same Grid Position where the unit is already at
                        continue;
                    }

                    if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                    {
                        // Grid Position already occupied with another Unit
                        continue;
                    }
                    validGridPositionList.Add(testGridPosition);
                }
            }
            
            return validGridPositionList;
        }
    }    
}
