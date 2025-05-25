using _VanHelsingVR.Enemy;
using UnityEngine;

namespace _VR_Helsing.Enemy
{
    public class FlyDoc : MonoBehaviour
    {
        [Header("Enemy Variables")]
        [SerializeField] private float speed = 5f;

        [Header("Detection Variables")]
        [SerializeField] [Range(0, 20)] private float detectionRange = 10f;
        [SerializeField] private float timeBetweenShoots = 2f;
        private float lastShootTime = 0f;
        private Transform target;

        [Header("References")]
        private WayPointManager wayPointManager;
        private Animator animator;

        private Transform currentWayPoint;

        private void Awake()
        {
            wayPointManager = GetComponent<WayPointManager>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            currentWayPoint = wayPointManager.GetFirst();
            if (!target)
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        private void Update()
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if(distanceToTarget <= detectionRange)
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

        private void Movement()
        {
            animator.SetBool("Fly", true);

            transform.SetPositionAndRotation(Vector3.MoveTowards(transform.position, currentWayPoint.position, speed * Time.deltaTime), Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(currentWayPoint.position - transform.position), Time.deltaTime * 5f));

            if (Vector3.Distance(transform.position, currentWayPoint.position) < 0.1f)
            {
                currentWayPoint = wayPointManager.GetNext();
            }
        }

        private void DetectTarget()
        {
            animator.SetBool("Attack", true);

            Vector3 direction = (target.position - transform.position).normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5f);

            if (Time.time - lastShootTime >= timeBetweenShoots)
            {
                lastShootTime = Time.time;
                Shoot();
            }
        }

        private void Shoot()
        {

        }

        private void DebugPath()
        {
            if (currentWayPoint)
            {
                Debug.DrawLine(transform.position, currentWayPoint.position, Color.red);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }
    }
}
