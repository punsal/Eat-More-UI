#pragma warning disable 649

using UnityEngine;
using UnityEngine.UI;

public class TestPanelController : MonoBehaviour
{
    [Header("Panel Animator")]
    [SerializeField] private Animator anim;

    [Header("Input Fields")]
    [SerializeField] private InputField kill;
    [SerializeField] private InputField coin;
    [SerializeField] private InputField xp;

    [Header("Controllers")]
    [SerializeField] private IndicatorController indicators;
    [SerializeField] private LevelBarController levelBar;
    [SerializeField] private ItemStateController states;

    private void Start()
    {
        kill.contentType = InputField.ContentType.IntegerNumber;
        kill.characterLimit = 2;

        coin.contentType = InputField.ContentType.IntegerNumber;
        coin.characterLimit = 4;

        xp.contentType = InputField.ContentType.IntegerNumber;
        xp.characterLimit = 6;
    }

    #region Open-Close Panel
    public void OpenPanel()
    {
        anim.SetTrigger("IsOpen");
    }

    public void ClosePanel()
    {
        anim.SetTrigger("IsClose");
    }
    #endregion

    #region Test/Buttons Actions
    public void ClearFields()
    {
        string clear = "";

        kill.text = clear;
        coin.text = clear;
        xp.text = clear;
    }

    public void ResetData()
    {
        PlayerData.instance.SetDefault();
        indicators.Apply();
        levelBar.Apply();
        states.CheckStatesAgain();
    }

    public void ApplyChanges()
    {
        int killCount = 0;
        if(kill.text != "")
        {
            killCount = System.Convert.ToInt32(kill.text);
        }
        int coinCount = 0;
        if(coin.text != "")
        {
            coinCount = System.Convert.ToInt32(coin.text);
        }
        int xpCount = 0;
        if(xp.text != "")
        {
            xpCount = System.Convert.ToInt32(xp.text);
        }

        indicators.SetVariables(killCount, coinCount);
        indicators.Apply();

        levelBar.SetVariable(xpCount);
        levelBar.Apply();

        states.CheckStatesAgain();

        ClosePanel();
    }
    #endregion

}
