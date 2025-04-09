using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Survivor.UI
{
    public class PanelRevive : PanelBase
    {
        [SerializeField] private TextMeshProUGUI textCountDown;
        [SerializeField] private Button continueBtn;
        [SerializeField] private Button closeBtn;

        private float timeRemaining = 5f;

        protected override void Setup()
        {
            base.Setup();
            continueBtn.onClick.AddListener(ClickContinue);
            closeBtn.onClick.AddListener(ClickClose);
        }

        public override void Open(PanelData panelData = null)
        {
            base.Open(panelData);

            textCountDown.text = Mathf.CeilToInt(timeRemaining).ToString();

            StartCoroutine(CountDownLose());
        }

        IEnumerator CountDownLose()
        {
            while (timeRemaining > 0)
            {
                textCountDown.text = Mathf.CeilToInt(timeRemaining).ToString();
                timeRemaining -= Time.deltaTime;
                yield return null;
            }

            ClickClose();
        }

        private void ClickContinue()
        {
            Close();
        }

        private void ClickClose()
        {
            Close();
            GameManager.Instance.OnLose();
        }

        private void OnDisable()
        {
            StopCoroutine(CountDownLose());
        }
    }
}
