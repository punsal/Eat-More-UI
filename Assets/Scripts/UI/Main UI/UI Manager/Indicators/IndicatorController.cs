#pragma warning disable 649

using UnityEngine;

public class IndicatorController : MonoBehaviour
{
    private UIDataBinder binder;

    private IIndicate killIndicator;
    private IIndicate coinIndicator;

    private GameObject killIndicatorObject;
    private GameObject coinIndicatorObject;

    private void Start()
    {
        binder = transform.parent.GetComponent<UIDataBinder>();

        killIndicatorObject = FindObjectOfType<KillIndicator>().GetIndicateObject();
        coinIndicatorObject = FindObjectOfType<CoinIndicator>().GetIndicateObject();

        killIndicator = killIndicatorObject.GetComponent<KillIndicator>();
        coinIndicator = coinIndicatorObject.GetComponent<CoinIndicator>();


        Apply();
    }

    public void Apply()
    {
        killIndicator.ApplyChanges(binder.GetIndicatorsData().killCount);
        coinIndicator.ApplyChanges(binder.GetIndicatorsData().coinCount);
        
    }

    public void ActivateIndicators()
    {
        killIndicatorObject.SetActive(true);
        coinIndicatorObject.SetActive(true);
    }

    public void DeactivateIndicators()
    {
        killIndicatorObject.SetActive(false);
        coinIndicatorObject.SetActive(false);
    }

    public int m_kill, m_coin;
    public void SetVariables(int kill, int coin)
    {
        for(int i = 0; i < kill; i++)
        {
            PlayerData.instance.AddPlayerKill();
        }
        PlayerData.instance.AdjustPlayerCoin(coin);
    }
}
