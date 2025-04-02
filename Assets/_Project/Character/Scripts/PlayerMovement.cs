using UnityEngine;
using UnityEngine.InputSystem;

namespace Character {
    public class PlayerMovement : MonoBehaviour {
        [SerializeField] private float defaultMoveSpeed = 10f;

        public float DefaultMoveSpeed => defaultMoveSpeed;
        public float CurrentMoveSpeed { get; set; }

        private Rigidbody2D _rb;
        private PlayerInput _playerInput;
        private InputAction _moveAction;
        
        private void Awake() {
            _rb = GetComponent<Rigidbody2D>();
            _playerInput = GetComponent<PlayerInput>();
            _moveAction = _playerInput.actions["Move"];
            
            CurrentMoveSpeed = defaultMoveSpeed;
        }

        private void Update() {
            Vector2 moveDirection = _moveAction.ReadValue<Vector2>();
            moveDirection *= CurrentMoveSpeed;
            _rb.velocity = moveDirection;
        }
    }
}