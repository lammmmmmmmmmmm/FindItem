using System;
using Survivor.Gameplay;
using Survivor.Patterns;
using Survivor.UI;

public class GameManager : Singleton<GameManager> {
    public Action<GameState> OnChangeState;
    public GameMode gameMode;
    public GameState state;
    
    public enum GameState : byte {
        Home,
        Play,
        Pause,
        Win,
        Lose,
    }
    
    protected override void OnAwake() {
        base.OnAwake();
        DontDestroyOnLoad(gameObject);
    }

    public void SetState(GameState state) {
        this.state = state;
        OnChangeState?.Invoke(state);
    }

    public void OnWin() {
        PanelManager.Instance.OpenPanel<PanelWin>();
    }

    public void OnLose() {
        PanelManager.Instance.OpenPanel<PanelLose>();
    }
}

public enum PlayerRole {
    Imposter,
    Monster
}