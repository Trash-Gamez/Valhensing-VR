using System.Collections;
using Autohand;
using UnityEngine;


namespace _VR_Helsing.Gun
{
    public class GunActivatorCollider : MonoBehaviour
    {
        [SerializeField] private NEW_GunHolder gunHolder;
        [SerializeField] private string handTag;

        private bool _canDetect = false;

        private IEnumerator Start()
        {
            _canDetect = false;
            yield return new WaitForSeconds(0.1f); //Wait Until player ready:
            _canDetect = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_canDetect) return;
            if (!other.CompareTag(handTag)) return;
            if (!other.TryGetComponent(out Hand hand)) return;
            
            gunHolder.AlterGun(hand);
        }
    }
}
