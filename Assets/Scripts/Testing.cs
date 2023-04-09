using Environment.Grid;
using Environment.Systems;
using Gameplay.Characters;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GridSystemVisual.Instance.HideAllGridPosition();
            GridSystemVisual.Instance.ShowGridPositionList(_unit.GetMoveAction().GetValidActionGridPositionList());
        }
    }
}
