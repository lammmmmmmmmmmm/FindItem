using System;
using Bot.Entities;
using Cinemachine;
using Item;
using Map;
using Survivor.Gameplay;
using Survivor.UI;
using UnityEngine;

namespace GameState {
    public class GameplayController : MonoBehaviour {
        private GameMode gameMode;
        private UIIngame uiIngame;
        private PlayerRole playerRole;

        [Header("Player")]
        [SerializeField] private GameObject humanPlayer;
        [SerializeField] private GameObject monsterPlayer;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        [Space]
        [Header("Map")]
        [SerializeField] private GameModeSO gameModeSO;
        [SerializeField] private CountDownTimer countDownTimer;
        [SerializeField] private BotSpawner botSpawner;
        [SerializeField] private ItemSpawner itemSpawner;
        [SerializeField] private MapSpawner mapSpawner;

        private void Awake()
        {
            uiIngame = FindObjectOfType<UIIngame>();
        }

        private void Start() {
            gameMode = GameManager.Instance.gameMode;

            countDownTimer.SetTimeToWait(gameModeSO.PlayTime);
            
            botSpawner.SetBotCount(gameModeSO.NumberOfHumanBots, gameModeSO.NumberOfMonsterBots);
            botSpawner.SetBotConfig(gameModeSO.HumanBotConfigSO, gameModeSO.MonsterBotConfigSO);
            
            itemSpawner.SetNumberOfItemsToSpawn(gameModeSO.NumberOfItemsToSpawn);
            
            mapSpawner.SetRandomMaps(gameModeSO.MapPrefabs);

            ShowChooseRole();
        }

        private void ShowChooseRole()
        {
            PanelData panelData = new PanelData();
            Action<PlayerRole> chooseRoleAction = (role) => SetPlayerRole(role);

            panelData.Add("ChooseRoleAction", chooseRoleAction);

            PanelManager.Instance.OpenPanel<PanelChooseRole>(panelData);
        }

        private void SetPlayerRole(PlayerRole playerRole)
        {
            this.playerRole = playerRole;
            Debug.Log("Update Role");
            if (playerRole == PlayerRole.Imposter)
            {
                humanPlayer.SetActive(true);
                monsterPlayer.SetActive(false);
                virtualCamera.Follow = humanPlayer.transform;
                botSpawner.SetAllHumanDieEvent(null);
            }
            else
            {
                humanPlayer.SetActive(false);
                monsterPlayer.SetActive(true);
                virtualCamera.Follow = monsterPlayer.transform;
                botSpawner.SetAllHumanDieEvent(() => GameManager.Instance.OnWin());
            }

            uiIngame.SetRoleUI(playerRole);
            SpawnMap();
        }

        private void SpawnMap()
        {
            mapSpawner.SpawnMap();
        }

        public void OnFullItem()
        {
            if (playerRole == PlayerRole.Imposter)
            {
                GameManager.Instance.OnWin();
            }
            else
            {
                GameManager.Instance.OnLose();
            }
        }
    }
}