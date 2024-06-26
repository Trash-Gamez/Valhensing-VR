using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAnimation : MonoBehaviour
{
    public string animName;
   private void OnEnable()
    {
        gameObject.GetComponent<Animator>().Play(animName);
    }
}
