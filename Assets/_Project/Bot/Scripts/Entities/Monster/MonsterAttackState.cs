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
        private Transform _chosenTarget;

        public MonsterAttackState(MonsterBotController monsterBotController, IAstarAI aiMovement, TargetFinder humanFinder) {
            _monsterBotController = monsterBotController;
            _humanFinder = humanFinder;
            _aiMovement = aiMovement;
        }

        public void OnEnter() {
            _aiMovement.isStopped = true;
            _targetDie = _humanFinder.Target.GetComponent<IDie>();
            _chosenTarget = _humanFinder.Target.transform;
            
            Attack();
        }

        public void OnExit() {
            _aiMovement.isStopped = false;
        }

        public void Tick() {
        }

        private void Attack() {
            _monsterBotController.transform.DOScale(Vector2.one * 2f, 0.1f).SetLoops(10, LoopType.Yoyo).OnComplete(() => {
                // target could be killed by other monster before the animation is done
                if (!_chosenTarget) return;
                
                var dir = _chosenTarget.position - _monsterBotController.transform.position;
                _targetDie.Die(_monsterBotController.transform.position + dir.normalized);
            });
        }
    }
}