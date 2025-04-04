using Survivor.Booster;
using UnityEngine;

public class BoosterController : MonoBehaviour
{
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

    }

    private void UseMoreTime()
    {

    }

    public void UseLightUp()
    {

    }

    public void UseMoreItem()
    {

    }
    #endregion
}
