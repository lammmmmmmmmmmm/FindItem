using _Global;
using Pathfinding;
using UnityEngine;

namespace Bot.Entities.Monster
{
    public class MonsterRunState : IState
    {
        private readonly MonsterController _monsterController;
        private readonly IAstarAI _aiMovement;

        private Transform _transform;
        private Vector2 _targetPosition;

        private Vector2 _minPosition = new Vector2(-5, -5);
        private Vector2 _maxPosition = new Vector2(5, 5);

        public MonsterRunState(MonsterController monsterController, IAstarAI aiMovement)
        {
            _monsterController = monsterController;
            _aiMovement = aiMovement;
        }

        public void OnEnter()
        {
            _aiMovement.canMove = true;
            //_aiMovement.destination = UpdateTarget();
            //_aiMovement.SearchPath();
        }

        public void Tick()
        {
            if (!_aiMovement.pathPending && (_aiMovement.reachedEndOfPath || !_aiMovement.hasPath))
            {
                _aiMovement.destination = UpdateTarget();
                _aiMovement.SearchPath();
            }
        }

        public void OnExit()
        {
            Debug.Log($"Exit state: Run");
        }

        private Vector2 UpdateTarget()
        {
            MathUtils.Random(ref _targetPosition, _minPosition, _maxPosition);
            return _targetPosition;
        }

        private void Run()
        {

        }
    }

}
