
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Survivor.Gameplay;

namespace Survivor.UI
{
    [RequireComponent(typeof(Button))]
    public class GameModeButton : MonoBehaviour
    {
        private Button button;
        [SerializeField] private GameMode gameMode;

        [Header("UI Components")]
        [SerializeField] private TextMeshProUGUI textModeName;
        [SerializeField] private GameObject mostPlayerObj;
        [SerializeField] private TextMeshProUGUI textNumSurvived;
        [SerializeField] private TextMeshProUGUI textNumDie;

        [Space]
        [SerializeField] private Image imageMode;

        private void Start()
        {
            //Setup 

            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            LoadingManager.Instance.LoadScene(gameMode.ToString()).Forget();
        }

        private void UpdateVisual()
        {
            textNumSurvived.text = DataManager.Instance.GetTotalSurvived(gameMode).ToString();
            textNumDie.text = DataManager.Instance.GetTotalDie(gameMode).ToString();
        }

        private void OnEnable()
        {
            UpdateVisual();
        }
    }
}
