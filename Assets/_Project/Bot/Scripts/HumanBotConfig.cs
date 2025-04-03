using UnityEngine;

namespace HumanBot {
    [CreateAssetMenu(menuName = "Human Bot Config")]
    public class HumanBotConfig : ScriptableObject {
        [SerializeField] private float pickUpChance = 0.5f;
        [SerializeField] private float hideChance = 0.5f;
        [SerializeField] private float wanderingSpeed = 1f;
        [SerializeField] private float itemDetectionRange = 10f;
        [SerializeField] private float monsterDetectionRange = 5f;
        
        public float PickUpChance => pickUpChance;
        public float HideChance => hideChance;
        public float WanderingSpeed => wanderingSpeed;
        public float ItemDetectionRange => itemDetectionRange;
        public float MonsterDetectionRange => monsterDetectionRange;
    }
}