using _Global;
using Pathfinding;
using UnityEngine;

namespace Bot.States {
    public class WanderingState : IState {
        private const float WANDERING_RADIUS = 20;
        private readonly IAstarAI _ai;
        
        public WanderingState(IAstarAI ai) {
            _ai = ai;
        }

        public void OnEnter() {
            _ai.destination = PickRandomPoint();
            _ai.SearchPath();
            
            Debug.Log("Change to Wandering");
        }

        public void Tick() {
            if (!_ai.pathPending && (_ai.reachedEndOfPath || !_ai.hasPath)) {
                _ai.destination = PickRandomPoint();
                _ai.SearchPath();
            }
        }

        public void OnExit() {
        }
        
        private Vector3 PickRandomPoint () {
            var point = Random.insideUnitSphere * WANDERING_RADIUS;

            point.z = 0;
            point += _ai.position;
            return point;
        }
    }
}