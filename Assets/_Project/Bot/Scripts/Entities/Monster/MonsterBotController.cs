using _Global;
using _Global.Utils;
using Bot.States;
using Pathfinding;
using UnityEngine;

namespace Bot.Entities.Monster {
    public class MonsterBotController : MonoBehaviour {
        [SerializeField] private TargetFinder humanFinder;

        private BotManager _botManager;

        private MonsterBotConfigSO _config;
        private StateMachine _stateMachine;
        private AIPath _ai;

        private bool _shouldChase;

        private void Awake() {
            _stateMachine = new StateMachine();
            _ai = GetComponent<AIPath>();
        }

        private void Start() {
            _ai.maxSpeed = _config.WanderingSpeed;
            humanFinder.SetRadius(_config.HumanDetectionRange);

            var wanderState = new WanderingState(_ai);
            var chaseState = new GoToTargetState(_ai, humanFinder);
            var attackState = new MonsterAttackState(this, _ai, humanFinder);

            _stateMachine.AddAnyTransition(wanderState, () => !humanFinder.Target);
            
            _stateMachine.AddTransition(wanderState, chaseState, () => humanFinder.Target && _shouldChase && !TargetIsInAttackRange());
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
            _ai.canMove = false;
            _ai.maxSpeed = 0;
        }
    }
}