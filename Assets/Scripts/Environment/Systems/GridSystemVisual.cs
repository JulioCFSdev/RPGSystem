using System.Collections.Generic;
using Environment.Grid;
using UnityEngine;
using Gameplay.Characters;

namespace Environment.Systems
{
    public class GridSystemVisual : MonoBehaviour
    {
        [SerializeField] private Transform gridSystemVisualSinglePrefab;
        public static GridSystemVisual Instance { get; private set; }

        private GridSystemVisualSingle[,] _gridSystemVisualSingleArray;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There's more than one GridSystemVisual!" + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            _gridSystemVisualSingleArray = new GridSystemVisualSingle[
                LevelGrid.Instance.GetWidth(),
                LevelGrid.Instance.GetHeight()
            ];
            
            for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
            {
                for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);

                    Transform gridSystemVisualSingleTransform = Instantiate(gridSystemVisualSinglePrefab,
                        LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);

                    _gridSystemVisualSingleArray[x, z] =
                        gridSystemVisualSingleTransform.GetComponent<GridSystemVisualSingle>();
                }
            }
        }

        private void Update()
        {
            UpdateGridVisual();
        }

        public void HideAllGridPosition()
        {
            for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
            {
                for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
                {
                    _gridSystemVisualSingleArray[x, z].Hide();
                }
            }
        }

        public void ShowGridPositionList(List<GridPosition> gridPositionList)
        {
            foreach (GridPosition gridPosition in gridPositionList)
            {
                _gridSystemVisualSingleArray[gridPosition.x, gridPosition.z].Show();
            }
        }

        private void UpdateGridVisual()
        {
            Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
            HideAllGridPosition();
            ShowGridPositionList(selectedUnit.GetMoveAction().GetValidActionGridPositionList());
        }
    }    
}
