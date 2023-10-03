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

        /*
        private void Awake()
        {
            foreach (Products p in products)
            {
                groupDictionary.Add(p.name, p.product);
            }
        }

        public GameObject GetProductFromName(string name)
        {
            if (groupDictionary.ContainsKey(name))
            {
                GameObject pro = groupDictionary[name];
                return pro;
            }
            return null;
        }
        */

        public void BuyItem(ShopItem itemToBuy)
        {

        }
    }
}
