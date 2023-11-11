using Sirenix.OdinInspector;
using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

namespace _VanHelsingVR.Economy
{
    [RequireComponent(typeof(XRPokeFilter))]
    public class ShopItem : XRSimpleInteractable
    {
        [Title("SHOPT ITEM")]
        [SerializeField] private Buyable buyable = null;
        public Buyable Buyable => buyable;

        [field: SerializeField]
        public byte stock = 255;

        private Shop _shop;

        public void InitItem(Shop shopFrom)
        {
            _shop = shopFrom;
        }

        protected override void Awake()
        {
            SetSkins();
            base.Awake();
        }

        [ContextMenu("Set Skins")]
        private void SetSkins()
        {
            //TODO: SO BETTER SKINS SYSTEM
            return;
            var previewSkin = gameObject.GetNamedChild("Skin");
            if (previewSkin != null)
            {
                DestroyImmediate(previewSkin);
            }
            previewSkin = Instantiate(buyable.ObjectPreview, transform);
            previewSkin.name = "Skin";
        }

        private void BuyItem()
        {
            _shop.BuyItem(this);
        }
        
        //bUTTON gETS pRESSED
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            BuyItem();
        }
    }
}
