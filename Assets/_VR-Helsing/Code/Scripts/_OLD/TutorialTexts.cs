using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialTexts : MonoBehaviour
{
    [SerializeField] string Text;
    [SerializeField] TextMeshProUGUI textCamp;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textCamp.text = Text;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textCamp.text = "";
        }
    }
}
