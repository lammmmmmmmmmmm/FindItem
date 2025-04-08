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
            claimBtn.onClick.AddListener(() => ClickClaim().Forget());
            closeBtn.onClick.AddListener(() => ClickClaim().Forget());
            bonusClaimBtn.onClick.AddListener(() => ClickBonusClaim().Forget());
        }

        public override void Open(PanelData panelData)
        {
            base.Open(panelData);

            //PlayerRole playerRole = panelData.Get<PlayerRole>(PanelDataKey.PlayerRole);

            numCoin = GameConfigs.COIN_WIN;
            numDiamond = GameConfigs.DIAMOND_WIN;

            textNumCoin.text = numCoin.ToString();
            textNumDiamond.text = numDiamond.ToString();

            bonusClaimBtn.gameObject.SetActive(false);
            closeBtn.gameObject.SetActive(false);
            claimBtn.gameObject.SetActive(true);
        }

        private async UniTaskVoid ClickClaim()
        {
            await UIManager.Instance.EffectManager.SpawnCoins(claimBtn.transform.position);
            Close();
            LoadingManager.Instance.LoadScene("HomeScene").Forget();
        }

        private async UniTaskVoid ClickBonusClaim()
        {
            await UIManager.Instance.EffectManager.SpawnDiamond(bonusClaimBtn.transform.position);
            Close();
            LoadingManager.Instance.LoadScene("HomeScene").Forget();
        }

        public override void Close()
        {
            base.Close();
            LoadingManager.Instance.LoadScene("HomeScene").Forget();
        }
    }
}

