using _Global;
using Bot.Entities.Human;
using Character;
using DG.Tweening;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private TargetFinder finder;
    private PlayerMovement movement;
    private HumanBotController _chosenTarget;

    [SerializeField] private GameObject rangeImage;
    private bool isAttacking;

    private void Awake()
    {
        finder = GetComponent<TargetFinder>();
        movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        finder.OnTargetInRange += AddNewBot;
        finder.OnTargetLost += RemoveBot;

        rangeImage.SetActive(false);
        isAttacking = false;
    }

    private void AddNewBot(GameObject botObject)
    {
        _chosenTarget = botObject.GetComponent<HumanBotController>();
        EnableRange(true);
    }

    private void RemoveBot()
    {
        _chosenTarget = null;
        EnableRange(false);
    }

    public void Attack()
    {
        if (_chosenTarget == null || isAttacking)
        {
            return;
        }
        isAttacking = true;
        movement.SetCanMove(false);

        var dir = _chosenTarget.transform.position - transform.position;
        transform.DOScale(Vector2.one * 2f, 0.1f).SetLoops(10, LoopType.Yoyo).OnComplete(() => {
            movement.SetCanMove(true);
            isAttacking = false;
            transform.localScale = Vector2.one;

            if (!_chosenTarget) return;
            _chosenTarget.Die(transform.position + dir.normalized);
        });
    }

    private void EnableRange(bool isEnable)
    {
        rangeImage.SetActive(isEnable);
    }
}
