using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogMessage : MonoBehaviour
{
    public void Message(string message)
    {
        Debug.Log(message, gameObject);
    }
}
