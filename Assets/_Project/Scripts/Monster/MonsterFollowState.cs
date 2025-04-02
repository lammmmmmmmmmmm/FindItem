using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Survivor.Enemy;
using UnityEngine;
using _Global;


namespace Survivor.Enemy
{
    public class MonsterFollowState : IState
    {
        private readonly MonsterController _monsterController;
        private readonly ImposterDetector _detector;
        private readonly IAstarAI _aiMovement;

        private Transform _transform;
        private IImposter _targetImposter;
        private Transform _imposterTf;
        private Vector2 _targetPosition;

        private Vector2 _minPosition = new Vector2(-5, -5);
        private Vector2 _maxPosition = new Vector2(5, 5);

        public MonsterFollowState(MonsterController monsterController, IAstarAI aiMovement, ImposterDetector detector)
        {
            _monsterController = monsterController;
            _detector = detector;
            _aiMovement = aiMovement;
        }

        public void OnEnter()
        {
            _aiMovement.canMove = true;
            _targetImposter = _detector.TargetImposter;
            _imposterTf = _detector.TargetImposterTf;
            Debug.Log($"Enter state: Follow");
        }

        public void Tick()
        {
            _aiMovement.destination = _imposterTf.position;
            _aiMovement.SearchPath();
        }

        public void OnExit()
        {
            Debug.Log($"Exit state: Follow");
        }
    }
}
