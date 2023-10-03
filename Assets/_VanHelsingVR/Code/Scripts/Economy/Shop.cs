using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _VanHelsingVR.Economy
{
    public class Shop : MonoBehaviour
    {
        /*
         * TODO: MAKE THE SHOP HANDLE EVERY *BUYABLE* AND BE THE SORTER
         * AND THE *RESTOCKER* OF  EVERY ITEM
         *
        */
        //[SerializeField] private 
        [SerializeField] private List<ShopItem> shopItems = new List<ShopItem>();

        [SerializeField] private EconomySystem economySystem;

        [SerializeField] private Transform positionToSpawn;

        public void BuyItem(ShopItem itemToBuy)
        {
            var buyable = itemToBuy.Buyable;
            if (!economySystem.CanAfford(buyable)) return;
            
            economySystem.BuyItem(buyable);
            AppearItem(buyable);
        }

        private void AppearItem(Buyable buyableToAppear)
        {
            //TODO: Make logic for composite pattern
            // USE UNITAKS FOR APPEARING THE OBJECT
            Instantiate(buyableToAppear.ObjectBuyable, positionToSpawn.position, Quaternion.identity);
        }
    }
}
