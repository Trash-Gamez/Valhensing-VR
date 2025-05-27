using UnityEngine;

namespace _VR_Helsing.Enemy
{
    public class FlyDoc : FlyingEnemy
    {
        [Header("FlyDoc Settings")]
        [SerializeField][Range(0,10)] private float bulletSpeed = 1f;
        [SerializeField] private float timeBetweenShots = 2f;
        private float lastShootTime = 0f;

        [Header("Bullet Settings")]
        [SerializeField] private Transform firePoint;
        [SerializeField] private PoolManager bulletPool;

        private void Update()
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= detectionRange)
            {
                DetectTarget();
            }
            else
            {
                animator.SetBool("Attack", false);
                Movement();
            }
            DebugPath();
        }

        public override void Movement()
        {
            animator.SetBool("Fly", true);
            base.Movement();
        }

        public override void DetectTarget()
        {
            animator.SetBool("Attack", true);

            if(Time.time - lastShootTime >= timeBetweenShots)
            {
                lastShootTime = Time.time;
                Shoot();
            }

            base.DetectTarget();
        }

        private void Shoot()
        {
            if (bulletPool == null) return;
            GameObject bullet = bulletPool.GetObject(firePoint.position);
            bullet.transform.rotation = firePoint.rotation;
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = firePoint.forward * bulletSpeed;
            }
        }

        private void DebugPath()
        {
            if (currentWayPoint)
            {
                Debug.DrawLine(transform.position, currentWayPoint.position, Color.green);
            }
        }
    }
}
