using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private string startSound;
    private void OnEnable()
    {
        /*if (startSound == null || startSound == "") return;
        AudioManager.Instance.PlaySound3D(startSound, transform.position);*/
    }
    public void Play3DSound(string soundName)
    {
        AudioManager.Instance.PlaySound3D(soundName, transform.position);
    }

    public void Play2DSound(string soundName)
    {
        AudioManager.Instance.PlaySound2D(soundName);
    }
}
