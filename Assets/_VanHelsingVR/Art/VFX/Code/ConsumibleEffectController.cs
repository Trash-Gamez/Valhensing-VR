using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumibleEffectController : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;

    public void DestroyEffect(string sound)
    {
        AudioManager.Instance.PlaySound3D(sound, transform.position);
        GameObject vfx = Instantiate(destroyVFX);
        vfx.transform.position = transform.position;
        Destroy(vfx, 5);
    }
}
