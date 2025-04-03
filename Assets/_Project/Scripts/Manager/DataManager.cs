using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Survivor.Patterns;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private const string PLAYER_DATA_KEY = "player_data";
    private const string SETTINGS_DATA_KEY = "settings_data";

    private PlayerData _playerData;
    private SettingsData _settingsData;

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
    }
}

[Serializable]
public class PlayerData
{
    public string name;
    public Dictionary<GameResources, int> resourcesData;
}

[Serializable]
public class SettingsData
{
    public bool music;
    public bool sound;
    public bool vibration;
}

public enum GameResources
{
    None = -1,
    Coin = 0,
    Diamond = 1,
}