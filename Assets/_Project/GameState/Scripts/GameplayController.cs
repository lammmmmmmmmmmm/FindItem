using System;
using Bot.Entities;
using Character;
using Cinemachine;
using Cysharp.Threading.Tasks;
using FOW;
using Item;
using Map;
using Survivor.Gameplay;
using Survivor.UI;
using UnityEngine;

namespace GameState {
    public class GameplayController : MonoBehaviour {
        private GameMode _gameMode;
        private UIIngame _uiInGame;
        private PlayerRole _playerRole;
        private float waitingTime = 10;
        private BoosterController boosterController;

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
            boosterController = new BoosterController(this);
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
            WaitingStartGame().Forget();
        }

        private async UniTaskVoid WaitingStartGame()
        {
            mapSpawner.SpawnMap();
            _uiInGame.SetWaitingUI();
            countDownTimer.SetTimeToWait(gameModeSO.PlayTime);

            PanelData panelData = new PanelData();
            panelData.Add(PanelDataKey.PlayerRole, _playerRole);
            panelData.Add(PanelDataKey.BoosterController, boosterController);
            PanelManager.Instance.OpenPanel<PanelChooseBooster>(panelData);

            await UniTask.Delay((int)(waitingTime * 1000));
            PanelManager.Instance.ClosePanel<PanelChooseBooster>();
            StartGame();
        }

        private void StartGame()
        {
            countDownTimer.SetRunningTime(true);
        }

        public void OnFullItem() {
            if (_playerRole == PlayerRole.Imposter) {
                GameManager.Instance.OnWin();
            } else {
                GameManager.Instance.OnLose();
            }
        }

        #region Control Booster
        public void EnableLight(bool enabled = false)
        {
            FogOfWarWorld fow = FindObjectOfType<FogOfWarWorld>();
            if (!fow)
                return;

            fow.enabled = enabled;
        }

        public void AddTime(float time = 10f)
        {
            countDownTimer.AddTime(time);
        }

        public void BoostSpeed()
        {
            PlayerMovement playerMovement = (_playerRole == PlayerRole.Imposter) ? 
                humanPlayer.GetComponent<PlayerMovement>() :
                monsterPlayer.GetComponent<PlayerMovement>();

            playerMovement?.BoostSpeed();
        }

        public void MoreItem()
        {
            itemSpawner.SetNumberOfItemsToSpawn(20);
            itemSpawner.SpawnItems();
        }
        #endregion
    }
}