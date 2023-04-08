using System;
using Environment.Systems;
using UnityEngine;

namespace Gameplay.Characters
{
    public class UnitSelectVisual : MonoBehaviour
    {
        [SerializeField] private Unit unit;
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            UnitActionSystem.Instance.OnSelectionUnitChanged += UnitActionSystem_OnSelectionUnitChanged;
            UpdateVisual();
        }

        private void UnitActionSystem_OnSelectionUnitChanged(object sender, EventArgs empty)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            if (UnitActionSystem.Instance.GetSelectedUnit() == unit)
            {
                _meshRenderer.enabled = true;
            }
            else
            {
                _meshRenderer.enabled = false;
            }
        }
    } 
}
