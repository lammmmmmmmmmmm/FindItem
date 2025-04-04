using Cinemachine;
using UnityEngine;

namespace GameState {
    public class ChooseHumanOrMonster : MonoBehaviour {
        [SerializeField] private GameObject humanPlayer;
        [SerializeField] private GameObject monsterPlayer;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        
        private void Awake() {
            humanPlayer.SetActive(false);
            monsterPlayer.SetActive(false);
        }
        
        public void ChooseHuman() {
            humanPlayer.SetActive(true);
            monsterPlayer.SetActive(false);
            
            virtualCamera.Follow = humanPlayer.transform;
        }
        
        public void ChooseMonster() {
            humanPlayer.SetActive(false);
            monsterPlayer.SetActive(true);
            
            virtualCamera.Follow = monsterPlayer.transform;
        }
    }
}