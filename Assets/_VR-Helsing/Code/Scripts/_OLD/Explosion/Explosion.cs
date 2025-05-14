
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using _VanHelsingVR.Health;
using _VanHelsingVR.Utilities;
using _VR_Helsing.HealthSystem;
using Cysharp.Threading.Tasks;

using Sirenix.OdinInspector;
using UnityEngine.Events;
using Range = RacTools.Utils.Range;

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

        //private CancellationTokenSource _cancellationTokenSource = null;

        public void DoExplosion()
        {
            DoExplosionAsync(explosionSeconds, CancellationToken.None);
        }
        
        public void DoExplosion(float seconds, CancellationToken token)
        { 
            DoExplosionAsync(seconds, token);
        }

        public void DoExplosion(float seconds)
        {
            DoExplosion(seconds, CancellationToken.None);
        }

        public async UniTask<List<HealthSystem>> DoExplosionAsync(float seconds,CancellationToken token)
        {
            var healthTouched = new List<HealthSystem>();
            
            var waitSecondsRange = new Range(seconds, 0);
            var radiusRange = new Range(maxRadius, initialRadius);
            var transcurredTime = 0f;
            
            onExplosionStarted?.Invoke();
            
            var task = UniTask.WaitUntil(() =>
            {
                transcurredTime += Time.fixedDeltaTime;
                
                return transcurredTime >= seconds;
            }, cancellationToken: token);
            

            while (task.Status == UniTaskStatus.Pending)
            {
                _currentRadius = UtilitieExtensions.Map(transcurredTime, waitSecondsRange, radiusRange);
                SphereCastHealth(healthTouched);

                await UniTask.WaitForFixedUpdate(cancellationToken: token);
            }
            
            Debug.Log("Termino la explosion");
            onExplosion?.Invoke();
            
            _currentRadius = 0f;
            return healthTouched;
        }

        private void SphereCastHealth(List<HealthSystem> healthTouched)
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

        #region Unity Editor
        #if UNITY_EDITOR
        [Button("Test Explosion", ButtonSizes.Medium)]
        private async void TestExplosion()
        {
            await DoExplosionAsync(explosionSeconds, CancellationToken.None);
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
