using Survivor.Patterns;

public class GameplayManager : Singleton<GameplayManager>
{
    public enum GameMode : byte
    {
        FindTheBlock,
        FeedTheMonster,
        FixTheMachine,
        LightTheRoom,
        Battle,
        MysteryNight,
    }

    private GameMode _currentGameMode;
}
