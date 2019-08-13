using UnityEngine;

namespace ScrollItemData
{
    [System.Serializable]
    public struct BodyItemData
    {
        public BasicItemData basicData;
        public CheckableItemData checkableData;
        public PayableItemData payableData;
        public Sprite frame_1;
        public Sprite frame_2;
        public Sprite frame_3;
    }
}