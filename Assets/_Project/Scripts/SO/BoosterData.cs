using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survivor.Booster
{
    [CreateAssetMenu(fileName = "BoosterDataSO", menuName = "SO/Data/BoosterData")]
    public class BoosterData : ScriptableObject
    {
        public BoosterType boosterType;
        public Sprite icon;
    }

    public enum BoosterType
    {
        None = -1,
        Speed,
        MoreTime,
        LightUp,
        MoreItem,
    }
}

