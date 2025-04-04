using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Character {
    public class BotHumanNameUIUpdater : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI botNameText;
        private readonly List<string> _nameList = new() { "Alice", "Bob", "Charlie", "David", "Emma" };

        private void Start() {
            SetName();
        }

        private void SetName() {
            var randomIndex = Random.Range(0, _nameList.Count);
            if (randomIndex >= 0 && randomIndex < _nameList.Count) {
                botNameText.text = _nameList[randomIndex];
            } else {
                Debug.LogError("Random index is out of range: " + randomIndex);
            }
        }
    }
}