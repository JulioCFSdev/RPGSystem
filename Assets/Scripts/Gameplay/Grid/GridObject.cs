using Gameplay.Systems;

namespace Gameplay.Grid
{
    public class GridObject
    {
        private GridSystem _gridSystem;
        private GridPosition _gridPosition;

        public GridObject(GridSystem gridSystem, GridPosition gridPosition)
        {
            this._gridSystem = gridSystem;
            this._gridPosition = gridPosition;
        }
    }    
}
