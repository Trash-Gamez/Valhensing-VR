using _VanHelsingVR.Interaction;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
    [SerializeField] private LayerMask hitLayer;
    private void OnTriggerEnter(Collider other)
    {
        if (hitLayer.value == other.gameObject.layer) return;
        if (!other.gameObject.TryGetComponent<Damagable>(out var damagable)) return;
        damagable.OnDamage();
    }
}
