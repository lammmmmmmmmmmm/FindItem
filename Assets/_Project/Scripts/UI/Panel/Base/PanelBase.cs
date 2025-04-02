using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Survivor.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PanelBase : MonoBehaviour
    {
        protected PanelData _panelData;

        public List<TweenData> listOpenTween = new List<TweenData>();
        public List<TweenData> listCloseTween = new List<TweenData>();

        public virtual void Open(PanelData panelData)
        {
            _panelData = panelData;
            PlayTween(listOpenTween, OnOpenCompleted).Forget();
        }

        protected virtual void OnOpenCompleted()
        {

        }

        public virtual void Close()
        {
            PlayTween(listCloseTween, OnCloseCompleted).Forget();
        }

        protected virtual void OnCloseCompleted()
        {
            PanelManager.Instance.ClosePanel(this);
            Destroy(gameObject);
        }

        private async UniTaskVoid PlayTween(List<TweenData> listTween, Action callback)
        {
            List<UniTask> tasks = new List<UniTask>();
            foreach (TweenData tween in listTween)
            {
                tasks.Add(PanelManager.Instance.PlayTween(tween));
            }

            await UniTask.WhenAll(tasks);
            callback();
        }
    }
}
