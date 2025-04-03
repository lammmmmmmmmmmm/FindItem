using UnityEngine;
using UnityEngine.InputSystem;

namespace Character {
    public class PlayerMovement : MonoBehaviour, IImposter {
        [SerializeField] private float defaultMoveSpeed = 10f;

        public float DefaultMoveSpeed => defaultMoveSpeed;
        public float CurrentMoveSpeed { get; set; }

        private Rigidbody2D _rb;
        private PlayerInput _playerInput;
        private InputAction _moveAction;
        private FogOfWall _fogOfWall;
        
        private void Awake() {
            _rb = GetComponent<Rigidbody2D>();
            _playerInput = GetComponent<PlayerInput>();
            _moveAction = _playerInput.actions["Move"];
            
            CurrentMoveSpeed = defaultMoveSpeed;
            _fogOfWall = GetComponentInChildren<FogOfWall>();
        }

        private void FixedUpdate() {
            Vector2 moveDirection = _moveAction.ReadValue<Vector2>();
            _fogOfWall?.UpdateRotation(moveDirection);
            moveDirection *= CurrentMoveSpeed;
            _rb.velocity = moveDirection;
        }
        
        public void Die()
        {
            Debug.Log("Player Die");
        }
    }
}