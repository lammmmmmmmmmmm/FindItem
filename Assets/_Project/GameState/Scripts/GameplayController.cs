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
        private GameMode gameMode;
        private UIIngame uiIngame;
        private PlayerRole playerRole;
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
        [SerializeField] private BotSpawner botSpawner;
        [SerializeField] private ItemSpawner itemSpawner;
        [SerializeField] private MapSpawner mapSpawner;

        private void Awake()
        {
            uiIngame = FindObjectOfType<UIIngame>();
            boosterController = new BoosterController(this);
        }

        private void Start() {
            gameMode = GameManager.Instance.gameMode;

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
            WaitingStartGame().Forget();
        }

        private async UniTaskVoid WaitingStartGame()
        {
            mapSpawner.SpawnMap();
            uiIngame.SetWaitingUI();
            countDownTimer.SetTimeToWait(gameModeSO.PlayTime);

            PanelData panelData = new PanelData();
            panelData.Add(PanelDataKey.PlayerRole, playerRole);
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
            PlayerMovement playerMovement = (playerRole == PlayerRole.Imposter) ? 
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