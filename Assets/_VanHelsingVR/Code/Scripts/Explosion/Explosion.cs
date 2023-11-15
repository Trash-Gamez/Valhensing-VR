using System;
using System.Collections.Generic;
using UnityEngine;
using _VanHelsingVR.Health;
using Cysharp.Threading.Tasks;
using RacTools.Miscelaneous;
using Sirenix.OdinInspector;
using UnityEditor;
using Range = RacTools.Utilities.Range;

namespace _VanHelsingVR.Explosion
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private float initialRadius;
        [SerializeField] private float maxRadius;
        [SerializeField, Min(0.25f)] private float explosionSeconds;
        [SerializeField] private LayerMask explosionLayer;

        private float _currentRadius;

        public async UniTask<List<Health.Health>> DoExplosionAsync(float seconds)
        {
            var healthTouched = new List<Health.Health>();
            
            var waitSecondsRange = new Range(seconds, 0);
            var radiusRange = new Range(maxRadius, initialRadius);
            var transcurredTime = 0f;
            
            var task = UniTask.WaitUntil(() =>
            {
                transcurredTime += Time.deltaTime;
                return transcurredTime >= seconds;
            });

            while (task.Status == UniTaskStatus.Pending)
            {
                _currentRadius = Map.MapFloatRange(transcurredTime, waitSecondsRange, radiusRange);
                SphereCastHealth(healthTouched);

                await UniTask.WaitForFixedUpdate();
            }
            
            Debug.Log("Termino la explosion");
            _currentRadius = 0f;
            return healthTouched;
        }

        private void SphereCastHealth(List<Health.Health> healthTouched)
        {
            var results = new Collider[5];
            var size = Physics.OverlapSphereNonAlloc(transform.position, _currentRadius, results, explosionLayer);
            for (int i = 0; i < size; i++)
            {
                var collider = results[i];
                if (!collider.TryGetComponent<Hurtbox>(out var hurtbox)) continue;
                var health = hurtbox.HealthReference.HealthSystem;
                if (health == null) continue;
                if (healthTouched.Contains(health)) continue;
                
                hurtbox.ForceDeath();
                healthTouched.Add(health);
            }
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
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, initialRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, maxRadius);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, _currentRadius);
        }
        #endif
        #endregion
    }
}
