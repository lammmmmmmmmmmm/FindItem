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

        private bool canMove;

        private void Awake() {
            _rb = GetComponent<Rigidbody2D>();
            _fogOfWall = GetComponentInChildren<FogOfWall>();

            CurrentMoveSpeed = defaultMoveSpeed;
            canMove = true;
        }

        private void FixedUpdate() {
            if (!canMove)
            {
                _rb.velocity = Vector2.zero;
                return;
            }

            if (joystick.Direction.y != 0 || joystick.Direction.x != 0) {
                Vector2 moveDirection = new Vector2(joystick.Direction.x, joystick.Direction.y);
                _rb.velocity = moveDirection * CurrentMoveSpeed; // Adjust speed as needed
            
                _fogOfWall?.UpdateRotation(moveDirection);
            } else {
                _rb.velocity = Vector2.zero; // Stop movement when joystick is not used
            }
        }
        
        public void SetCanMove(bool canMove)
        {
            this.canMove = canMove;
        }

        public void BoostSpeed()
        {
            CurrentMoveSpeed = defaultMoveSpeed * 1.3f;
            Debug.Log("BoostSpeed");
        }
    }
}