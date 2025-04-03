using UnityEngine;

namespace Bot.Entities.Monster {
    public class ImposterDetector : MonoBehaviour {
        private IImposter _targetImposter;
        public IImposter TargetImposter => _targetImposter;
        public Transform TargetImposterTransform { get; private set; }

        private void OnTriggerEnter2D(Collider2D other) {
            if (_targetImposter != null)
                return;

            if (other.TryGetComponent<IImposter>(out var imposter)) {
                _targetImposter = imposter;
                TargetImposterTransform = other.transform;
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (_targetImposter == null)
                return;

            if (other.GetComponent<IImposter>() == _targetImposter) {
                RemoveCharacterTarget();
            }
        }

        public float TargetDistance() {
            return Vector2.Distance(transform.position, TargetImposterTransform.position);
        }

        private void RemoveCharacterTarget() {
            _targetImposter = null;
            TargetImposterTransform = null;
        }
    }
}