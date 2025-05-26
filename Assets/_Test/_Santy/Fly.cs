using UnityEngine;

namespace _VR_Helsing.Enemy
{
    public class Fly : FlyingEnemy
    {
        [Header("Fly Settings")]
        [Range(0, 20)] public float explotionRange = 2.5f;
        private bool isChasing = false;

        private void Update()
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= explotionRange)
            {
                Explode();
                return;
            }

            if (distance <= detectionRange)
            {
                isChasing = true;
                DetectTarget();
                ChasePlayer();
            }
            else
            {
                isChasing = false;
                Movement();
            }
            DebugPath();
        }

        private void ChasePlayer()
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        private void Explode()
        {
        }

        public override void Movement()
        {
            base.Movement();
        }

        public override void DetectTarget()
        {
            base.DetectTarget();
        }

        private void DebugPath()
        {
            if (currentWayPoint)
            {
                Debug.DrawLine(transform.position, currentWayPoint.position, Color.green);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (!drawGizmos) return;
            base.OnDrawGizmosSelected();
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explotionRange);
        }
    }
}
