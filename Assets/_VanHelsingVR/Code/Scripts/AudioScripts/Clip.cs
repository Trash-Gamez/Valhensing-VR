
using UnityEngine;

[CreateAssetMenu(fileName = "AudioClip", menuName ="ScriptableObjects/AudioClip",order = 1)]
public class Clip : ScriptableObject
{
    public AudioClip clip;
    [Range(0, 1)]
    public float volume = 1;
}
