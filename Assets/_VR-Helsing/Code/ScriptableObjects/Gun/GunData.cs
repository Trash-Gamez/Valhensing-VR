using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/Gun/GunData")]
public class GunData : ScriptableObject
{
    [field: SerializeField] public GunConfig Config { get; private set; }
    [field: SerializeField] public int indexMesh { get; private set; }
}
