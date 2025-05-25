using System;
using System.Collections.Generic;
using _VanHelsingVR.Explosion;
using _VanHelsingVR.Health;
using UnityEngine;

public class BombHitbox : Hitbox
{
    private static readonly Collider[] _Colliders = new Collider[5];

    [Header("Bomb params")] 
    [SerializeField] private List<Collider> ignoreColliders;
    [SerializeField] private Explosion explosion;
    [SerializeField] private SphereCollider sphereCollider;

    [SerializeField] private LayerMask bombLayer;

    private bool _hasExploded;
    
    private void FixedUpdate()
    {
        if (_hasExploded) return;
        
        var scale = transform.lossyScale;
        var scaleFactor = Mathf.Max(scale.x, scale.y, scale.z);
        var hits = Physics.OverlapSphereNonAlloc(sphereCollider.transform.position + sphereCollider.center, sphereCollider.radius * scaleFactor, _Colliders, bombLayer);

        for (int i = 0; i < hits; i++)
        {
            if(ignoreColliders.Contains(_Colliders[i])) continue;
            if(_Colliders[i].transform.root.CompareTag("Player")) continue;
            
            explosion.DoExplosion();
            _hasExploded = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        var scale = transform.lossyScale;
        var scaleFactor = Mathf.Max(scale.x, scale.y, scale.z);
        Gizmos.DrawWireSphere(sphereCollider.transform.position + sphereCollider.center, sphereCollider.radius * scaleFactor);
    }
}
