using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    public Animator animator;
   


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("Close", true);
            AudioManager.Instance.PlaySound2D("BigDoor");
        }
    }
}
