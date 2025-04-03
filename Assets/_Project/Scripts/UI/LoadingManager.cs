using DG.Tweening;
using Survivor.Patterns;
using UnityEngine;
using UnityEngine.UI;

namespace Survivor.UI
{
    public class LoadingManager : Singleton<LoadingManager>
    {
        [Header("UI Components")]
        [SerializeField] private GameObject loadingObject;
        private CanvasGroup canvasGroup;

        [SerializeField] private Image iconDecor;
        [SerializeField] private Slider progress;

        private void Awake()
        {
            canvasGroup = loadingObject.GetComponent<CanvasGroup>();
            loadingObject.SetActive(false);
        }

        private void Setup()
        {
            // Random character decor

            // Reset Progress
            progress.value = 0;
            canvasGroup.alpha = 0;
        }

        private void StartLoading()
        {
            loadingObject.SetActive(true);
            canvasGroup.DOFade(1, 0.1f).SetEase(Ease.Linear);
        }

        private void OnEnable()
        {
            Setup();
            StartLoading();
        }
    }
}

