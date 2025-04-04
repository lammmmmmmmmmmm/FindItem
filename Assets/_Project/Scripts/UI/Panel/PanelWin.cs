using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Survivor.UI
{
    public class PanelWin : PanelBase
    {
        [Header("Reward")]
        [SerializeField] private TextMeshProUGUI textNumCoin;
        [SerializeField] private TextMeshProUGUI textNumDiamond;

        [Header("Button")]
        [SerializeField] private Button claimBtn;
        [SerializeField] private TextMeshProUGUI textQuantityClaim;
        [Space]
        [SerializeField] private Button bonusClaimBtn;
        [SerializeField] private TextMeshProUGUI textQuantityBonus;
        [SerializeField] private Button closeBtn;

        private int numCoin;
        private int numDiamond;

        protected override void Setup()
        {
            base.Setup();
            claimBtn.onClick.AddListener(ClickClaim);
            closeBtn.onClick.AddListener(ClickClaim);
            bonusClaimBtn.onClick.AddListener(ClickBonusClaim);
        }

        public override void Open(PanelData panelData)
        {
            base.Open(panelData);

            PlayerRole playerRole = panelData.Get<PlayerRole>(PanelDataKey.PlayerRole);

            numCoin = GameConfigs.COIN_WIN;
            numDiamond = GameConfigs.DIAMOND_WIN;

            textNumCoin.text = numCoin.ToString();
            textNumDiamond.text = numDiamond.ToString();

            bonusClaimBtn.gameObject.SetActive(false);
            closeBtn.gameObject.SetActive(false);
        }

        private void ClickClaim()
        {
            EffectManager.Instance.SpawnCoins(claimBtn.transform.position).Forget();
            Close();
            LoadingManager.Instance.LoadScene("HomeScene").Forget();
        }

        private void ClickBonusClaim()
        {
            EffectManager.Instance.SpawnDiamond(bonusClaimBtn.transform.position).Forget();
            Close();
            LoadingManager.Instance.LoadScene("HomeScene").Forget();
        }
    }
}

