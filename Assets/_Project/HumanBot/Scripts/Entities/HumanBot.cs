using System;
using _Global;
using HumanBot.States;
using Item;
using Pathfinding;
using UnityEngine;

namespace HumanBot.Entities {
    public class HumanBot : MonoBehaviour, IImposter
    {
        [SerializeField] private GameObject destination;
        [SerializeField] private TargetFinder itemFinder;
        [SerializeField] private TargetFinder monsterFinder;
        
        private StateMachine _stateMachine;
        private IAstarAI _ai;
        private ItemCarrier _itemCarrier;
        
        private void Awake() {
            _ai = GetComponent<IAstarAI>();
            _itemCarrier = GetComponent<ItemCarrier>();
        }

        private void Start() {
            var wanderState = new WanderingState(_ai);
            var goToTargetState = new GoToTargetState(_ai);
            var hideState = new HideState();

            _stateMachine = new StateMachine();

            _stateMachine.AddTransition(wanderState, goToTargetState, ItemFound());
            _stateMachine.AddTransition(goToTargetState, wanderState, () => !itemFinder.Target && !_itemCarrier.IsCarrying);
            // _stateMachine.AddAnyTransition(hideState, MonsterFound());
            
            _stateMachine.SetState(wanderState);
            
            _itemCarrier.OnItemPickedUp += () => {
                goToTargetState.SetTarget(destination);
            };
            
            itemFinder.OnTargetFound += item => {
                if (_itemCarrier.IsCarrying) return;
                
                goToTargetState.SetTarget(item);
            };
            
            return;

            Func<bool> ItemFound() => () => itemFinder.Target;
            // Func<bool> MonsterFound() => () => monsterFinder.Target;
        }

        private void Update() {
            _stateMachine.Tick();
        }

        public void Die()
        {
            Debug.Log("Die");
            Destroy(gameObject);
        }
    }
}