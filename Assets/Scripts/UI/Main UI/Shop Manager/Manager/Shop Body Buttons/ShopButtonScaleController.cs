using UnityEngine;
using UIUtility;

public class ShopButtonScaleController : ShopButtonBehaviourController
{
    [Header("Scale Change")]
    [SerializeField] private float idleScale = 0.65f;
    [SerializeField] private float onClickScale = 0.75f;

    private RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        IdleAction();
    }

    private void Update()
    {
        base.ButtonBehaviour();
    }
    public override void IdleAction()
    {
        rect.localScale = rect.localScale.ModifyV3(idleScale, idleScale, idleScale);
    }

    public override void OnClickAction()
    {
        rect.localScale = rect.localScale.ModifyV3(onClickScale, onClickScale, onClickScale);
    }
}
