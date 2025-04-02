using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameModeData", menuName = "SO/Data/GameModeData")]
public class GameModeData : ScriptableObject
{
    public GameplayManager.GameMode gameMode;
    [Header("Player Data")]
    public int survived;
    public int die;
}
