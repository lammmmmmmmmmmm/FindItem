using Bot.Entities;
using Pathfinding;
using UnityEngine;

namespace Hiding {
    public class HumanBotHide : Hide {
        [SerializeField] private HumanBotConfig config;
        
        private IAstarAI _ai;
        
        private void Awake() {
            _ai = GetComponent<IAstarAI>();
        }
        
        protected override void DisableMovement() {
            if (_ai != null) {
                _ai.isStopped = true;
            }
        }

        protected override void EnableMovement(float speedMultiplier) {
            if (_ai != null) {
                _ai.isStopped = false;
                _ai.maxSpeed = config.WanderingSpeed * speedMultiplier;
            }
        }
    }
}