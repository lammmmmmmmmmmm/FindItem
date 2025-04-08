using UnityEngine;

public class UIIngame : MonoBehaviour
{
    [SerializeField] private GameObject uiForImposter;
    [SerializeField] private GameObject uiForMonster;

    public void SetRoleUI(PlayerRole playerRole)
    {
        uiForImposter.SetActive(playerRole == PlayerRole.Imposter);
        uiForMonster.SetActive(playerRole == PlayerRole.Monster);
    }

    public void SetWaitingUI()
    {

    }

    public void SetStartUI()
    {

    }
}
