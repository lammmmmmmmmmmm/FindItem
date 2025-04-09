using System;
using Survivor.Booster;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Survivor.UI
{
    public class BoosterButton : MonoBehaviour
    {
        public Action<BoosterType> OnClickCompleted;
        private BoosterType boosterType;

        [Header("UI Components")]
        [SerializeField] private TextMeshProUGUI textNameBooster;
        [SerializeField] private Image iconBooster;
        [SerializeField] private Button button;

        private void Start()
        {
            button.onClick.AddListener(OnClick);
        }

        public void SetButton(BoosterData boosterData, PlayerRole playerRole, Action<BoosterType> action)
        {
            OnClickCompleted = action;

            boosterType = boosterData.boosterType;
            button.gameObject.SetActive(true);

            iconBooster.sprite = playerRole == PlayerRole.Imposter ? boosterData.iconImposter : boosterData.iconMonster;
            textNameBooster.text = boosterData.boosterName;

            gameObject.SetActive(true);
        }

        private void OnClick()
        {
            button.gameObject.SetActive(false);
            OnClickCompleted?.Invoke(boosterType);
        }
    }
}

