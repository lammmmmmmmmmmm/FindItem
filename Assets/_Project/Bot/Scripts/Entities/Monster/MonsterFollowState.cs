// using _Global;
// using Pathfinding;
// using UnityEngine;
//
// namespace Bot.Entities.Monster {
//     public class MonsterFollowState : IState {
//         private readonly TargetDetectorWithTrigger _detectorWithTrigger;
//         private readonly IAstarAI _aiMovement;
//
//         private Transform _transform;
//         private Transform _imposterTransform;
//         private Vector2 _targetPosition;
//
//         public MonsterFollowState(IAstarAI aiMovement, TargetDetectorWithTrigger detectorWithTrigger) {
//             _detectorWithTrigger = detectorWithTrigger;
//             _aiMovement = aiMovement;
//         }
//
//         public void OnEnter() {
//             _aiMovement.isStopped = false;
//             _imposterTransform = _detectorWithTrigger.TargetTransform;
//             Debug.Log($"Enter state: Follow");
//         }
//
//         public void Tick() {
//             _aiMovement.destination = _imposterTransform.position;
//             _aiMovement.SearchPath();
//         }
//
//         public void OnExit() {
//             Debug.Log("Exit state: Follow");
//         }
//     }
// }