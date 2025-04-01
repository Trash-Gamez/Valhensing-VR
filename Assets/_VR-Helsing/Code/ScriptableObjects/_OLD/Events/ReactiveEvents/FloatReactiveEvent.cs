using UnityEngine;

namespace _VanHelsingVR.Events
{
    [CreateAssetMenu(order = 0,fileName = "Float Reactive Event", menuName = "Events/Reactive/Float Reactive Event")]
    public sealed class FloatReactiveEvent : ReactiveEvent<float>{}
}