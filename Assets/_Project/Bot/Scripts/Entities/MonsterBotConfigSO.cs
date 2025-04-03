using UnityEngine;

namespace Bot.Entities {
    [CreateAssetMenu(menuName = "Monster Bot Config SO")]
    public class MonsterBotConfigSO : ScriptableObject {
        [Range(0, 1)]
        [SerializeField] private float chaseChance = 0.5f;
        [SerializeField] private float wanderingSpeed = 1f;
        [SerializeField] private float humanDetectionRange = 5f;
        
        public float ChaseChance => chaseChance;
        public float WanderingSpeed => wanderingSpeed;
        public float HumanDetectionRange => humanDetectionRange;
    }
}