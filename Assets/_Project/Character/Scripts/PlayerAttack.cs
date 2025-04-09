using _Global;
using Bot.Entities.Human;
using DG.Tweening;
using UnityEngine;

namespace Character {
    public class PlayerAttack : MonoBehaviour {
        private TargetFinder _finder;
        private PlayerMovement _movement;

        [SerializeField] private GameObject rangeImage;
        private bool _isAttacking;

        private void Awake() {
            _finder = GetComponent<TargetFinder>();
            _movement = GetComponent<PlayerMovement>();
        }

        private void Start() {
            _finder.OnTargetInRange += AddNewBot;
            _finder.OnTargetLost += RemoveBot;

            rangeImage.SetActive(false);
            _isAttacking = false;
        }

        private void AddNewBot(GameObject botObject) {
            rangeImage.SetActive(true);
        }

        private void RemoveBot() {
            rangeImage.SetActive(false);
        }

        public void Attack() {
            if (!_finder.Target || _isAttacking) {
                return;
            }
            
            var tempTarget = _finder.Target.GetComponent<HumanBotController>();

            _isAttacking = true;
            _movement.SetCanMove(false);
            
            transform.DOScale(Vector2.one * 2f, 0.1f).SetLoops(4, LoopType.Yoyo).OnComplete(() => {
                _movement.SetCanMove(true);
                _isAttacking = false;
                transform.localScale = Vector2.one;
                
                var targetCollider = tempTarget.GetComponent<Collider2D>();
                if (targetCollider) {
                    targetCollider.enabled = false;
                }

                var dir = tempTarget.transform.position - transform.position;
                tempTarget.Die(transform.position + dir.normalized);
            });
        }
    }
}