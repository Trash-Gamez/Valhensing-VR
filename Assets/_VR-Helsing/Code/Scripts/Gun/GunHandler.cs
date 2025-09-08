using UnityEngine;

namespace _VR_Helsing.Gun
{
    /// <summary>
    /// The main class that handles all Gun Logic
    /// </summary>
    public class GunHandler : MonoBehaviour
    {
        [Header("Gun Settings")] 
        [SerializeField] private GunDataHandler dataHandler;
        
        [Header("Gun Properties")]
        [SerializeField] private Rigidbody gunRigidbody;
        [SerializeField] private Animator gunAnimator;
        [SerializeField] private Transform shootPoint;
        
        void Update()
        {
            
        }
    }
}
