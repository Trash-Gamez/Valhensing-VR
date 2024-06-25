using UnityEngine;

namespace _VanHelsingVR.Events
{
    [CreateAssetMenu(order = 0,fileName = "Int Reactive Event", menuName = "Events/Reactive/Int Reactive Event")]
    public sealed class IntReactiveEvent : ReactiveEvent<int>{}
}