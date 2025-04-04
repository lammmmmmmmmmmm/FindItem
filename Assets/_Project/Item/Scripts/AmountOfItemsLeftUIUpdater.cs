using TMPro;
using UnityEngine;

namespace Item {
    public class AmountOfItemsLeftUIUpdater : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI amountOfItemsLeftText;
        private int _totalItems;
        
        public void SetTotalItems() {
            _totalItems = ItemUnloader.Instance.TotalItems;
            UpdateText(0);
        }

        public void UpdateText(int currentNumberOfItems) {
            amountOfItemsLeftText.text = "Find: \n" + currentNumberOfItems + "/" + _totalItems;
        }
    }
}