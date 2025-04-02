using System.Collections;
using System.Collections.Generic;
using Survivor.Patterns;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{


    public class PlayerData
    {

    }
}


public enum GameResources
{
    None = -1,
    Coin = 0,
    Diamond = 1,
}