using UnityEngine;
using UnityEngine.InputSystem;

namespace CharacterController {
    public class PlayerController : MonoBehaviour {
        [SerializeField] private float moveSpeed = 10f;
        private UnityEngine.CharacterController _characterController;
        
        private PlayerInput _playerInput;
        private InputAction _moveAction;
        
        private void Awake() {
            _characterController = GetComponent<UnityEngine.CharacterController>();
            _playerInput = GetComponent<PlayerInput>();
            _moveAction = _playerInput.actions["Move"];
        }

        private void Update() {
            _characterController.Move(_moveAction.ReadValue<Vector2>() * (moveSpeed * Time.deltaTime));
        }
    }
}