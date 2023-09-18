using UnityEngine;

namespace _VanHelsingVR.Events
{
    [CreateAssetMenu(order = 0,fileName = "String Reactive Event", menuName = "Events/Reactive/String Reactive Event")]
    public sealed class StringReactiveEvent : ReactiveEvent<string>{}
}