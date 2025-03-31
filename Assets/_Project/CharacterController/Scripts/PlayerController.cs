using UnityEngine;
using UnityEngine.InputSystem;

namespace CharacterController {
    public class PlayerController : MonoBehaviour {
        [SerializeField] private float moveSpeed = 10f;
        
        private Rigidbody2D _rb;
        private PlayerInput _playerInput;
        private InputAction _moveAction;
        
        private void Awake() {
            _rb = GetComponent<Rigidbody2D>();
            _playerInput = GetComponent<PlayerInput>();
            _moveAction = _playerInput.actions["Move"];
        }

        private void Update() {
            Vector2 moveDirection = _moveAction.ReadValue<Vector2>();
            moveDirection *= moveSpeed;
            _rb.velocity = moveDirection;
        }
    }
}