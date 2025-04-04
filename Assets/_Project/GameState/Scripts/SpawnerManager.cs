using Bot.Entities;
using Item;
using Map;
using UnityEngine;

namespace GameState {
    public class SpawnerManager : MonoBehaviour {
        [SerializeField] private GameModeSO gameModeSO;
        
        [SerializeField] private CountDownTimer countDownTimer;
        [SerializeField] private BotSpawner botSpawner;
        [SerializeField] private ItemSpawner itemSpawner;
        [SerializeField] private MapSpawner mapSpawner;
        
        private void Start() {
            countDownTimer.SetTimeToWait(gameModeSO.PlayTime);
            
            botSpawner.SetBotCount(gameModeSO.NumberOfHumanBots, gameModeSO.NumberOfMonsterBots);
            botSpawner.SetBotConfig(gameModeSO.HumanBotConfigSO, gameModeSO.MonsterBotConfigSO);
            
            itemSpawner.SetNumberOfItemsToSpawn(gameModeSO.NumberOfItemsToSpawn);
            
            mapSpawner.SetRandomMaps(gameModeSO.MapPrefabs);
        }
    }
}