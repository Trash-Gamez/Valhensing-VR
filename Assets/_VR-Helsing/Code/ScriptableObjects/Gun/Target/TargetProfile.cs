using UnityEngine;

namespace _VR_Helsing.Gun.Target
{
    [CreateAssetMenu(fileName = "TargetProfile", menuName = "Trash Gamez/Targeting/Target Profile", order = 0)]
    public class TargetProfile : ScriptableObject
    {
        [field: SerializeField] public TargetType TargetType { get; private set; }
        [field: SerializeField] public Color CrosshairColor { get; private set; }
        [field: SerializeField] public string AnimationState { get; private set; }
        [field: SerializeField] public bool IsHostile { get; private set; }
        
        //No se si esto encaje, quizás para secretos?, objetos que sean objetivos pero no se vean en el crosshair?
        [field: SerializeField] public bool IsInteractable { get; private set; }
    }
}