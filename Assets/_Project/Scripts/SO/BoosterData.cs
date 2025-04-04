using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Survivor.Booster
{
    [CreateAssetMenu(fileName = "BoosterDataSO", menuName = "SO/Data/BoosterData")]
    public class BoosterData : ScriptableObject
    {
        public BoosterType boosterType;
        public string boosterName;

        public List<PlayerRole> useForRoles;

        [Header("icon")]
        public Sprite iconImposter;
        public Sprite iconMonster;
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

