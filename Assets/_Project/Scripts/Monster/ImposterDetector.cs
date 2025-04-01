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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_targetImposter != null)
                return;

            if (other.TryGetComponent<IImposter>(out var imposter))
            {
                _targetImposter = imposter;
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

        private void RemoveCharacterTarget()
        {
            _targetImposter = null;
        }
    }
}
