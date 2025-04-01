using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

namespace Survivor.Enemy
{
    public class ImposterDetector : MonoBehaviour
    {
        //private Imposter _imposterDetected;
        private PlayerMovement _playerTarget;

        public PlayerMovement PlayerTarget()
        {
            Debug.Log("Check");
            return _playerTarget;

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerMovement>(out var character))
            {
                _playerTarget = character;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_playerTarget == null)
                return;

            if (other.GetComponent<PlayerMovement>() == _playerTarget)
            {
                RemoveCharacterTarget();
            }
        }

        private void RemoveCharacterTarget()
        {
            _playerTarget = null;
        }
    }
}
