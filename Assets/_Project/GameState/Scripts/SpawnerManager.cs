using Bot.Entities;
using Item;
using Map;
using Survivor.Gameplay;
using UnityEngine;

namespace GameState {
    public class SpawnerManager : MonoBehaviour {
        private GameMode gameMode;

        [SerializeField] private GameModeSO gameModeSO;
        
        [SerializeField] private CountDownTimer countDownTimer;
        [SerializeField] private BotSpawner botSpawner;
        [SerializeField] private ItemSpawner itemSpawner;
        [SerializeField] private MapSpawner mapSpawner;
        
        private void Start() {
            gameMode = GameManager.Instance.gameMode;

            countDownTimer.SetTimeToWait(gameModeSO.PlayTime);
            
            botSpawner.SetBotCount(gameModeSO.NumberOfHumanBots, gameModeSO.NumberOfMonsterBots);
            botSpawner.SetBotConfig(gameModeSO.HumanBotConfigSO, gameModeSO.MonsterBotConfigSO);
            
            itemSpawner.SetNumberOfItemsToSpawn(gameModeSO.NumberOfItemsToSpawn);
            
            mapSpawner.SetRandomMaps(gameModeSO.MapPrefabs);
        }
    }
}