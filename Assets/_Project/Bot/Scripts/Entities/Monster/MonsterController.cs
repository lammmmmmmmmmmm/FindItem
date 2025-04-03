using System;
using _Global;
using HumanBot.States;
using Pathfinding;
using UnityEngine;

namespace Bot.Entities.Monster {
    public class MonsterController : MonoBehaviour {
        private StateMachine _stateMachine;
        private ImposterDetector _detector;
        private AIPath _aiMovement;

        private const float ATTACK_RANGE = 0.8f;

        private void Awake() {
            _stateMachine = new StateMachine();
            _detector = GetComponentInChildren<ImposterDetector>();
            _aiMovement = GetComponent<AIPath>();

            // var wanderState = new MonsterRunState(this, _aiMovement);
            var wanderState = new WanderingState(_aiMovement);
            var chaseState = new MonsterFollowState(_aiMovement, _detector);
            var attackState = new MonsterAttackState(_aiMovement, _detector);

            _stateMachine.AddAnyTransition(wanderState, NotHasTarget());

            AddTransition(wanderState, chaseState, HasTarget());
            AddTransition(attackState, chaseState, TargetOutOfRange());
            AddTransition(chaseState, attackState, CanAttackTarget());

            _stateMachine.SetState(wanderState);

            void AddTransition(IState to, IState from, Func<bool> condition) =>
                _stateMachine.AddTransition(to, from, condition);

            Func<bool> HasTarget() => () => _detector.TargetImposter != null;
            Func<bool> NotHasTarget() => () => _detector.TargetImposter == null;

            Func<bool> CanAttackTarget() => () => {
                if (_detector.TargetImposter == null)
                    return false;

                return _detector.TargetDistance() < ATTACK_RANGE;
            };

            Func<bool> TargetOutOfRange() => () => {
                if (_detector.TargetImposter == null)
                    return false;

                return _detector.TargetDistance() > ATTACK_RANGE;
            };
        }


        private void Update() => _stateMachine.Tick();
    }
}