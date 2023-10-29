using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RacTools.Variables;
public class Watch : MonoBehaviour
{
    [SerializeField] Material bloodMaterial;
    [SerializeField] Material soulMaterial;

    [SerializeField] VariableReference<int> health;
}
