using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Survivor.UI
{
    [RequireComponent(typeof(Button))]
    public class GameModeButton : MonoBehaviour
    {
        private Button button;
        [SerializeField] private GameplayManager.GameMode gameMode;

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
            Debug.Log("Play Mode: " + gameMode.ToString());
        }
    }
}
