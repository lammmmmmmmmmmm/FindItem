using UnityEngine;
using UnityEngine.UI;

namespace Survivor.UI
{
    public class PanelMoreTime : PanelBase
    {
        [Header("UI Components")]
        [SerializeField] private Button _claimButton;
        [SerializeField] private Button _closeButton;

        protected override void Setup()
        {
            base.Setup();
            _claimButton.onClick.RemoveAllListeners();
            _closeButton.onClick.RemoveAllListeners();

            _claimButton.onClick.AddListener(OnClickClaim);
            _closeButton.onClick.AddListener(OnClickClose);
        }

        private void OnClickClaim()
        {
            Close();
        }

        private void OnClickClose()
        {
            Close();
        }
    }
}

