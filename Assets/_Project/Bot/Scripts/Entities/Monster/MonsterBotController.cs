using _Global;
using _Global.Utils;
using Bot.States;
using Pathfinding;
using UnityEngine;

namespace Bot.Entities.Monster {
    public class MonsterBotController : MonoBehaviour {
        [SerializeField] private TargetFinder humanFinder;
        
        private MonsterBotConfigSO _config;
        private StateMachine _stateMachine;
        private AIPath _aiMovement;

        private bool _shouldChase;

        private void Awake() {
            _stateMachine = new StateMachine();
            _aiMovement = GetComponent<AIPath>();
        }

        private void Start() {
            _aiMovement.maxSpeed = _config.WanderingSpeed;
            humanFinder.SetRadius(_config.HumanDetectionRange);

            var wanderState = new WanderingState(_aiMovement);
            var chaseState = new GoToTargetState(_aiMovement, humanFinder);
            var attackState = new MonsterAttackState(this, _aiMovement, humanFinder);

            _stateMachine.AddAnyTransition(wanderState, () => !humanFinder.Target);
            _stateMachine.AddAnyTransition(chaseState, () => humanFinder.Target && _shouldChase && !TargetIsInAttackRange());
            
            _stateMachine.AddTransition(chaseState, attackState, () => humanFinder.Target && TargetIsInAttackRange());

            _stateMachine.SetState(wanderState);

            humanFinder.OnTargetInRange += _ => { _shouldChase = MathUtils.RandomChance(_config.ChaseChance); };
            humanFinder.OnTargetLost += () => { _shouldChase = false; };

            return;

            bool TargetIsInAttackRange() {
                return Vector2.Distance(transform.position, humanFinder.Target.transform.position) < _config.AttackRange;
            }
        }

        private void Update() => _stateMachine.Tick();
        
        public void SetConfig(MonsterBotConfigSO config) {
            _config = config;
        }
        
        public void Stop() {
            _aiMovement.isStopped = true;
            _aiMovement.maxSpeed = 0;
        }
    }
}