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
            Debug.Log("Change to Attack");
        }

        public void OnExit() {
            Debug.Log($"Exit state: Attack");
            _aiMovement.isStopped = false;
        }

        public void Tick() {
        }

        private void Attack() {
            //TODO: Anim Attack
            _monsterBotController.transform.DOScale(Vector2.one * 1.1f, 0.05f).SetLoops(4, LoopType.Yoyo);
            _targetDie.Die();
        }
    }
}