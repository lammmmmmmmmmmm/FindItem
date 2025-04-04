using System.Collections.Generic;
using UnityEngine;

namespace Survivor.UI
{
    public class PanelData
    {
        public Dictionary<string, object> data;

        public PanelData()
        {
            data = new Dictionary<string, object>();
        }

        public void Add(string key, object value)
        {
            if (data.ContainsKey(key))
            {
                Debug.LogWarning($"Key {key} is existed!");
                return;
            }

            data.Add(key, value);
        }

        public void Add(PanelDataKey key, object value)
        {
            Add(key.ToString(), value);
        }

        public T Get<T>(string key)
        {
            if (!data.ContainsKey(key))
                return default(T);

            return (T)data[key];
        }

        public T Get<T>(PanelDataKey key)
        {
            return Get<T>(key.ToString());
        }

    }

    public enum PanelDataKey : byte
    {
        //
        PlayerRole,
    }
}
