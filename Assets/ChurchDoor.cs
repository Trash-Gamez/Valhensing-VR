using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChurchDoor : MonoBehaviour
{
    public Animator anim;
    public int Keys=0;
    public void OpenDoor()
    {
        Keys++;
        if(Keys == 3)
        {
            anim.Play("Open");
        }
    }
}
