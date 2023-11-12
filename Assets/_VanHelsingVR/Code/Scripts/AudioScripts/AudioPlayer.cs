using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public void Play3DSound(string soundName)
    {
        AudioManager.Instance.PlaySound3D(soundName, transform.position);
    }
}
