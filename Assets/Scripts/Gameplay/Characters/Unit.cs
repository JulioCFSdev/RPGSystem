using System;
using Environment.Grid;
using Action;
using UnityEngine;


namespace Gameplay.Characters
{
    public class Unit : MonoBehaviour
    {
        private GridPosition _gridPosition;
        private MoveAction _moveAction;
        private SpinAction _spinAction;
        private BaseAction[] _baseActionsArray;

        private void Awake()
        {
            _moveAction = GetComponent<MoveAction>();
            _spinAction = GetComponent<SpinAction>();
            _baseActionsArray = GetComponents<BaseAction>();
        }

        private void Start()
        {
            _gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            LevelGrid.Instance.AddUnitAtGridPosition(_gridPosition, this);
        }

        private void Update()
        {

            // Update Unit to its new GridPosition.
            GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            if (newGridPosition != _gridPosition)
            {
                LevelGrid.Instance.UnitMovedGridPosition(this, _gridPosition,newGridPosition);
                _gridPosition = newGridPosition;
            }

        }

        public MoveAction GetMoveAction()
        {
            return _moveAction;
        }

        public SpinAction GetSpinAction()
        {
            return _spinAction;
        }

        public GridPosition GetGridPosition()
        {
            return _gridPosition;
        }

        public BaseAction[] GetBaseActionArray()
        {
            return _baseActionsArray;
        }
    }
}
