using System;
using _Global;
using _Global.Utils;
using Bot.States;
using DG.Tweening;
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

        private bool _goToItem;
        
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
            var goToItemState = new GoToTargetState(_ai, itemFinder);
            var goToGoalState = new GoToTargetState(_ai, destination.transform);

            _stateMachine.AddTransition(goToItemState, goToGoalState, () => _itemCarrier.IsCarrying);
            
            _stateMachine.AddAnyTransition(wanderState, () => !_goToItem || !itemFinder.Target && !_itemCarrier.IsCarrying);
            _stateMachine.AddAnyTransition(goToItemState, ShouldGoToItem());

            _stateMachine.SetState(wanderState);

            itemFinder.OnTargetInRange += _ => {
                _goToItem = MathUtils.RandomChance(config.PickUpChance);
            };
            _itemCarrier.OnItemPickedUp += () => {
                _goToItem = false;
            };

            //TODO: hiding is not working properly
            // monsterFinder.OnNewTargetFound += _ => {
            //     if (MathUtils.RandomChance(config.HideChance)) {
            //         _humanBotHide.ToggleHiding();
            //     }
            // };
            //
            // monsterFinder.OnTargetLost += () => { _humanBotHide.ToggleHiding(); };
            
            return;
            
            Func<bool> ShouldGoToItem() => () => itemFinder.Target && _goToItem && !_itemCarrier.IsCarrying;
        }

        private void Update() {
            _stateMachine.Tick();
        }

        public void Die(Vector3 position) {
            _ai.isStopped = true;
            transform.position = position;
            
            DOVirtual.DelayedCall(0.5f, () => { Destroy(gameObject); });
        }
    }
}