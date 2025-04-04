using System;
using _Global;
using _Global.Utils;
using Bot.States;
using DG.Tweening;
using Hiding;
using Item;
using Pathfinding;
using UnityEngine;

namespace Bot.Entities.Human {
    public class HumanBotController : MonoBehaviour, IDie {
        [SerializeField] private TargetFinder itemFinder;
        [SerializeField] private TargetFinder monsterFinder;
        
        private HumanBotConfig _config;
        private Hide _humanBotHide;

        private bool _shouldGoToItem;

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
            _ai.maxSpeed = _config.WanderingSpeed;
            itemFinder.SetRadius(_config.ItemDetectionRange);
            monsterFinder.SetRadius(_config.MonsterDetectionRange);

            var wanderState = new WanderingState(_ai);
            var goToItemState = new GoToTargetState(_ai, itemFinder);
            var goToGoalState = new GoToTargetState(_ai, ItemUnloader.Instance.transform);

            _stateMachine.AddAnyTransition(goToGoalState, () => _itemCarrier.IsCarrying);
            _stateMachine.AddAnyTransition(wanderState,
                () => (!_shouldGoToItem || !itemFinder.Target) && !_itemCarrier.IsCarrying);
            _stateMachine.AddAnyTransition(goToItemState, GoToItemCondition());

            _stateMachine.SetState(wanderState);

            itemFinder.OnTargetInRange += _ => {
                if (_shouldGoToItem) return;
                _shouldGoToItem = MathUtils.RandomChance(_config.PickUpChance);
            };
            itemFinder.OnTargetLost += () => { _shouldGoToItem = false; };
            _itemCarrier.OnItemPickedUp += () => { _shouldGoToItem = false; };

            monsterFinder.OnNewTargetFound += _ => {
                if (MathUtils.RandomChance(_config.HideChance)) {
                    _humanBotHide.StartHiding();
                }
            };

            monsterFinder.OnTargetLost += () => { _humanBotHide.StopHiding(); };

            return;

            Func<bool> GoToItemCondition() => () => itemFinder.Target && _shouldGoToItem && !_itemCarrier.IsCarrying;
        }

        private void Update() {
            _stateMachine.Tick();
        }

        public void Die(Vector3 position) {
            _ai.isStopped = true;
            transform.position = position;

            DOVirtual.DelayedCall(0.5f, () => { Destroy(gameObject); }).SetLink(gameObject);
        }

        public void SetConfig(HumanBotConfig config) {
            _config = config;
        }
        
        public void Stop() {
            _ai.isStopped = true;
            _ai.maxSpeed = 0;
        }
    }
}