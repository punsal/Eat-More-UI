using UnityEngine;

namespace ScrollItemData
{
    [System.Serializable]
    public struct MapItemData
    {
        public BasicItemData basicData;
        public AnimatableItemData animatableData;
        public Sprite locked;
    }
}