using UnityEngine;

namespace ScrollItemData
{
    [System.Serializable]
    public struct ExtraItemData
    {
        public BasicItemData basicData;
        public CheckableItemData checkableData;
        public PayableItemData payableData;
        public Sprite extra;
    }
}