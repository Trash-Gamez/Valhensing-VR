using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public GameObject damagePanel;
    public void StartDamageCoroutine()
    {
        StartCoroutine(DamageCoroutine());
    }

    IEnumerator DamageCoroutine()
    {
        damagePanel.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        damagePanel.SetActive(false);
    }
}
