using System.Collections;
using System.Collections.Generic;
using RacTools.Variables;
using UnityEngine;

public class Bootstrapper
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Execute()
    {
        Debug.Log("Execute Bootstrapper");
    }
}
