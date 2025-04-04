using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Survivor.Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        private WaitForSeconds waitTime = new WaitForSeconds(0.5f);

        private void Awake()
        {
            DontDestroyOnLoad(this);

            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;

            canvasGroup = loadingObject.GetComponent<CanvasGroup>();
            loadingObject.SetActive(false);
        }

        private void Setup()
        {
            // Random character decor

            // Reset Progress
            progress.value = 0;
            canvasGroup.alpha = 0.8f;
        }

        private async UniTask StartLoading()
        {
            loadingObject.SetActive(true);
            await canvasGroup.DOFade(1, 0.1f).SetEase(Ease.Linear).ToUniTask();
        }

        private void StopLoading()
        {
            canvasGroup.DOFade(0.5f, 0.1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                loadingObject.SetActive(false);
            });
        }

        IEnumerator LoadNextScene(string nameScene)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(nameScene);
            operation.allowSceneActivation = false;

            var tween = progress.DOValue(0.9f, 0.2f);

            while (operation.progress < 0.9f)
            {
                yield return null;
            }

            operation.allowSceneActivation = true;

            tween.Complete();
            progress.DOValue(1, 0.1f);

            yield return waitTime;
            StopLoading();
        }

        public async UniTaskVoid LoadScene(string nameScene)
        {
            Setup();
            await StartLoading();
            StartCoroutine(LoadNextScene(nameScene));
        }
    }
}

