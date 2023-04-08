using System;
using Environment.Grid;
using Gameplay.Mouse;
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
            _unit.GetMoveAction().GetValidActionGridPositionList();
        }
    }
}
