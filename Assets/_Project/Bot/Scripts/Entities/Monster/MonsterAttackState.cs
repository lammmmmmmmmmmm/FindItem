using _Global;
using DG.Tweening;
using Pathfinding;
using UnityEngine;

namespace Bot.Entities.Monster {
    public class MonsterAttackState : IState {
        private readonly MonsterController _monsterController;
        private readonly ImposterDetector _detector;
        private readonly IAstarAI _aiMovement;
        private IImposter _targetImposter;

        public MonsterAttackState(IAstarAI aiMovement, ImposterDetector detector) {
            _detector = detector;
            _aiMovement = aiMovement;
        }

        public void OnEnter() {
            _aiMovement.canMove = false;
            _targetImposter = _detector.TargetImposter;
            
            Attack();
            Debug.Log("Change to Attack");
        }

        public void OnExit() {
            Debug.Log($"Exit state: Attack");
        }

        public void Tick() {
        }

        private void Attack() {
            _monsterController.transform.DOScale(Vector2.one * 1.1f, 0.05f).SetLoops(4, LoopType.Yoyo);
            _targetImposter.Die();
        }
    }
}