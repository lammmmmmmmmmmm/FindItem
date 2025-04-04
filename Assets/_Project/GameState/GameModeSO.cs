using System.Collections.Generic;
using Bot.Entities;
using Bot.Entities.Human;
using Bot.Entities.Monster;
using Map;
using UnityEngine;

namespace GameState {
    [CreateAssetMenu(menuName = "Game Mode SO")]
    public class GameModeSO : ScriptableObject {
        [SerializeField] private float playTime = 60f;
        
        [SerializeField] private int numberOfHumanBots = 5;
        [SerializeField] private int numberOfMonsterBots = 2;
        [SerializeField] private HumanBotConfig humanBotConfigSO;
        [SerializeField] private MonsterBotConfigSO monsterBotConfigSO;
        
        [SerializeField] private int numberOfItemsToSpawn = 15;
        
        [SerializeField] private List<MapSpawnArea> mapPrefabs;
        
        public float PlayTime => playTime;
        public int NumberOfHumanBots => numberOfHumanBots;
        public int NumberOfMonsterBots => numberOfMonsterBots;
        public HumanBotConfig HumanBotConfigSO => humanBotConfigSO;
        public MonsterBotConfigSO MonsterBotConfigSO => monsterBotConfigSO;
        public int NumberOfItemsToSpawn => numberOfItemsToSpawn;
        public List<MapSpawnArea> MapPrefabs => mapPrefabs;
    }
}