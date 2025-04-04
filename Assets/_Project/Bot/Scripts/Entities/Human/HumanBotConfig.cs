using UnityEngine;

namespace Bot.Entities.Human {
    [CreateAssetMenu(menuName = "Bot Config SO/Human Bot Config SO")]
    public class HumanBotConfig : ScriptableObject {
        [SerializeField] private float wanderingSpeed = 1f;
        [Range(0, 1)]
        [SerializeField] private float pickUpChance = 0.5f;
        [Range(0, 1)]
        [SerializeField] private float hideChance = 0.5f;
        
        [SerializeField] private float itemDetectionRange = 10f;
        [SerializeField] private float monsterDetectionRange = 5f;
        
        public float WanderingSpeed => wanderingSpeed;
        public float PickUpChance => pickUpChance;
        public float HideChance => hideChance;
        public float ItemDetectionRange => itemDetectionRange;
        public float MonsterDetectionRange => monsterDetectionRange;
    }
}