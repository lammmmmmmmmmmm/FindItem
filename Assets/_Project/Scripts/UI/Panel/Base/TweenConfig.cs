using System;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "TweenConfig",menuName = "SO/UI/TweenConfig")]
[Serializable]
public class TweenConfig : ScriptableObject
{
    public TweenData.TweenType tweenType;

    public float startValue;
    public float endValue;
    [Space]
    public int delay;
    public float duration;

    public Ease ease;
}
