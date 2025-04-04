using System;
using System.Collections.Generic;
using _Global.EventChannels.ScriptableObjects;
using Bot.Entities.Human;
using Bot.Entities.Monster;
using Map;
using UnityEngine;

namespace Bot.Entities {
    public class BotSpawner : MonoBehaviour {
        [SerializeField] private GameObject humanPlayer;
        [SerializeField] private GameObject monsterPlayer;
        [SerializeField] private int numberOfHumanBots;
        [SerializeField] private int numberOfMonsterBots;
        [SerializeField] private HumanBotConfig humanBotConfigSO;
        [SerializeField] private MonsterBotConfigSO monsterBotConfigSO;
        [SerializeField] private HumanBotController humanBotPrefab;
        [SerializeField] private MonsterBotController monsterBotPrefab;

        [SerializeField] private VoidEventChannelSO OnAllHumanDie;
        
        private readonly List<HumanBotController> _humanBots = new();
        private readonly List<MonsterBotController> _monsterBots = new();
        
        private Collider2D _humanSpawnArea;
        private Collider2D _monsterSpawnArea;
        
        public void SpawnHumanBots() {
            for (int i = 0; i < numberOfHumanBots; i++) {
                Vector3 spawnPosition = GetRandomSpawnPosition(_humanSpawnArea);
                var humanBot = Instantiate(humanBotPrefab, spawnPosition, Quaternion.identity);
                humanBot.SetConfig(humanBotConfigSO);
                humanBot.Add(this);
                _humanBots.Add(humanBot);
            }
            
            humanPlayer.transform.position = GetRandomSpawnPosition(_humanSpawnArea);
        }
        
        public void SpawnMonsterBots() {
            for (int i = 0; i < numberOfMonsterBots; i++) {
                Vector3 spawnPosition = GetRandomSpawnPosition(_monsterSpawnArea);
                var monsterBot = Instantiate(monsterBotPrefab, spawnPosition, Quaternion.identity);
                monsterBot.SetConfig(monsterBotConfigSO);
                _monsterBots.Add(monsterBot);
            }
            
            monsterPlayer.transform.position = GetRandomSpawnPosition(_monsterSpawnArea);
        }
        
        private Vector3 GetRandomSpawnPosition(Collider2D spawnArea) {
            Vector2 randomPoint = UnityEngine.Random.insideUnitCircle * spawnArea.bounds.extents.x;
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
        
        public void SetSpawnArea(MapData area) {
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

        public void RemoveBot(HumanBotController bot)
        {
            Debug.Log("Human Bot " + _humanBots.Count);
            _humanBots.Remove(bot);
            if (_humanBots.Count == 0)
            {
                OnAllHumanDie.RaiseEvent();
            }
        }

        public void SetAllHumanDieEvent(Action callback = null)
        {
            OnAllHumanDie.OnEventRaised += () =>
            {
                callback?.Invoke();
            };
        }
    }
}