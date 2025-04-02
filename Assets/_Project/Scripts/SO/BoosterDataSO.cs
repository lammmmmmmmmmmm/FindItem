using System.Collections.Generic;
using UnityEngine;

namespace Survivor.Booster
{
    [CreateAssetMenu(fileName = "BoosterDataSO", menuName = "SO/Data/BoosterDataSO")]
    public class BoosterDataSO : ScriptableObject
    {
        public List<BoosterData> listBoosterData;
    }
}

