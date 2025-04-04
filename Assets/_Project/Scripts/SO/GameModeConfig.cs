using UnityEngine;
using Survivor.Gameplay;

[CreateAssetMenu(fileName = "GameModeConfig", menuName = "SO/Data/GameModeConfig")]
public class GameModeConfig : ScriptableObject
{
    public GameMode gameMode;
    [Header("Player Data")]
    public int survived;
    public int die;
}
