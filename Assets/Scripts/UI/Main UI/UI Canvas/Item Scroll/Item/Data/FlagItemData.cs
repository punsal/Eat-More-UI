using UnityEngine;

namespace ScrollItemData
{
    [System.Serializable]
    public struct FlagItemData
    {
        public BasicItemData basicData;
        public AnimatableItemData animatableData;
        public CheckableItemData checkableData;
    }
}