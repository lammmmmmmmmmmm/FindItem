using DG.Tweening;
using UnityEngine;

namespace Item {
    public class CoinSessionTracker : MonoBehaviour {
        [SerializeField] private GameObject coinPrefab;
        
        public int CurrentSessionCoinCount { get; private set; }
        
        public void AddCoin(int amount) {
            DataManager.Instance.PlayerData.ResourcesData[GameResources.Coin] += amount;
            DataManager.Instance.GameSessionData.totalCoinCollected += amount;
            
            var coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            coin.transform.DOMove(transform.position + new Vector3(0, 1, 0), 1f)
                .OnComplete(() => {
                    Destroy(coin);
                });
        }
        
        public void AddCoin(Vector3 position) {
            int amount = 100;
            DataManager.Instance.PlayerData.ResourcesData[GameResources.Coin] += amount;
            DataManager.Instance.GameSessionData.totalCoinCollected += amount;
            
            var coin = Instantiate(coinPrefab, position, Quaternion.identity);
            coin.transform.DOMove(position + new Vector3(0, 1, 0), 1f)
                .OnComplete(() => {
                    Destroy(coin);
                });
        }
    }
}