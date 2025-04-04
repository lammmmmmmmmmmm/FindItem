using Cysharp.Threading.Tasks;
using DG.Tweening;
using Survivor.UI;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] private Slider progress;

    private void Start()
    {
        progress.value = 0;

        Loading().Forget();
    }

    private async UniTaskVoid Loading()
    {
        await progress.DOValue(1, 1f).SetEase(Ease.InOutQuart).ToUniTask();
        LoadingManager.Instance.LoadScene("HomeScene").Forget();
    }
}
