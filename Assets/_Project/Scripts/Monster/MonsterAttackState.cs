using System.Collections;
using System.Collections.Generic;
using Survivor.Enemy;
using UnityEngine;

public class MonsterAttackState : IState
{
    private readonly MonsterController _monsterController;

    public MonsterAttackState(MonsterController monsterController)
    {
        _monsterController = monsterController;
    }

    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public void Tick()
    {
        throw new System.NotImplementedException();
    }
}
