using UnityEngine;
using RacTools.Variables;

namespace _VanHelsingVR.Economy 
{
    [CreateAssetMenu(menuName = "Economy/Economy System", fileName = "_Economy")]
    public class EconomySystem : ScriptableObject
    {
        [SerializeField] private Variable<int> currency;

        private void AddCurrency(int addedCurrency)
        {
            currency.Value += addedCurrency;
        }

        public void BuyItem(Buyable buyable)
        {
            currency.Value -= buyable.Price;
        }

        // returns true if can buy item
        public bool CanAfford(Buyable buyable)
        {
            //Discounts and other stuff
            return currency.Value >= buyable.Price;
        }
    }
}
