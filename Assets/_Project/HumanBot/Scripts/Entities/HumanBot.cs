using HumanBot.States;
using UnityEngine;

namespace HumanBot.Entities {
    public class HumanBot : MonoBehaviour {
        private StateMachine _stateMachine;

        private void Start() {
            var wanderState = new WanderingState(this);
            
            _stateMachine = new StateMachine();
            _stateMachine.SetState(wanderState);
        }

        private void Update () {
            _stateMachine.Tick();
        }
    }
}