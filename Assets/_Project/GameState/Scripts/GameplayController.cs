using System;
using Bot.Entities;
using Cinemachine;
using Item;
using Map;
using Survivor.Gameplay;
using Survivor.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameState {
    public class GameplayController : MonoBehaviour {
        private GameMode _gameMode;
        private UIIngame _uiInGame;
        private PlayerRole _playerRole;

        [Header("Player")]
        [SerializeField] private GameObject humanPlayer;
        [SerializeField] private GameObject monsterPlayer;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        [Space]
        [Header("Map")]
        [SerializeField] private GameModeSO gameModeSO;
        [SerializeField] private CountDownTimer countDownTimer;
        [SerializeField] private BotManager botManager;
        [SerializeField] private ItemSpawner itemSpawner;
        [SerializeField] private MapSpawner mapSpawner;

        private void Awake() {
            _uiInGame = FindObjectOfType<UIIngame>();
        }

        private void Start() {
            _gameMode = GameManager.Instance.gameMode;

            botManager.SetBotCount(gameModeSO.NumberOfHumanBots, gameModeSO.NumberOfMonsterBots);
            botManager.SetBotConfig(gameModeSO.HumanBotConfigSO, gameModeSO.MonsterBotConfigSO);

            itemSpawner.SetNumberOfItemsToSpawn(gameModeSO.NumberOfItemsToSpawn);

            mapSpawner.SetRandomMaps(gameModeSO.MapPrefabs);

            ShowChooseRole();
        }

        private void ShowChooseRole() {
            PanelData panelData = new PanelData();
            Action<PlayerRole> chooseRoleAction = SetPlayerRole;

            panelData.Add("ChooseRoleAction", chooseRoleAction);

            PanelManager.Instance.OpenPanel<PanelChooseRole>(panelData);
        }

        private void SetPlayerRole(PlayerRole playerRole) {
            _playerRole = playerRole;
            Debug.Log("Update Role: " + playerRole);
            if (playerRole == PlayerRole.Imposter) {
                humanPlayer.SetActive(true);
                monsterPlayer.SetActive(false);
                virtualCamera.Follow = humanPlayer.transform;
                botManager.SetAllHumanDieEvent(() => GameManager.Instance.OnLose());
            } else {
                humanPlayer.SetActive(false);
                monsterPlayer.SetActive(true);
                virtualCamera.Follow = monsterPlayer.transform;
                botManager.SetAllHumanDieEvent(() => GameManager.Instance.OnWin());
            }

            _uiInGame.SetRoleUI(playerRole);
            StartGame();
        }

        private void StartGame() {
            mapSpawner.SpawnMap();
            countDownTimer.SetTimeToWait(gameModeSO.PlayTime);
        }

        public void OnFullItem() {
            if (_playerRole == PlayerRole.Imposter) {
                GameManager.Instance.OnWin();
            } else {
                GameManager.Instance.OnLose();
            }
        }
    }
}