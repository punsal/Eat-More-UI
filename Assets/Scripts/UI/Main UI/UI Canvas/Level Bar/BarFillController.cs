#pragma warning disable 649

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum BarFillType { Static, Animated }
public enum MapCheck { Enabled, Disabled }

[ExecuteAlways]
public class BarFillController : MonoBehaviour
{
    [Header("Data Binder")]
    [SerializeField] private UIDataBinder binder;

    [Header("Item")]
    public MapCheck mapCheck;
    [SerializeField] private ItemStateController stateController;

    [Header("Components")]
    [SerializeField] private Animator anim;
    [SerializeField] private Image filler;
    [SerializeField] private Text textXP;
    [Header("Fill Field")]
    [SerializeField] private float startValue;
    [Header("Animation Properties")]
    [SerializeField] private BarFillType fillType;
    [SerializeField] private float yieldTime;
    [SerializeField] private float fillAmount;

    /// <summary>
    /// It controls if Level Bar's LevelUp animation must be called.
    /// Default : False.
    /// </summary>
    public bool isLevelUp { get; private set; } = false;
    /// <summary>
    /// It controls if Level Bar's XP : Text is decreasing.
    /// Default : False.
    /// </summary>
    public bool isXPDecreasing { get; private set; } = false;
    /// <summary>
    /// It controls if Level Bar's fill animation is still ongoing.
    /// Default : True.
    /// </summary>
    public bool isAnimationOn { get; private set; } = true;
    public BarFillType FillType { get => fillType; }

    public void SetFillType(bool isAnimated)
    {
        if (isAnimated) { fillType = BarFillType.Animated; } else fillType = BarFillType.Static;
    }

    public void UpdateIndicator(int level)
    {
        LevelIndicator indicator = FindObjectOfType<LevelIndicator>();
        indicator.ApplyChanges(level);
    }

    public void SetXPText(int xp)
    {
        textXP.text = "+ " + xp + " XP";

        if (!isXPDecreasing)
        {
            StartCoroutine(DecreaseXPText(xp));
        }
    }

    public void DisableXPText()
    {
        textXP.gameObject.SetActive(false);
    }

    private IEnumerator DecreaseXPText(int xp)
    {
        isXPDecreasing = true;
        int decreaseRate = 1;
        while (xp > 0)
        {
            xp -= decreaseRate;
            if (xp <= 0)
            {
                DisableXPText();
                break;
            }

            SetXPText(xp);

            decreaseRate++;
            yield return new WaitForSeconds(yieldTime);
        }
        isXPDecreasing = false;
    }

    public void SetFill(int percentage)
    {
        float amount = CalculateAmount(percentage);

        filler.fillAmount = amount;
    }

    private float CalculateAmount(int percentage)
    {
        if (percentage >= 100)
        {
            percentage = percentage % 100;
        }
        return (percentage * (1f - startValue) / 100f + startValue);
    }

    private float currentAmount;
    public IEnumerator AnimateFill(int percentage)
    {
        isAnimationOn = true;

        float calculatedAmount = CalculateAmount(percentage);
        float expectedAmount = calculatedAmount >= filler.fillAmount ? calculatedAmount : 1f;
        isLevelUp = expectedAmount == 1f ? true : false;


        currentAmount = filler.fillAmount;
        while (currentAmount < expectedAmount)
        {
            filler.fillAmount = currentAmount + fillAmount;

            currentAmount = filler.fillAmount;
            yield return new WaitForSeconds(yieldTime);
        }
        if (isLevelUp)
        {
            StartCoroutine(LevelUp(percentage));
        }
        else
        {
            isAnimationOn = false;
        }
    }

    private IEnumerator LevelUp(int percentage)
    {
        anim.SetTrigger("IsLevelUp");
        PlayerData.instance.SetPlayerLevelUp(1);

        yield return new WaitForSeconds(0.5f);
        UpdateIndicator(binder.GetExperiencePoints().level);

        yield return new WaitForSeconds(2.5f);
        filler.fillAmount = startValue;

        if (mapCheck == MapCheck.Enabled)
        {
            if (stateController != null)
            {
                stateController.CheckStatesAgain();
            }
        }

        StartCoroutine(AnimateFill(percentage));
    }
}
