using UnityEngine;
using UnityEngine.UI;

namespace Survivor.UI
{
    public class PanelLose : PanelBase
    {
        [SerializeField] private Button closeButton;

        protected override void Setup()
        {
            base.Setup();
            closeButton.onClick.AddListener(Close);
        }
    }
}
