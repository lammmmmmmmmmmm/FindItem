using FOW;
using GameState;
using Survivor.Booster;
using UnityEngine;

public class BoosterController
{
    private GameplayController gameplayController;

    public BoosterController (GameplayController gameplayController)
    {
        this.gameplayController = gameplayController;
    }

    public void UseBooster(BoosterType boosterType)
    {
        switch (boosterType)
        {
            case BoosterType.Speed:
                UseSpeed();
                break;

            case BoosterType.MoreTime:
                UseMoreTime();
                break;

            case BoosterType.MoreItem:
                UseMoreItem();
                break;

            case BoosterType.LightUp:
                UseLightUp();
                break;

            default:
                Debug.LogError("Not have booster " + boosterType);
                return;
        }
    }

    #region Booster 
    private void UseSpeed()
    {
        Debug.Log("Use Speed");
        gameplayController.BoostSpeed();
    }

    private void UseMoreTime()
    {
        Debug.Log("Use More Time");
        gameplayController.AddTime();
    }

    public void UseLightUp()
    {
        Debug.Log("Use Light Up");
        gameplayController.EnableLight();
    }

    public void UseMoreItem()
    {
        Debug.Log("Use More Item");
        gameplayController.MoreItem();
    }
    #endregion
}
