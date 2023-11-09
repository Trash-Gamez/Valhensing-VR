using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play3DSound : MonoBehaviour
{
    public void Play(string soundName)
    {
        AudioManager.Instance.PlaySound3D(soundName, transform.position);
    }

    public void PlayMusic(string soundName)
    {

        StartCoroutine(AudioManager.Instance.CrossFadeMusic(soundName, 1));
    }
}
