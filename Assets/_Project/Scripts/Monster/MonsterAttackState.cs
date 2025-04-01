using DG.Tweening;
using Pathfinding;
using Survivor.Enemy;
using UnityEngine;

public class MonsterAttackState : IState
{
    private readonly MonsterController _monsterController;
    private readonly ImposterDetector _detector;
    private readonly IAstarAI _aiMovement;
    private IImposter _targetImposter;

    private bool isAttacking;

    public MonsterAttackState(MonsterController monsterController, ImposterDetector detector, IAstarAI aiMovement)
    {
        _monsterController = monsterController;
        _detector = detector;
        _aiMovement = aiMovement;
    }

    public void OnEnter()
    {
        _aiMovement.canMove = false;
        isAttacking = false;
        _targetImposter = _detector.TargetImposter;
        Debug.LogError("Change to Attack");
    }

    public void OnExit()
    {
        Debug.Log($"Exit state: Attack");
    }

    public void Tick()
    {
        if (isAttacking)
            return;

        Attack();
    }

    private void Attack()
    {
        isAttacking = true;
        // Anim Attack
        _monsterController.transform.DOScale(Vector2.one * 1.1f, 0.05f).SetLoops(4, LoopType.Yoyo);
        _targetImposter.Die();
    }
}
