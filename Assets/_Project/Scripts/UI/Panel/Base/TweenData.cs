using System;
using UnityEngine;

[Serializable]
public class TweenData
{
    public Transform target;
    public TweenConfig config;

    public enum TweenType : byte
    {
        FadeCanvas = 0,
        FadeImage = 1,
        Scale = 2,
    }
}
