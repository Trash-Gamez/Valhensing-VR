using UnityEngine;

namespace _VanHelsingVR.Health
{
    [RequireComponent(typeof(DamageSystem))]
    [DisallowMultipleComponent]
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] private DamageSystem damageSystem;

        private void Start()
        {
            if (!damageSystem) {damageSystem = GetComponent<DamageSystem>();}
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!damageSystem){damageSystem = GetComponent<DamageSystem>();}
        }
#endif
    }
}
