using System.Collections.Generic;
using Bot.Entities.Human;
using Bot.Entities.Monster;
using Map;
using UnityEngine;

namespace Bot.Entities {
    public class BotSpawner : MonoBehaviour {
        [SerializeField] private int numberOfHumanBots;
        [SerializeField] private int numberOfMonsterBots;
        [SerializeField] private HumanBotConfig humanBotConfigSO;
        [SerializeField] private MonsterBotConfigSO monsterBotConfigSO;
        [SerializeField] private HumanBotController humanBotPrefab;
        [SerializeField] private MonsterBotController monsterBotPrefab;
        
        private readonly List<HumanBotController> _humanBots = new();
        private readonly List<MonsterBotController> _monsterBots = new();
        
        private Collider2D _humanSpawnArea;
        private Collider2D _monsterSpawnArea;
        
        public void SpawnHumanBots() {
            for (int i = 0; i < numberOfHumanBots; i++) {
                Vector3 spawnPosition = GetRandomSpawnPosition(_humanSpawnArea);
                var humanBot = Instantiate(humanBotPrefab, spawnPosition, Quaternion.identity);
                humanBot.SetConfig(humanBotConfigSO);
                _humanBots.Add(humanBot);
            }
        }
        
        public void SpawnMonsterBots() {
            for (int i = 0; i < numberOfMonsterBots; i++) {
                Vector3 spawnPosition = GetRandomSpawnPosition(_monsterSpawnArea);
                var monsterBot = Instantiate(monsterBotPrefab, spawnPosition, Quaternion.identity);
                monsterBot.SetConfig(monsterBotConfigSO);
                _monsterBots.Add(monsterBot);
            }
        }
        
        private Vector3 GetRandomSpawnPosition(Collider2D spawnArea) {
            Vector2 randomPoint = Random.insideUnitCircle * spawnArea.bounds.extents.x;
            Vector3 spawnPosition = new Vector3(randomPoint.x, randomPoint.y, 0);
            spawnPosition += spawnArea.transform.position;

            // Check if the position is within the spawn area
            if (spawnArea.OverlapPoint(spawnPosition)) {
                return spawnPosition;
            }

            return GetRandomSpawnPosition(spawnArea); // Retry if not valid
        }
        
        public void StopAllBots() {
            foreach (var humanBot in _humanBots) {
                humanBot.Stop();
            }
            
            foreach (var monsterBot in _monsterBots) {
                monsterBot.Stop();
            }
        }
        
        public void SetSpawnArea(MapSpawnArea area) {
            _humanSpawnArea = area.humanSpawnAreaCollider;
            _monsterSpawnArea = area.monsterSpawnAreaCollider;
        }
        
        public void SetBotCount(int humanBots, int monsterBots) {
            numberOfHumanBots = humanBots;
            numberOfMonsterBots = monsterBots;
        }
        
        public void SetBotConfig(HumanBotConfig humanBotConfig, MonsterBotConfigSO monsterBotConfig) {
            humanBotConfigSO = humanBotConfig;
            monsterBotConfigSO = monsterBotConfig;
        }
    }
}