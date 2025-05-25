using UnityEngine;
using UnityEngine.Events;

public class Doors:MonoBehaviour
{
    public Animator anim;
    public int Keys = 0;
    public string AudioName;

    public UnityEvent AbrirPuerta;
    public void OpenDoor()
    {
        Keys++;
        if (Keys == 2)
        {
            //AudioManager.Instance.PlaySound2D(AudioName);
            anim.Play("Open");
            AbrirPuerta.Invoke();
        }
    }
}
