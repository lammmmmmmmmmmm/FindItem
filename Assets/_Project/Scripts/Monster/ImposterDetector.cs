using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

namespace Survivor.Enemy
{
    public class ImposterDetector : MonoBehaviour
    {
        private IImposter _targetImposter;
        public IImposter TargetImposter => _targetImposter;
        public Transform TargetImposterTf {  get; private set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_targetImposter != null)
                return;

            if (other.TryGetComponent<IImposter>(out var imposter))
            {
                _targetImposter = imposter;
                TargetImposterTf = other.transform;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_targetImposter == null)
                return;

            if (other.GetComponent<IImposter>() == _targetImposter)
            {
                RemoveCharacterTarget();
            }
        }

        public float TargetDistance()
        {
            return Vector2.Distance(transform.position, TargetImposterTf.position);
        }

        private void RemoveCharacterTarget()
        {
            _targetImposter = null;
            TargetImposterTf = null;
        }
    }
}
