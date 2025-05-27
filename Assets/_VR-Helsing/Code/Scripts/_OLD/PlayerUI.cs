using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject damagePanel;
    [SerializeField] private GameObject deadPanel;

    [SerializeField] private Transform deadZoneTransform;
    [SerializeField] private Transform playerTransform;

    [SerializeField] UnityEvent OnPlayerTeleport;
    [SerializeField] private Image FadePanel;
    [SerializeField] private float timeToFade=10f;
    [SerializeField] private float damageTime= 0.4f;
    public void StartDamageCoroutine()
    {
        StartCoroutine(DamageCoroutine());
    }

    public void StartDeadCoroutine()
    {
        StartCoroutine(DeadCoroutine());
    }

    IEnumerator DamageCoroutine()
    {
        damagePanel.SetActive(true);
        yield return new WaitForSeconds(damageTime);
        damagePanel.SetActive(false);
    }

    IEnumerator DeadCoroutine()
    {
        deadPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        float timeElapsed = 0;
        while (timeElapsed < timeToFade)
        {
            FadePanel.color = new Color(0,0,0, Mathf.Lerp(0, 1, timeElapsed / timeToFade));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        //playerTransform.position = deadZoneTransform.position;
        yield return new WaitForSeconds(5f);
        OnPlayerTeleport.Invoke();
        deadPanel.SetActive(false);
        timeElapsed = 0;
        while (timeElapsed < timeToFade)
        {
            FadePanel.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, timeElapsed / timeToFade));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
