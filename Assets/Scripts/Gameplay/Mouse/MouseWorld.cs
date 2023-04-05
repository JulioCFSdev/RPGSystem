using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Mouse
{
    public class MouseWorld : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            if (Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // Check if the mouse position on the mainCamera is colliding with any object. => return bool
                print(Physics.Raycast(ray));
            }
        }
    }    
}
