using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using _VanHelsingVR.Health;
using Cysharp.Threading.Tasks;
using RacTools.Miscelaneous;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine.Events;
using Range = RacTools.Utilities.Range;

namespace _VanHelsingVR.Explosion
{
    public class Explosion : MonoBehaviour
    {
        [Title("Params")]
        [SerializeField] private float initialRadius;
        [SerializeField] private float maxRadius;
        [SerializeField] private LayerMask explosionLayer;
        [SerializeField, Min(0.25f)] private float explosionSeconds;
        [SerializeField] private List<Collider> ignoreColliders;

        [Title("Damage")] 
        [SerializeField] private bool instantDeath = true;
        [SerializeField, HideIf(nameof(instantDeath))] private int damageValue;
        
        [Title("Events")]
        [SerializeField] private UnityEvent onExplosionStarted;
        [SerializeField] private UnityEvent onExplosion;
        
        #if UNITY_EDITOR
        public bool DrawSphereExplosionGizmos;
        public bool DrawSphereRadiusGizmos;
        #endif

        private float _currentRadius;

        private CancellationTokenSource _cancellationTokenSource = null;

        public async void DoExplosion(float seconds)
        {
            if(!_cancellationTokenSource.IsCancellationRequested)
                _cancellationTokenSource.Cancel();
            try
            {
                await DoExplosionAsync(seconds);
            }
            catch (OperationCanceledException exception)
            {
                Debug.Log("SE CANCELO LA OPERACION DE LA EXPLOSION");
            }
        }

        public async UniTask<List<Health.Health>> DoExplosionAsync(float seconds)
        {
            var healthTouched = new List<Health.Health>();
            
            var waitSecondsRange = new Range(seconds, 0);
            var radiusRange = new Range(maxRadius, initialRadius);
            var transcurredTime = 0f;
            
            var task = UniTask.WaitUntil(() =>
            {
                transcurredTime += Time.fixedDeltaTime;
                return transcurredTime >= seconds;
            }, cancellationToken: _cancellationTokenSource.Token);
            
            onExplosionStarted?.Invoke();

            while (task.Status == UniTaskStatus.Pending)
            {
                _currentRadius = Map.MapFloatRange(transcurredTime, waitSecondsRange, radiusRange);
                SphereCastHealth(healthTouched);

                await UniTask.WaitForFixedUpdate();
            }
            
            Debug.Log("Termino la explosion");
            onExplosion?.Invoke();
            
            _currentRadius = 0f;
            return healthTouched;
        }

        private void SphereCastHealth(List<Health.Health> healthTouched)
        {
            var results = new Collider[5];
            var size = Physics.OverlapSphereNonAlloc(transform.position, _currentRadius, results, explosionLayer, QueryTriggerInteraction.Collide);
            for (int i = 0; i < size; i++)
            {
                var collider = results[i];
                if(ignoreColliders.Contains(collider)) continue;
                if(!collider.isTrigger) continue;
                if (!collider.TryGetComponent<Hurtbox>(out var hurtbox)) continue;
                var health = hurtbox.HealthReference.HealthSystem;
                if (health == null) continue;
                if (healthTouched.Contains(health)) continue;
                
                if(instantDeath)
                    hurtbox.ForceDeath();
                else
                    hurtbox.Hit(damageValue);
                healthTouched.Add(health);
            }
        }

        private void OnDisable()
        {
            if(!_cancellationTokenSource.IsCancellationRequested)
                _cancellationTokenSource.Cancel();
        }

        #region Unity Editor
        #if UNITY_EDITOR
        [Button("Test Explosion", ButtonSizes.Medium)]
        private async void TestExplosion()
        {
            await DoExplosionAsync(explosionSeconds);
        }

        private void OnValidate()
        {
            if (maxRadius < initialRadius)
            {
                maxRadius = initialRadius;
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (!DrawSphereRadiusGizmos) return;
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, initialRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, maxRadius);
        }

        private void OnDrawGizmos()
        {
            if (!DrawSphereExplosionGizmos) return;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, _currentRadius);
        }
        #endif
        #endregion
    }
}
