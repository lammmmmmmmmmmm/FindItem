using System.Collections;
using System.Collections.Generic;
using Survivor.Enemy;
using UnityEngine;
using UnityEngine.PlayerLoop;


namespace Survivor.Enemy
{
    public class MonsterRunState : IState
    {
        private readonly MonsterController _monsterController;

        private Transform _transform;
        private Vector2 _targetPosition;

        public MonsterRunState(MonsterController monsterController)
        {
            _monsterController = monsterController;
        }

        public void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public void Tick()
        {
            if (Vector2.Distance(_transform.position, _targetPosition) < 0.1f)
            {
                UpdateTarget();
            }
        }

        private void UpdateTarget()
        {
            
        }

        private void Run()
        {

        }
    }

}
