using _Global;
using Pathfinding;
using UnityEngine;

namespace HumanBot.States {
    public class GoToTargetState : IState {
        private GameObject _currentTarget;
        private readonly IAstarAI _ai;
        
        public GoToTargetState(IAstarAI ai) {
            _ai = ai;
        }
        
        public void OnEnter() {
        }

        public void Tick() {
        }

        public void OnExit() {
        }
        
        public void SetTarget(GameObject target) {
            if (!target || target == _currentTarget) return;
            
            _currentTarget = target;
            _ai.destination = _currentTarget.transform.position;
            _ai.SearchPath();
        }
    }
}