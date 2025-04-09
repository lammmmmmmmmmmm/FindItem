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

        private int _displayValue = 0;

        private void Start()
        {
            button.onClick.AddListener(OnClickResource);
            UpdateQuantity();
        }

        private void OnClickResource()
        {
            Debug.Log("Click Resource: " + resourceType);
        }

        public void UpdateQuantity(float delay = 0)
        {
            int curValue = DataManager.Instance.PlayerData.ResourcesData[resourceType];
            if (curValue != _displayValue)
            {
                _displayValue = curValue;
                textQuantity.text = curValue.ToString();
            }
        }
    }
}

