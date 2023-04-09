using UnityEngine;
using Gameplay.Characters;

namespace Action
{
    public abstract class BaseAction : MonoBehaviour
    {
        protected Unit unit;
        protected bool isActive;

        protected virtual void Awake()
        {
            unit = GetComponent<Unit>();
        }
    }    
}
