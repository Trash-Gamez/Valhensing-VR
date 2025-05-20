using System;
using UnityEngine;

public class Rail : MonoBehaviour
{
    [SerializeField] private Transform[] railPoints;
    private void OnValidate()
    {
        railPoints = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            railPoints[i] = transform.GetChild(i);
        }
    }
}
