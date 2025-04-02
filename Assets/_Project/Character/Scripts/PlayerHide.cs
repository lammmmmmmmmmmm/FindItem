namespace Character {
    public class PlayerHide : Hide {
        private PlayerMovement _playerMovement;
        
        private void Awake() {
            _playerMovement = GetComponent<PlayerMovement>();
        }
        
        protected override void DisableMovement() {
            if (!_playerMovement) {
                _playerMovement.enabled = false;
            }
        }

        protected override void EnableMovement(float speedMultiplier) {
            if (_playerMovement) {
                _playerMovement.enabled = true;
                _playerMovement.CurrentMoveSpeed = _playerMovement.DefaultMoveSpeed * speedMultiplier;
            }
        }
    }
}