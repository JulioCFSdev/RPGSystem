using UnityEngine;

namespace Gameplay.Unit
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private Vector3 targetPosition;

        [SerializeField] private float moveSpeed = 4f;
        
        // Update is called once per frame
        void Update()
        {
            float stoppingDistance = 0.1f;
            if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                gameObject.transform.position += moveDirection * (Time.deltaTime * moveSpeed);    
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                Move(new Vector3(4,0,4));
                Vector3.Distance(transform.position, targetPosition);
            }
        }

        private void Move(Vector3 newTargetPosition)
        {
            targetPosition = newTargetPosition;
        }
    }
}
