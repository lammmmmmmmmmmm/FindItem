using System.Collections;
using System.Collections.Generic;
using Survivor.Patterns;
using UnityEngine;

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
}
