using _Global;
using Character;
using HumanBot.States;
using Item;
using Pathfinding;
using UnityEngine;

namespace HumanBot.Entities {
    public class HumanBot : MonoBehaviour {
        [SerializeField] private GameObject destination;
        [SerializeField] private TargetFinder itemFinder;
        [SerializeField] private TargetFinder monsterFinder;
        
        private Hide _humanBotHide;
        
        private StateMachine _stateMachine;
        private IAstarAI _ai;
        private ItemCarrier _itemCarrier;
        
        private void Awake() {
            _ai = GetComponent<IAstarAI>();
            _itemCarrier = GetComponent<ItemCarrier>();
            _humanBotHide = GetComponent<HumanBotHide>();
        }

        private void Start() {
            var wanderState = new WanderingState(_ai);
            var goToTargetState = new GoToTargetState(_ai);

            _stateMachine = new StateMachine();

            _stateMachine.AddTransition(wanderState, goToTargetState, () => itemFinder.Target);
            _stateMachine.AddTransition(goToTargetState, wanderState, () => !itemFinder.Target && !_itemCarrier.IsCarrying);
            
            _stateMachine.SetState(wanderState);
            
            _itemCarrier.OnItemPickedUp += () => {
                goToTargetState.SetTarget(destination);
            };
            
            itemFinder.OnTargetInRange += item => {
                if (_itemCarrier.IsCarrying) return;
                
                goToTargetState.SetTarget(item);
            };
            
            monsterFinder.OnNewTargetFound += _ => {
                _humanBotHide.ToggleHiding();
            };
            
            monsterFinder.OnTargetLost += () => {
                _humanBotHide.ToggleHiding();
            };
        }

        private void Update() {
            _stateMachine.Tick();
        }
    }
}