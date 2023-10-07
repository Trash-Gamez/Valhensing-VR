using UnityEngine;
using UnityEngine.Events;

namespace _VanHelsingVR.Interaction
{
    public class DamagableButton : Hittable
    {
        [SerializeField] UnityEvent OnPress;
        public override void OnDamage()
        {
            OnPress.Invoke();
            Destroy(gameObject);
        }
    }
}
