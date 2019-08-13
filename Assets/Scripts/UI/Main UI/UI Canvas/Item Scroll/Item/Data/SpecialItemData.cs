using UnityEngine;

namespace ScrollItemData
{
    [System.Serializable]
    public struct SpecialItemData
    {
        public BasicItemData basicData;
        public CheckableItemData checkableData;
        public PayableItemData payableData;
        public Sprite specialFrame_1;
        public Sprite specialFrame_2;
        public Sprite specialFrame_3;
        public GameObject special;
    }
}