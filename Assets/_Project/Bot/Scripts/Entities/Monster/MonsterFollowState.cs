using _Global;
using Pathfinding;
using UnityEngine;

namespace Bot.Entities.Monster {
    public class MonsterFollowState : IState {
        private readonly ImposterDetector _detector;
        private readonly IAstarAI _aiMovement;

        private Transform _transform;
        private Transform _imposterTf;
        private Vector2 _targetPosition;

        public MonsterFollowState(IAstarAI aiMovement, ImposterDetector detector) {
            _detector = detector;
            _aiMovement = aiMovement;
        }

        public void OnEnter() {
            _aiMovement.canMove = true;
            _imposterTf = _detector.TargetImposterTransform;
            Debug.Log($"Enter state: Follow");
        }

        public void Tick() {
            _aiMovement.destination = _imposterTf.position;
            _aiMovement.SearchPath();
        }

        public void OnExit() {
            Debug.Log($"Exit state: Follow");
        }
    }
}