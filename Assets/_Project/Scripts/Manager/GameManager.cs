using System;
using Survivor.Gameplay;
using Survivor.Patterns;
using Survivor.UI;

public class GameManager : Singleton<GameManager>
{
    public Action<GameState> OnChangeState;
    public GameMode gameMode;

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
        PanelManager.Instance.OpenPanel<PanelWin>();
    }

    public void OnLose()
    {
        PanelManager.Instance.OpenPanel<PanelLose>();
    }

    public void OnTimeUp()
    {
        PanelManager.Instance.OpenPanel<PanelMoreTime>();
    }

    public void OnKilled()
    {
        PanelManager.Instance.OpenPanel<PanelRevive>();
    }
}

public enum PlayerRole
{
    Imposter,
    Monster
}

