using Gameplay.Grid;
using Gameplay.Mouse;
using Gameplay.Systems;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Transform gridDebugObjectPrefab;
    
    private GridSystem _gridSystem;
    void Start()
    {
        _gridSystem = new GridSystem(10, 10, 2f);
        _gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
        Debug.Log(new GridPosition(10,10));
    }

    private void Update()
    {
        Debug.Log(_gridSystem.GetGridPosition(MouseWorld.GetRaycastPoint()));
    }
    
}
