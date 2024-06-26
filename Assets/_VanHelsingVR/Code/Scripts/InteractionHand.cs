using _VanHelsingVR.Animation;
using UnityEngine;

namespace _VanHelsingVR
{
    public class InteractionHand : MonoBehaviour
    {
        private HandEffectsController _handEffectsController;
        public string nombreAnimacion;
    
        void Start()
        {
            _handEffectsController = FindObjectOfType<HandEffectsController>();
        }

        public void LlamarAnimacion()
        {
            _handEffectsController.Grap(nombreAnimacion);
        }

    
    }
}
