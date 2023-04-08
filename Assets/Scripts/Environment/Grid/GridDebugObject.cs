using TMPro;
using UnityEngine;

namespace Environment.Grid
{
    public class GridDebugObject : MonoBehaviour
    {
        [SerializeField] private TextMeshPro textDebugGridObject;
        private GridObject _gridObject;

        public void SetGridObject(GridObject gridObject)
        {
            this._gridObject = gridObject;
        }

        public void Update()
        {
            textDebugGridObject.text = _gridObject.ToString();
        }
    }    
}
