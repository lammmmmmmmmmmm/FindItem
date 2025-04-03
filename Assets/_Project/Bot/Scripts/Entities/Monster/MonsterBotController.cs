using _Global;
using _Global.Utils;
using Bot.States;
using Pathfinding;
using UnityEngine;

namespace Bot.Entities.Monster {
    public class MonsterBotController : MonoBehaviour {
        [SerializeField] private TargetFinder humanFinder;
        [SerializeField] private MonsterBotConfigSO config;

        private StateMachine _stateMachine;
        private AIPath _aiMovement;

        private const float ATTACK_RANGE = 0.8f;

        private void Awake() {
            _stateMachine = new StateMachine();
            _aiMovement = GetComponent<AIPath>();
        }

        private void Start() {
            _aiMovement.maxSpeed = config.WanderingSpeed;
            humanFinder.SetRadius(config.HumanDetectionRange);

            var wanderState = new WanderingState(_aiMovement);
            var chaseState = new GoToTargetState(_aiMovement);
            var attackState = new MonsterAttackState(this, _aiMovement, humanFinder);

            _stateMachine.AddAnyTransition(wanderState, () => !chaseState.HasTarget());

            _stateMachine.AddTransition(wanderState, chaseState, () => chaseState.HasTarget());
            _stateMachine.AddTransition(attackState, chaseState, () => chaseState.HasTarget() && TargetIsInRange());
            _stateMachine.AddTransition(chaseState, attackState, TargetIsInRange);

            _stateMachine.SetState(wanderState);

            humanFinder.OnTargetInRange += human => {
                if (MathUtils.RandomChance(config.ChaseChance)) {
                    chaseState.SetTarget(human);
                }
            };
            
            humanFinder.OnTargetLost += () => {
                chaseState.SetTarget(null);
            };
            
            return;

            bool TargetIsInRange() {
                if (!humanFinder.Target) return false;
                
                return Vector2.Distance(transform.position, humanFinder.Target.transform.position) <
                       ATTACK_RANGE;
            }
        }

        private void Update() => _stateMachine.Tick();
    }
}