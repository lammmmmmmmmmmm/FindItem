using DG.Tweening;
using UnityEngine;

namespace Item {
    public class CoinUpdater : MonoBehaviour {
        [SerializeField] private GameObject coinPrefab;
        
        public void AddCoin(int amount) {
            DataManager.Instance.PlayerData.resourcesData[GameResources.Coin] += amount;
            var coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            coin.transform.DOMove(transform.position + new Vector3(0, 1, 0), 1f)
                .OnComplete(() => {
                    Destroy(coin);
                });
        }
        
        public void AddCoin(Vector3 position) {
            int amount = 100;
            DataManager.Instance.PlayerData.resourcesData[GameResources.Coin] += amount;
            var coin = Instantiate(coinPrefab, position, Quaternion.identity);
            coin.transform.DOMove(position + new Vector3(0, 1, 0), 1f)
                .OnComplete(() => {
                    Destroy(coin);
                });
        }
    }
}