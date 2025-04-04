using UnityEngine;

namespace Bot.Entities.Monster {
    [CreateAssetMenu(menuName = "Bot Config SO/Monster Bot Config SO")]
    public class MonsterBotConfigSO : ScriptableObject {
        [SerializeField] private float wanderingSpeed = 1f;
        [Range(0, 1)]
        [SerializeField] private float chaseChance = 0.5f;
        [SerializeField] private float humanDetectionRange = 5f;
        [SerializeField] private float attackRange = 3f;
        
        public float WanderingSpeed => wanderingSpeed;
        public float ChaseChance => chaseChance;
        public float HumanDetectionRange => humanDetectionRange;
        public float AttackRange => attackRange;
    }
}