using Gameplay.Mouse;
using UnityEngine;
using System;
using Environment.Grid;
using Gameplay.Characters;

namespace Environment.Systems
{
    public class UnitActionSystem : MonoBehaviour
    {
        public static UnitActionSystem Instance { get; private set; }
        public event EventHandler OnSelectionUnitChanged;
        [SerializeField] private Unit selectedUnit;
        [SerializeField] private LayerMask unitLayerMask;
        private readonly int _mouseKeyCode = 0;

        private bool _isBusy;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There's more than one  UnitActionSystem!" + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Update()
        {
            if (_isBusy)
            {
                return;
            }
            if (Input.GetMouseButtonDown(_mouseKeyCode))
            {
                if(HandleUnitSelection()) return;
                
                var mousePosition = MouseWorld.GetRaycastPoint();
                GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(mousePosition);

                if (selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
                {
                    SetBusy();
                    selectedUnit.GetMoveAction().Move(mouseGridPosition, ClearBusy);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                SetBusy();
                selectedUnit.GetSpinAction().Spin(ClearBusy);
            }
        }

        private void SetBusy()
        {
            _isBusy = true;
        }
        
        private void ClearBusy()
        {
            _isBusy = false;
        }

        private bool HandleUnitSelection()
        {
            try
            {
                Camera mainCamera = Camera.main;
                // Check if the "mainCamera" is present in the scene.
                if (mainCamera == null)
                {
                    throw new NullReferenceException("Camera.main is null");
                }

                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out Unit unit))
                    {
                        SetSelectedUnit(unit);
                        return true;
                    }
                }

                return false;
            }
            catch (NullReferenceException e)
            {
                Debug.LogError(e);
                throw;
            }

        }

        private void SetSelectedUnit(Unit unit)
        {
            selectedUnit = unit;
            OnSelectionUnitChanged?.Invoke(this, EventArgs.Empty);  
        }

        public Unit GetSelectedUnit()
        {
            return selectedUnit;    
        }
    }    
}
