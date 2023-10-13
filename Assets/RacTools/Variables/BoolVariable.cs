using UnityEngine;

namespace RacTools.Variables
{
    [CreateAssetMenu(order = -1, fileName = "BoolVariable", menuName = "GameVariable/Bool")]
    public sealed class BoolVariable : Variable<bool> {}
}