using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Survivor.UI
{
    public class PanelChooseRole : PanelBase
    {
        [SerializeField] private Button chooseImposterBtn;
        [SerializeField] private Button chooseMonsterBtn;

        private Action<PlayerRole> chooseRoleAction;

        protected override void Setup()
        {
            base.Setup();
            chooseImposterBtn.onClick.AddListener(() => OnClickChooseRole(PlayerRole.Imposter));
            chooseMonsterBtn.onClick.AddListener(() => OnClickChooseRole(PlayerRole.Monster));
        }

        public override void Open(PanelData panelData)
        {
            base.Open(panelData);
            Action<PlayerRole> callback = panelData.Get<Action<PlayerRole>>("ChooseRoleAction");
            chooseRoleAction = callback;
        }

        private void OnClickChooseRole(PlayerRole playerRole)
        {
            chooseRoleAction?.Invoke(playerRole);
            Close();
        }
    }

}
