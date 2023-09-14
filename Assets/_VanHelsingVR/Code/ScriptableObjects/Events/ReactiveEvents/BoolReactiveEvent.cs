using UnityEngine;

namespace _VanHelsingVR.Events
{
    [CreateAssetMenu(order = 0,fileName = "Bool Reactive Event", menuName = "Events/Reactive/Bool Reactive Event")]
    public sealed class BoolReactiveEvent : ReactiveEvent<bool>{}
}