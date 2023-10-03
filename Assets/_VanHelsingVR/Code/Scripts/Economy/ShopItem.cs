using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModestTree;

namespace _VanHelsingVR.Economy
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private Buyable buyable = null;

        [field: SerializeField]
        public byte stock = 255;

        private void Awake()
        {
            SetSkins();
        }

        [ContextMenu("Set Skins")]
        private void SetSkins()
        {
            if (TryGetComponent<MeshFilter>(out var filter))
            {
                if (buyable.Mesh == null)
                    Debug.LogError($"No existe una malla 3D asignada al scriptable: {buyable.name}", buyable);
                else
                    filter.sharedMesh = buyable.Mesh;
            }

            if (TryGetComponent<SpriteRenderer>(out var renderer))
            {
                if (buyable.Sprite == null)
                    Debug.LogError($"No existe una Sprite asignada al scriptable: {buyable.name}", buyable);
                else
                    renderer.sprite = buyable.Sprite;
            }
        }



        //TODO: Make instantiate of the buyable gameobject
#if UNITY_EDITOR
        private void OnValidate()
        {

        }
#endif

    }
}
