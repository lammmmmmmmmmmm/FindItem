using _Global;
using Pathfinding;
using UnityEngine;

namespace Bot.States {
    public class GoToTargetState : IState {
        private GameObject _currentTarget;
        private readonly IAstarAI _ai;

        public GoToTargetState(IAstarAI ai) {
            _ai = ai;
        }

        public void OnEnter() {
            _ai.destination = _currentTarget.transform.position;
            _ai.SearchPath();
        }

        public void Tick() {
            _ai.destination = _currentTarget.transform.position;

            if (_ai.reachedDestination) {
                _currentTarget = null;
            }
        }

        public void OnExit() {
            _currentTarget = null;
        }

        public bool HasTarget() {
            return _currentTarget;
        }

        public void SetTarget(GameObject target) {
            _currentTarget = target;
        }
    }
}