using System;
using Survivor.Patterns;

public class GameManager : Singleton<GameManager>
{
    public Action<GameState> OnChangeState;
    
    protected override void OnAwake()
    {
        base.OnAwake();
        DontDestroyOnLoad(gameObject);
    }

    public enum GameState : byte
    {
        Home,
        Play,
        Pause,
        Win,
        Lose,
    }

    public GameState state;

    public void SetState(GameState state)
    {
        this.state = state;
        OnChangeState?.Invoke(state);
    }

    public void OnWin()
    {

    }

    public void OnLose()
    {

    }
}

public enum PlayerRole
{
    Imposter,
    Monster
}

