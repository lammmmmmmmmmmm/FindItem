using _Global;
using DG.Tweening;
using Pathfinding;
using UnityEngine;

namespace Bot.Entities.Monster {
    public class MonsterAttackState : IState {
        private readonly MonsterBotController _monsterBotController;
        private readonly TargetFinder _humanFinder;
        private readonly IAstarAI _aiMovement;
        private IDie _targetDie;

        public MonsterAttackState(MonsterBotController monsterBotController, IAstarAI aiMovement, TargetFinder humanFinder) {
            _monsterBotController = monsterBotController;
            _humanFinder = humanFinder;
            _aiMovement = aiMovement;
        }

        public void OnEnter() {
            _aiMovement.isStopped = true;
            _targetDie = _humanFinder.Target.GetComponent<IDie>();
            
            Attack();
        }

        public void OnExit() {
            _aiMovement.isStopped = false;
        }

        public void Tick() {
        }

        private void Attack() {
            _monsterBotController.transform.DOScale(Vector2.one * 2f, 0.1f).SetLoops(4, LoopType.Yoyo).OnComplete(() => {
                var dir = _humanFinder.Target.position - _monsterBotController.transform.position;
                _targetDie.Die(_monsterBotController.transform.position + dir.normalized);
            });
        }
    }
}