using _VanHelsingVR.Interaction;
using UnityEngine;
using UnityEngine.Events;

public class Button : Hittable
{
    [SerializeField] UnityEvent OnPress;
    public override void OnDamage()
    {
        OnPress.Invoke();
        Destroy(gameObject);
    }
}
