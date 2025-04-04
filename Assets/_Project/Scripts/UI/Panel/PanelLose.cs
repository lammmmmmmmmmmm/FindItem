using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Survivor.UI
{
    public class PanelLose : PanelBase
    {
        [SerializeField] private Button closeButton;

        protected override void Setup()
        {
            base.Setup();
            closeButton.onClick.AddListener(Close);
        }

        public override void Close()
        {
            base.Close();
            LoadingManager.Instance.LoadScene("HomeScene").Forget();
        }
    }
}
