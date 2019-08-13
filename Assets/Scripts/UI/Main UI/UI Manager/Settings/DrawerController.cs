#pragma warning disable 649

using System.Collections;
using UnityEngine;

public class DrawerController : MonoBehaviour
{
    [SerializeField] private Animator icon;

    [Header("Drawer Properties")]
    [SerializeField] private RectTransform drawer;
    [SerializeField] private float closePosition;
    [SerializeField] private float openPosition;
    [SerializeField] private DrawerState state = DrawerState.Close;

    [Header("Drawer Animation")]
    [SerializeField] private float drawerYieldTime;
    [SerializeField] private float translationFactor;

    #region Button Method
    public void DrawerAction()
    {
        switch (state)
        {
            case DrawerState.Open:
                StartClose();
                break;
            case DrawerState.Close:
                StartOpen();
                break;
            default:
                throw new System.Exception("Drawer state is unknown!");
        }
    }
    #endregion

    private void StartOpen()
    {
        StartCoroutine(AnimateDrawer(closePosition, openPosition));
        state = DrawerState.Open;
        icon.SetTrigger("IsOpen");
    }

    private void StartClose()
    {
        StartCoroutine(AnimateDrawer(openPosition, closePosition));
        state = DrawerState.Close;
        icon.SetTrigger("IsClose");
    }

    private IEnumerator AnimateDrawer(float start, float finish)
    {
        float reposition = translationFactor;
        if (start > finish)
        {
            //Opening
            reposition *= -1;
            drawer.anchoredPosition = new Vector2(0f, closePosition);
        } else
        {
            reposition *= 1;
            drawer.anchoredPosition = new Vector2(0f, openPosition);
        }
        float posY = drawer.anchoredPosition.y;

        //TAKE A LOOK AT HERE AFTER!!!
        while (!(posY >= (finish - 0.1f) && posY <= (finish + 0.1f)))
        {
            drawer.anchoredPosition += new Vector2(0f, reposition);
            posY = drawer.anchoredPosition.y;
            yield return new WaitForSeconds(drawerYieldTime);
        }
    }
}
