using System.Collections;
using UnityEngine;
using UIUtility;

public class ShopButtonTranslationController : ShopButtonBehaviourController
{
    [Header("Position Change")]
    [SerializeField] private int idlePosition = 32;
    [SerializeField] private int onClickPosition = 0;
    [Header("Animation")]
    [SerializeField] private float translationFactor = 0.5f;
    [SerializeField] private float yieldTime = 0.001f;

    private RectTransform rect;
    private WaitForSeconds waitTime;
    private float tempTranslationFactor;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        waitTime = new WaitForSeconds(yieldTime);
        IdleAction();
    }

    private void Update()
    {
        base.ButtonBehaviour();
    }

    public override void IdleAction()
    {
        //rect.anchoredPosition = rect.anchoredPosition.ModifyV2(idlePosition);
        StartCoroutine(AnimateTranslation(new Vector2(idlePosition, rect.anchoredPosition.y)));
    }

    public override void OnClickAction()
    {
        //rect.anchoredPosition = rect.anchoredPosition.ModifyV2(onClickPosition);
        StartCoroutine(AnimateTranslation(new Vector2(onClickPosition, rect.anchoredPosition.y)));
    }

    private IEnumerator AnimateTranslation(Vector2 desiredPosition)
    {
        if (desiredPosition.x >= rect.anchoredPosition.x)
        {
            //animate from left to right
            //increase
            tempTranslationFactor = translationFactor;
        }
        else
        {
            //animate from right to left
            //decrease
            tempTranslationFactor = -1f * translationFactor;
        }

        while (true)
        {
            if (rect.anchoredPosition.x <= desiredPosition.x + translationFactor &&
                rect.anchoredPosition.x > desiredPosition.x - translationFactor)
            {
                rect.anchoredPosition = rect.anchoredPosition.ModifyV2(desiredPosition.x);
                break;
            }
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x + tempTranslationFactor, rect.anchoredPosition.y);
            yield return waitTime;
        }

    }
}
