using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Survivor.UI
{
    public interface ITweenState
    {
        public UniTask Play(Transform target, TweenConfig config);
    }

    public class FadeCanvasTween : ITweenState
    {
        public async UniTask Play(Transform target, TweenConfig config)
        {
            CanvasGroup canvas = target.GetComponent<CanvasGroup>();
            if (canvas == null)
            {
                return;
            }

            canvas.alpha = config.startValue;

            await UniTask.Delay(config.delay);

            await canvas.DOFade(config.endValue, config.duration).SetEase(config.ease).ToUniTask();
        }
    }

    public class FadeImageTween : ITweenState
    {
        public async UniTask Play(Transform target, TweenConfig config)
        {
            Image image = target.GetComponent<Image>();
            if (image == null)
            {
                return;
            }

            image.DOFade(config.startValue, 0).Complete();

            await UniTask.Delay(config.delay);

            await image.DOFade(config.endValue, config.duration).SetEase(config.ease).ToUniTask();
        }
    }

    public class ScaleTween : ITweenState
    {
        public async UniTask Play(Transform target, TweenConfig config)
        {
            target.DOScale(config.startValue, 0).Complete();

            await UniTask.Delay(config.delay);

            await target.DOScale(config.endValue, config.duration).SetEase(config.ease).ToUniTask();
        }
    }
}

