using Pathfinding;
using UnityEngine;

namespace HumanBot.States {
    public class WanderingState : IState {
        private const float WANDERING_RADIUS = 20;
        private readonly IAstarAI _ai;
        
        public WanderingState(Entities.HumanBot humanBot) {
            _ai = humanBot.GetComponent<IAstarAI>();
        }

        public void OnEnter() {
            _ai.destination = PickRandomPoint();
            _ai.SearchPath();
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