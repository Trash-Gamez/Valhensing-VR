using Sirenix.OdinInspector;
using UnityEngine;

namespace _VanHelsingVR.Economy
{
    [CreateAssetMenu(menuName = "Economy/Buyable Item", order = 1, fileName = "Buy_Item")]
    public sealed class Buyable : ScriptableObject
    {
        public int ID;
        public string ItemName;
        public int Price;

        // SKINS
        [AssetsOnly]
        public GameObject ObjectPreview;

        public GameObject ObjectBuyable;

        [TextArea] public string Description;
    }
}
