using UnityEngine;
using Survivor.Gameplay;

[CreateAssetMenu(fileName = "GameModeData", menuName = "SO/Data/GameModeData")]
public class GameModeData : ScriptableObject
{
    public GameMode gameMode;
    [Header("Player Data")]
    public int survived;
    public int die;
}
