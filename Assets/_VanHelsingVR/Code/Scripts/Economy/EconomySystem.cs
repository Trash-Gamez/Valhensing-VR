using UnityEngine;

using _VanHelsingVR.Variables;

namespace _VanHelsingVR.Economy 
{
    [CreateAssetMenu(menuName = "Systems/Economy", fileName = "_Economy")]
    public class EconomySystem : ScriptableObject
    {
        [SerializeField] private Variable<int> currency;

        private void AddCurrency(int addedCurrency)
        {
            currency.Value += addedCurrency;
        }

        public void BuyItem(/*BuyableItem buyable*/)
        {

        }
    }
}
