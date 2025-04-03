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

        private bool _shouldChase;

        private void Awake() {
            _stateMachine = new StateMachine();
            _aiMovement = GetComponent<AIPath>();
        }

        private void Start() {
            _aiMovement.maxSpeed = config.WanderingSpeed;
            humanFinder.SetRadius(config.HumanDetectionRange);

            var wanderState = new WanderingState(_aiMovement);
            var chaseState = new GoToTargetState(_aiMovement, humanFinder);
            var attackState = new MonsterAttackState(this, _aiMovement, humanFinder);

            _stateMachine.AddAnyTransition(wanderState, () => !humanFinder.Target);

            _stateMachine.AddTransition(wanderState, chaseState, () => humanFinder.Target && _shouldChase);
            _stateMachine.AddTransition(chaseState, attackState, () => humanFinder.Target && TargetIsInAttackRange());

            _stateMachine.SetState(wanderState);

            humanFinder.OnTargetInRange += _ => { _shouldChase = MathUtils.RandomChance(config.ChaseChance); };
            humanFinder.OnTargetLost += () => { _shouldChase = false; };

            return;

            bool TargetIsInAttackRange() {
                return Vector2.Distance(transform.position, humanFinder.Target.transform.position) < config.AttackRange;
            }
        }

        private void Update() => _stateMachine.Tick();
    }
}