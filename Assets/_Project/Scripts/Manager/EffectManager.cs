using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DarkTonic.PoolBoss;
using DG.Tweening;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private UIManager uiManager;
    [SerializeField] private Vector2 spawnRange = new Vector2(1f, 1f);
    [SerializeField] private float duration = 0.5f;

    [Header("Coin Configs")]
    [SerializeField] private Transform coinPrefab;
    [SerializeField] private int coinAmount = 5;
    [SerializeField] private Transform targetCoinBar;

    [Header("Diamond Configs")]
    [SerializeField] private Transform diamondPrefab;
    [SerializeField] private int diamondAmount = 5;
    [SerializeField] private Transform targetDiamondBar;

    public void Init(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    public async UniTask SpawnCoins(Vector3 startPosition)
    {
        List<UniTask> tasks = new List<UniTask>();

        for (int i = 0; i < coinAmount; i++)
        {
            Transform coinTf = PoolBoss.Spawn(coinPrefab, startPosition, Quaternion.identity, transform);
            coinTf.localScale = Vector3.one;
            coinTf.position = startPosition + new Vector3(Random.Range(-spawnRange.x, spawnRange.x), Random.Range(-spawnRange.y, spawnRange.y), 0);

            tasks.Add(coinTf.DOMove(targetCoinBar.position, duration)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() => PoolBoss.Despawn(coinTf))
                .ToUniTask());

            await UniTask.Delay(100);
        }

        await UniTask.WhenAll(tasks);
    }

    public async UniTask SpawnDiamond(Vector3 startPosition)
    {
        List<UniTask> tasks = new List<UniTask>();

        for (int i = 0; i < diamondAmount; i++)
        {
            Transform diamond = PoolBoss.Spawn(diamondPrefab, startPosition, Quaternion.identity, transform);
            diamond.localScale = Vector3.one;
            diamond.position = startPosition + new Vector3(Random.Range(-spawnRange.x, spawnRange.x), Random.Range(-spawnRange.y, spawnRange.y), 0);

            tasks.Add(diamond.DOMove(targetDiamondBar.position, duration)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() => PoolBoss.Despawn(diamond))
                .ToUniTask());

            await UniTask.Delay(100);
        }

        await UniTask.WhenAll(tasks);
    }
}
