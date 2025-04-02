using Pathfinding;

namespace Character {
    public class HumanBotHide : Hide {
        private IAstarAI _ai;
        private float _defaultSpeed;
        
        private void Awake() {
            _ai = GetComponent<IAstarAI>();
            if (_ai != null) {
                _defaultSpeed = _ai.maxSpeed;
            }
        }
        
        protected override void DisableMovement() {
            if (_ai != null) {
                _ai.isStopped = true;
            }
        }

        protected override void EnableMovement(float speedMultiplier) {
            if (_ai != null) {
                _ai.isStopped = false;
                _ai.maxSpeed = _defaultSpeed * speedMultiplier;
            }
        }
    }
}