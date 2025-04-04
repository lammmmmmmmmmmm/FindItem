using Survivor.Patterns;
using Survivor.UI;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private UIResource coinBar;
    [SerializeField] private UIResource diamondBar;
    
    private EffectManager effectManager;
    public EffectManager EffectManager => effectManager;

    protected override void OnAwake()
    {
        base.OnAwake();
        DontDestroyOnLoad(gameObject);

        effectManager = FindObjectOfType<EffectManager>();
    }

    private void Start()
    {
        effectManager.Init(this);
    }
}
