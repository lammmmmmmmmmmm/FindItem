using UnityEngine;
using UnityEngine.InputSystem;

namespace Character {
    public class PlayerMovement : MonoBehaviour {
        [SerializeField] private Joystick joystick;
        [SerializeField] private float defaultMoveSpeed = 10f;

        public float DefaultMoveSpeed => defaultMoveSpeed;
        public float CurrentMoveSpeed { get; set; }

        private Rigidbody2D _rb;
        private FogOfWall _fogOfWall;

        private void Awake() {
            _rb = GetComponent<Rigidbody2D>();
            _fogOfWall = GetComponentInChildren<FogOfWall>();

            CurrentMoveSpeed = defaultMoveSpeed;
        }

        private void FixedUpdate() {
            if (joystick.Direction.y != 0 || joystick.Direction.x != 0) {
                Vector2 moveDirection = new Vector2(joystick.Direction.x, joystick.Direction.y);
                _rb.velocity = moveDirection * 10f; // Adjust speed as needed
            
                _fogOfWall?.UpdateRotation(moveDirection);
            } else {
                _rb.velocity = Vector2.zero; // Stop movement when joystick is not used
            }
        }
        
        public void SetMoveSpeed(float moveSpeed) {
            CurrentMoveSpeed = moveSpeed;
        }
    }
}