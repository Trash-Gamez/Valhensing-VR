using UnityEngine;

namespace _VanHelsingVR.Economy
{
    [CreateAssetMenu(menuName = "Economy/Buyable Item", order = 1, fileName = "Buy_Item")]
    public sealed class Buyable : ScriptableObject
    {
        public int id;
        public string itemName;
        public float price;

        [TextArea] public string description;
    }
}
