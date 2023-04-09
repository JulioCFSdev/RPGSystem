using System.Collections;
using System.Collections.Generic;
using Action;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class ActionButtonUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshPro;
        [SerializeField] private Button button;

        public void SetBaseAction(BaseAction baseAction)
        {
            textMeshPro.text = baseAction.GetActionName().ToUpper();
        }
    }

}
