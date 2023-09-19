using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Grabbable : MonoBehaviour
{
    [SerializeField] private string animName;
    private SphereCollider grabCollider;

}
