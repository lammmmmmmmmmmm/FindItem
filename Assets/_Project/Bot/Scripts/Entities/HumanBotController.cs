using _Global;
using _Global.Utils;
using Bot.States;
using Hiding;
using Item;
using Pathfinding;
using UnityEngine;

namespace Bot.Entities {
    public class HumanBotController : MonoBehaviour, IDie {
        [SerializeField] private GameObject destination;
        [SerializeField] private TargetFinder itemFinder;
        [SerializeField] private TargetFinder monsterFinder;
        [SerializeField] private HumanBotConfig config;

        private Hide _humanBotHide;

        private StateMachine _stateMachine;
        private IAstarAI _ai;
        private ItemCarrier _itemCarrier;

        private void Awake() {
            _ai = GetComponent<IAstarAI>();
            _itemCarrier = GetComponent<ItemCarrier>();
            _humanBotHide = GetComponent<HumanBotHide>();

            _stateMachine = new StateMachine();
        }

        private void Start() {
            _ai.maxSpeed = config.WanderingSpeed;
            itemFinder.SetRadius(config.ItemDetectionRange);
            monsterFinder.SetRadius(config.MonsterDetectionRange);

            var wanderState = new WanderingState(_ai);
            var goToTargetState = new GoToTargetState(_ai);

            _stateMachine.AddTransition(wanderState, goToTargetState, () => goToTargetState.HasTarget());
            _stateMachine.AddTransition(goToTargetState, wanderState,
                () => !goToTargetState.HasTarget() && !_itemCarrier.IsCarrying);

            _stateMachine.SetState(wanderState);

            _itemCarrier.OnItemPickedUp += () => { goToTargetState.SetTarget(destination); };

            itemFinder.OnTargetInRange += item => {
                if (_itemCarrier.IsCarrying) return;

                if (MathUtils.RandomChance(config.PickUpChance)) {
                    goToTargetState.SetTarget(item);
                }
            };

            //TODO: hiding is not working properly
            monsterFinder.OnNewTargetFound += _ => {
                if (MathUtils.RandomChance(config.HideChance)) {
                    _humanBotHide.ToggleHiding();
                }
            };

            monsterFinder.OnTargetLost += () => { _humanBotHide.ToggleHiding(); };
        }

        private void Update() {
            _stateMachine.Tick();
        }

        public void Die() {
            Debug.Log("Die");
            Destroy(gameObject);
        }
    }
}