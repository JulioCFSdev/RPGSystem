using UnityEngine;

namespace Action
{
    public class SpinAction : BaseAction
    {
        private float _totalSpinAmount;
        private bool _isActive;
        private void Update()
        {
            if (!_isActive)
            {
                return;
            }
            
            float spinAmount = 360f * Time.deltaTime;
            transform.eulerAngles += new Vector3(0, spinAmount, 0);
            _totalSpinAmount += spinAmount;
            if (_totalSpinAmount >= 360f)
            {
                _isActive = false;
                _totalSpinAmount = 0f;
            }
        }

        public void Spin()
        {
            _isActive = true;
            Debug.Log("Spin");
        }
    }    
}
