using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;

namespace _VanHelsingVR.Economy
{
    public class ShopItem : XRSimpleInteractable
    {
        public Buyable Buyable => buyable;
        [Title("SHOPT ITEM")]
        [SerializeField] private Buyable buyable = null;

        [field: SerializeField]
        public byte stock = 255;

        protected override void Awake()
        {
            SetSkins();
            base.Awake();
        }

        [ContextMenu("Set Skins")]
        private void SetSkins()
        {
            var previewSkin = gameObject.GetNamedChild("Skin");
            if (previewSkin != null)
            {
                DestroyImmediate(previewSkin);
            }
            previewSkin = Instantiate(buyable.ObjectPreview, transform);
            previewSkin.name = "Skin";
        }
        
        //TOOD: MAKE A BUTTON OF THIS

    }
}
