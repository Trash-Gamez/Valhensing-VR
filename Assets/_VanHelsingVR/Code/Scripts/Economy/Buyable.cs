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
        public Mesh Mesh;
        public Sprite Sprite;

        [TextArea] public string Description;
    }
}
