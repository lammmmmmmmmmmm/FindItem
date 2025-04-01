using UnityEngine;
using UnityEngine.InputSystem;

namespace Character {
    public class PlayerMovement : MonoBehaviour {
        [SerializeField] private float moveSpeed = 10f;
        
        private Rigidbody2D _rb;
        private PlayerInput _playerInput;
        private InputAction _moveAction;
        private FogOfWall _fogOfWall;
        
        private void Awake() {
            _rb = GetComponent<Rigidbody2D>();
            _playerInput = GetComponent<PlayerInput>();
            _moveAction = _playerInput.actions["Move"];
            _fogOfWall = GetComponentInChildren<FogOfWall>();
        }

        private void Update() {
            Vector2 moveDirection = _moveAction.ReadValue<Vector2>();
            _fogOfWall?.UpdateRotation(moveDirection);
            moveDirection *= moveSpeed;
            _rb.velocity = moveDirection;

        }
        
        public void SetMoveSpeed(float speed) {
            moveSpeed = speed;
        }
    }
}