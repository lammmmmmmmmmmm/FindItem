using UnityEngine;
using UnityEngine.UI;

namespace Survivor.UI
{
    [RequireComponent(typeof(Button))]
    public class ShortCut : MonoBehaviour
    {
        [SerializeField] private string panelName;
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClickShortCut);
        }

        private void OnClickShortCut()
        {
            PanelManager.Instance.OpenPanel(panelName);
        }
    }
}

