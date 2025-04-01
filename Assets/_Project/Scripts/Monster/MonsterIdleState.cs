using System.Collections;
using System.Collections.Generic;
using Survivor.Enemy;
using UnityEngine;

public class MonsterIdleState : IState
{
    private readonly MonsterController _monsterController;

    public MonsterIdleState (MonsterController monsterController)
    {
        _monsterController = monsterController;
    }

    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        Debug.Log($"Exit state: Run");
    }

    public void Tick()
    {
        throw new System.NotImplementedException();
    }
}
