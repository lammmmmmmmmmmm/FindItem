using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using TMPro;
using UnityEngine;
using _Global;


namespace Survivor.Enemy
{
    public class MonsterController : MonoBehaviour
    {
        private StateMachine _stateMachine;
        private ImposterDetector _detector;
        private AIPath _aiMovement;

        private float attackRange = 0.8f;

        private void Awake ()
        {
            _stateMachine = new StateMachine();
            _detector = GetComponentInChildren<ImposterDetector>();
            _aiMovement = GetComponent<AIPath>();

            var run = new MonsterRunState(this, _aiMovement);
            var follow = new MonsterFollowState(this, _aiMovement, _detector);
            var attack = new MonsterAttackState(this, _aiMovement, _detector);

            _stateMachine.AddAnyTransition(run, () => _detector.TargetImposter == null);

            AddTransition(run, follow, HasTarget());
            AddTransition(attack, follow, CanNotAttackTarget());
            AddTransition(follow, attack, CanAttackTarget());

            _stateMachine.SetState(run);

            void AddTransition(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

            Func<bool> HasTarget() => () => _detector.TargetImposter != null;
            Func<bool> NotHasTarget() => () => _detector.TargetImposter == null;
            Func<bool> CanAttackTarget() => () =>
            {
                if (_detector.TargetImposter == null)
                    return false;

                return _detector.TargetDistance() < attackRange;
            };

            Func<bool> CanNotAttackTarget() => () =>
            {
                if (_detector.TargetImposter == null)
                    return false;

                return _detector.TargetDistance() > attackRange;
            };
        }


        private void Update() => _stateMachine.Tick();


    }
}
