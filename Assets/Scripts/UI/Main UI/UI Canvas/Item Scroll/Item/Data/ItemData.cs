using UnityEngine;

namespace ScrollItemData
{
    [System.Serializable]
    public struct ItemData
    {
        public ItemType type;
        public MapItemData mapData;
        public BodyItemData bodyData;
        public ExtraItemData extraData;
        public SpecialItemData specialData;
        public FlagItemData flagData;
    }
}