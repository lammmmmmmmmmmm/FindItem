using System.Collections.Generic;
using Survivor.Booster;
using UnityEngine;
using UnityEngine.UI;

namespace Survivor.UI
{
    public class PanelChooseBooster : PanelBase
    {
        private BoosterController boosterController;
        [SerializeField] private BoosterDataSO boosterDataSO;

        [SerializeField] private List<BoosterButton> listBoosterBtn = new();
        [SerializeField] private Button closeBtn;

        protected override void Setup()
        {
            base.Setup();
            closeBtn.onClick.AddListener(Close);
        }

        public override void Open(PanelData panelData)
        {
            base.Open(panelData);

            PlayerRole playerRole = panelData.Get<PlayerRole>(PanelDataKey.PlayerRole);
            boosterController = panelData.Get<BoosterController>(PanelDataKey.BoosterController);

            SetupBooster(playerRole);
        }

        private void SetupBooster(PlayerRole playerRole)
        {
            int counter = 0;
            foreach (var boosterData in boosterDataSO.listBoosterData)
            {
                if (!boosterData.useForRoles.Contains(playerRole))
                    continue;

                if (counter >= listBoosterBtn.Count)
                {
                    Debug.LogError("Not Enough Booster Button");
                    return;
                }

                listBoosterBtn[counter].SetButton(boosterData, playerRole, boosterController.UseBooster);
                counter++;
            }

            for (int i = counter; i < listBoosterBtn.Count; i++)
            {
                listBoosterBtn[i].gameObject.SetActive(false);
            }
        }
    }
}

