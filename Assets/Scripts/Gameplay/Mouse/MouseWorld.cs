using System;
using UnityEngine;

namespace Gameplay.Mouse
{
    public class MouseWorld : MonoBehaviour
    {
        [SerializeField] private LayerMask mousePlaneLayerMask;

        private static MouseWorld _instance;

        private void Awake()
        {
            _instance = this;
        }
        
        // Return Mouse Position in Floor
        public static Vector3 GetRaycastPoint()
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
                print("Mouse Position in World is enable ? : " + Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _instance.mousePlaneLayerMask));
                return raycastHit.point;
            }
            catch (NullReferenceException e)
            {
                Debug.LogError(e);
                throw;
            }
        }
    }    
}
