using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FlyEnemy : MonoBehaviour
{
    [Header("Patrullaje")]
    public Transform[] waypoints; // Lista de puntos a patrullar
    private int currentWaypointIndex = 0;
    public float speed = 3f; // Velocidad del movimiento
    public float reachDistance = 1f; // Distancia para considerar que llegó al waypoint

    [Header("Detección del jugador")]
    public float detectionRange = 8f; // Rango de persecución
    public float explosionRange = 2.5f; // Rango para explotar
    private Transform player;

    [Header("Debug / Gizmos")]
    public bool drawGizmos = true;

    private bool isChasingPlayer = false;
    private bool hasExploded = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (hasExploded) return;

        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= explosionRange)
        {
            Explode();
            return;
        }

        // Si ya empezó a perseguir o está dentro del rango, sigue persiguiendo
        if (isChasingPlayer || distanceToPlayer <= detectionRange)
        {
            isChasingPlayer = true;
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void ChasePlayer()
    {
        isChasingPlayer = true;
        transform.LookAt(player);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void Patrol()
    {
        isChasingPlayer = false;

        if (waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypointIndex];
        transform.LookAt(target);

        transform.position += transform.forward * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) <= reachDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    void Explode()
    {
        hasExploded = true;
        Debug.Log("La mosca ha explotado!");
        // Aquí irá el efecto visual 
        gameObject.SetActive(false); // Desactiva al enemigo por ahora
    }

    // Gizmos para mostrar los rangos
    private void OnDrawGizmosSelected()
    {
        if (!drawGizmos) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}