using _VanHelsingVR.Enemy;
using UnityEngine;

namespace _VR_Helsing.Enemy
{
    public class FlyingEnemy : MonoBehaviour
    {
        [Header("Enemy Variables")]
        [SerializeField][Range(0, 5)] protected float speed = 5f;

        [Header("Detection Variables")]
        [Range(0, 20)] public float detectionRange = 10f;
        protected Transform target;

        [Header("References")]
        protected WayPointManager wayPointManager;
        protected Animator animator;
        protected Transform currentWayPoint;

        [Header("Debug")]
        public bool drawGizmos = true;

        protected virtual void Awake()
        {
            wayPointManager = GetComponent<WayPointManager>();
            animator = GetComponentInChildren<Animator>();

            if (target == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    target = player.transform;
                }
            }
        }

        private void Start()
        {
            currentWayPoint = wayPointManager.GetFirst();
            if (!target)
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        public virtual void Movement()
        {
            transform.SetPositionAndRotation(Vector3.MoveTowards(transform.position, currentWayPoint.position, speed * Time.deltaTime), Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(currentWayPoint.position - transform.position), Time.deltaTime * 5f));

            if (Vector3.Distance(transform.position, currentWayPoint.position) < 0.1f)
            {
                currentWayPoint = wayPointManager.GetNext();
            }
        }

        public virtual void DetectTarget()
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5f);
        }

        protected virtual void OnDrawGizmosSelected()
        {
            if (!drawGizmos) return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }
    }
}
