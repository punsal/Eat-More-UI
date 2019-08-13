using UnityEngine;

namespace ScrollItemData
{
    [System.Serializable]
    public enum ItemState { Locked, Unlocked }

    [System.Serializable]
    public struct BasicItemData
    {
        public Sprite icon;
        public int requirement;
        public ItemState state;
    }

    [System.Serializable]
    public struct AnimatableItemData
    {
        public Sprite animation;
        public Sprite background;
    }

    [System.Serializable]
    public struct CheckableItemData
    {
        public Sprite check;
    }

    [System.Serializable]
    public enum ShopItemState { Locked, Unlocked }

    [System.Serializable]
    public struct PayableItemData
    {
        public ShopItemState state;
    }
}