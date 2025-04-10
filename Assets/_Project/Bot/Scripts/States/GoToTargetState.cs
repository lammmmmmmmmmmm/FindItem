using _Global;
using Pathfinding;
using UnityEngine;

namespace Bot.States {
    public class GoToTargetState : IState {
        private readonly IAstarAI _ai;
        private readonly TargetFinder _targetFinder;
        private readonly Transform _target;
        
        private Transform _currentTarget;

        public GoToTargetState(IAstarAI ai, TargetFinder targetFinder) {
            _ai = ai;
            _targetFinder = targetFinder;
        }

        public GoToTargetState(IAstarAI ai, Transform target) {
            _ai = ai;
            _target = target;
        }   

        public void OnEnter() {
            _currentTarget = _targetFinder ? _targetFinder.Target : _target;
            if (!_currentTarget) return;
            
            _ai.destination = _currentTarget.position;
            _ai.SearchPath();
        }

        public void Tick() {
            // target might be destroyed before the bot reaches it, so we need to update the target
            _currentTarget = _targetFinder ? _targetFinder.Target : _target;
            if (!_currentTarget) return;
            
            _ai.destination = _currentTarget.position;
        }

        public void OnExit() {
        }
    }
}