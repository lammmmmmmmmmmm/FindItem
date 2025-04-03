using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Survivor.UI
{
    public class UIResource : MonoBehaviour
    {
        [SerializeField] private GameResources resourceType;
        [Space]
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI textQuantity;
        [SerializeField] private Image icon;

        private int _displayValue;

        private void Start()
        {
            button.onClick.AddListener(OnClickResource);
        }

        private void OnClickResource()
        {
            Debug.Log("Click Resource: " + resourceType);
        }

        public void UpdateQuantity(float delay = 0)
        {

        }
    }
}

