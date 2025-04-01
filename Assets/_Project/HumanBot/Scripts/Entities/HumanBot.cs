using HumanBot.States;
using UnityEngine;

namespace HumanBot.Entities {
    public class HumanBot : MonoBehaviour, IImposter
    {
        private StateMachine _stateMachine;

        private void Start() {
            var wanderState = new WanderingState(this);
            
            _stateMachine = new StateMachine();
            _stateMachine.SetState(wanderState);
        }

        private void Update () {
            _stateMachine.Tick();
        }

        public void Die()
        {
            Debug.Log("Die");
            Destroy(gameObject);
        }
    }
}