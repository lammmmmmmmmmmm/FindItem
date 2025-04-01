using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Survivor.Enemy
{
    public class MonsterController : MonoBehaviour
    {
        private StateMachine _stateMachine;
        private ImposterDetector _detector;
        

        private void Awake ()
        {
            _stateMachine = new StateMachine();
            _detector = GetComponentInChildren<ImposterDetector>();

            var idle = new MonsterIdleState(this);
            var run = new MonsterRunState(this);
            var attack = new MonsterAttackState(this);

            _stateMachine.AddAnyTransition(attack, () => _detector.PlayerTarget());

            AddTransition(idle, run, HasTarget());
            AddTransition(run, attack, HasTarget());
            AddTransition(attack, run, NotHasTarget());

            _stateMachine.SetState(idle);

            void AddTransition(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

            Func<bool> HasTarget() => () =>  _detector.PlayerTarget() != null;
            Func<bool> NotHasTarget() => () => _detector.PlayerTarget() == null;
        }


        private void Update() => _stateMachine.Tick();


    }
}
