using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;


namespace Survivor.Enemy
{
    public class MonsterController : MonoBehaviour
    {
        private StateMachine _stateMachine;
        private ImposterDetector _detector;
        private AIPath _aiMovement;

        private void Awake ()
        {
            _stateMachine = new StateMachine();
            _detector = GetComponentInChildren<ImposterDetector>();
            _aiMovement = GetComponent<AIPath>();

            var idle = new MonsterIdleState(this);
            var run = new MonsterRunState(this, _aiMovement);
            var attack = new MonsterAttackState(this, _detector, _aiMovement);

            _stateMachine.AddAnyTransition(attack, () => _detector.TargetImposter != null);

            AddTransition(idle, run, HasTarget());
            AddTransition(run, attack, HasTarget());
            AddTransition(attack, run, NotHasTarget());

            _stateMachine.SetState(run);

            void AddTransition(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

            Func<bool> HasTarget() => () =>  _detector.TargetImposter != null;
            Func<bool> NotHasTarget() => () => _detector.TargetImposter == null;
        }


        private void Update() => _stateMachine.Tick();


    }
}
