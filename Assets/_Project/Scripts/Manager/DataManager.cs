using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Survivor.Gameplay;
using Survivor.Patterns;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private const string PLAYER_DATA_KEY = "player_data";
    private const string SETTINGS_DATA_KEY = "settings_data";
    private const string GAMEPLAY_DATA_KEY = "gameplay_data";

    private PlayerData _playerData;
    private SettingsData _settingsData;
    private Dictionary<GameMode, GameModeData> _gameplayData;

    public PlayerData PlayerData
    {
        get
        {
            if (_playerData == null)
            {
                if (!PlayerPrefs.HasKey(PLAYER_DATA_KEY))
                {
                    _playerData = new PlayerData();
                    PlayerData = _playerData;
                }
                else
                {
                    _playerData = JsonConvert.DeserializeObject<PlayerData>(PlayerPrefs.GetString(PLAYER_DATA_KEY));
                }
            }

            return _playerData;
        }
        set
        {
            PlayerPrefs.SetString(PLAYER_DATA_KEY, JsonConvert.SerializeObject(value));
        }
    }

    public SettingsData SettingsData
    {
        get
        {
            if (_settingsData == null)
            {
                if (!PlayerPrefs.HasKey(SETTINGS_DATA_KEY))
                {
                    _settingsData = new SettingsData();
                    SettingsData = _settingsData;
                }
                else
                {
                    _settingsData = JsonConvert.DeserializeObject<SettingsData>(PlayerPrefs.GetString(SETTINGS_DATA_KEY));
                }
            }

            return _settingsData;
        }
        set
        {
            PlayerPrefs.SetString(SETTINGS_DATA_KEY, JsonConvert.SerializeObject(value));
        }
    }

    public Dictionary<GameMode, GameModeData> GameplayData
    {
        get
        {
            if (_gameplayData == null)
            {
                if (!PlayerPrefs.HasKey(GAMEPLAY_DATA_KEY))
                {
                    _gameplayData = new Dictionary<GameMode, GameModeData>();
                    GameplayData = _gameplayData;
                }
                else
                {
                    _gameplayData = JsonConvert.DeserializeObject<Dictionary<GameMode, GameModeData>>(
                        PlayerPrefs.GetString(GAMEPLAY_DATA_KEY)
                        );
                }
            }

            return _gameplayData;
        }

        private set
        {
            PlayerPrefs.SetString(GAMEPLAY_DATA_KEY, JsonConvert.SerializeObject(value));
        }
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        DontDestroyOnLoad(this);

        LoadData();
    }

    private void LoadData()
    {
        _playerData = PlayerData;
        _settingsData = SettingsData;
        _gameplayData = GameplayData;
    }
}

[Serializable]
public class PlayerData
{
    public string name;
    public Dictionary<GameResources, int> resourcesData;

    public PlayerData()
    {
        name = "";
        resourcesData = new Dictionary<GameResources, int>()
        {
            { GameResources.Coin, GameConfigs.COIN_INIT },
            { GameResources.Diamond, GameConfigs.DIAMOND_INIT },
        };
    }
}

[Serializable]
public class SettingsData
{
    public bool music = true;
    public bool sound = true;
    public bool vibration = true;
}

[SerializeField]
public class GameModeData
{
    public GameMode gameMode;
    public int totalSurvived = 0;
    public int totalDie = 0;

    public GameModeData(GameMode gameMode)
    {
        this.gameMode = gameMode;
    }
}

public enum GameResources
{
    None = -1,
    Coin = 0,
    Diamond = 1,
}