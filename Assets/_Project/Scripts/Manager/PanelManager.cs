using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Survivor.Patterns;
using Survivor.UI;
using UnityEngine;

public class PanelManager : Singleton<PanelManager>
{
    [SerializeField] private Transform container;
    [SerializeField] private string path = "Panels";

    private Dictionary<string, GameObject> cachedPanel = new Dictionary<string, GameObject>();
    private List<PanelBase> listPanelRelease = new List<PanelBase>();

    public override void OnAwake()
    {
        base.OnAwake();
        if (container == null)
            container = transform;
    }

    public T OpenPanel<T>(PanelData panelData) where T : PanelBase
    {
        T panel = OpenPanel<T>(typeof(T).Name);

        if (panel == null)
        {
            return null;
        }

        panel.Open(panelData);
        listPanelRelease.Add(panel);

        return panel;
    }

    public T OpenPanel<T>(string name) where T : PanelBase
    {
        if (!cachedPanel.ContainsKey(name))
        {
            GameObject panelObj = Resources.Load<GameObject>($"{path}/{name}");
            if (panelObj == null)
            {
                Debug.LogError($"Not have panel {name} in {path}");
            }

            cachedPanel.Add(name, panelObj);
        }
        GameObject panelObject = cachedPanel[name];
        Instantiate(panelObject, container);

        return panelObject.GetComponent<T>();
    }

    public void ClosePanel(PanelBase panel)
    {
        listPanelRelease.Remove(panel);
    }

    public async UniTask PlayTween(TweenData tweenData)
    {
        Transform target = tweenData.target;
        TweenData.TweenType tweenType = tweenData.config.tweenType;

        ITweenState tweenState;

        switch (tweenType)
        {
            case TweenData.TweenType.FadeCanvas:
                tweenState = new FadeCanvasTween();
                break;
            case TweenData.TweenType.FadeImage:
                tweenState = new FadeImageTween();
                break;
            case TweenData.TweenType.Scale:
                tweenState = new ScaleTween();
                break;
            default:
                return;
        }

        await tweenState.Play(target, tweenData.config);
    }
}
