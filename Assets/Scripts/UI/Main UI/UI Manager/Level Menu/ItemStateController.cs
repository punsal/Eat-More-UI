using System;
using UnityEngine;
using ScrollItemData;

public class ItemStateController : MonoBehaviour
{
    public bool isConnected = true;

    #region Components
    private UIDataBinder binder;
    private ItemScrollManager manager;
    private SnapManager snap;
    #endregion

    private Item item;

    private void Start()
    {
        binder = GetComponentInParent<UIDataBinder>();

        manager = GetComponent<ItemScrollManager>();
        snap = GetComponent<SnapManager>();

        ControlState();
    }

    private void ControlState()
    {
        if (isConnected)
        {
            Item[] items = manager.GetItems();
            int level = binder.GetExperiencePoints().level;
            foreach (var item in items)
            {
                int itemLevel = item.Data.mapData.basicData.requirement;

                if (itemLevel <= level)
                {
                    item.SetItemState(ItemState.Unlocked);
                    item.ApplyIconVisual();
                } else
                {
                    item.SetItemState(ItemState.Locked);
                    item.ApplyIconVisual();
                }
            }
        }
    }

    public void CheckStatesAgain() { ControlState(); }

    public void SetMapItemState(ItemState state)
    {
        item = manager.GetItems()[snap.SnapIndex];
        item.SetItemState(state);
        item.ApplyIconVisual();
    }
}
