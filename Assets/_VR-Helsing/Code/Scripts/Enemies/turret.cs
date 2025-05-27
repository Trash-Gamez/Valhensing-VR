using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player;                  // Jugador objetivo
    public Transform turretTorso;             // Parte que rota 
    public Transform cannonBarrel;            // Punto desde donde salen los disparos

    [Header("Configuración de disparo")]
    public float shootRange = 15f;            // Rango máximo de detección
    public float fireRate = 0.5f;               // Disparos por segundo
    public string bulletTag = "Bullet";       // Tag usado en Object Pool

    [Header("Límites de rotación")]
    public float rotationSpeed = 5f;          // Velocidad de rotación
    public Vector2 horizontalAngleLimits = new Vector2(-90f, 90f);
    public Vector2 verticalAngleLimits = new Vector2(-90f, 90f);

    private float fireTimer = 0f;
    private Quaternion initialTorsoRotation;
    private Transform turretBase; // Referencia a la base de la torreta

    void Start()
    {
        if (turretTorso == null)
        {
            Debug.LogError("turretTorso no asignado en el scriptable object.");
            enabled = false;
            return;
        }

        initialTorsoRotation = turretTorso.localRotation;

        // La base es el padre del torso o el mismo transform si no tiene padre
        turretBase = turretTorso.parent != null ? turretTorso.parent : transform;
    }

    void Update()
    {
        if (player == null) return;

        if (IsPlayerInFiringRange())
        {
            RotateTorsoTowardsPlayer();

            fireTimer += Time.deltaTime;
            if (fireTimer >= 1f / fireRate)
            {
                Shoot();
                fireTimer = 0f;
            }
        }
        else
        {
            //volver a rotación inicial
            ResetTorsoRotation();
        }
    }

    bool IsPlayerInFiringRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > shootRange)
            return false;

        // Dirección desde la base de la torreta hacia el jugador
        Vector3 directionToPlayer = (player.position - turretBase.position).normalized;

        // Convertir la dirección a espacio local de la base
        Vector3 localDirection = turretBase.InverseTransformDirection(directionToPlayer);

        // Calcular ángulos en espacio local
        float horizontalAngle = Mathf.Atan2(localDirection.x, localDirection.z) * Mathf.Rad2Deg;
        float verticalAngle = Mathf.Atan2(localDirection.y, new Vector2(localDirection.x, localDirection.z).magnitude) * Mathf.Rad2Deg;

        // Normalizar ángulos
        horizontalAngle = NormalizeAngle(horizontalAngle);
        verticalAngle = NormalizeAngle(verticalAngle);

        bool inHorizontal = horizontalAngle >= horizontalAngleLimits.x && horizontalAngle <= horizontalAngleLimits.y;
        bool inVertical = verticalAngle >= verticalAngleLimits.x && verticalAngle <= verticalAngleLimits.y;

        return inHorizontal && inVertical;
    }

    void RotateTorsoTowardsPlayer()
    {
        // Dirección desde el torso hacia el jugador
        Vector3 direction = (player.position - turretTorso.position).normalized;

        // Calcular rotación objetivo en espacio mundo
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Convertir a espacio local del padre
        Quaternion localTargetRotation = Quaternion.Inverse(turretBase.rotation) * targetRotation;
        Vector3 eulerAngles = localTargetRotation.eulerAngles;

        // Normalizar ángulos
        eulerAngles.x = NormalizeAngle(eulerAngles.x);
        eulerAngles.y = NormalizeAngle(eulerAngles.y);
        eulerAngles.z = 0f; // No rotación en Z

        // Aplicar límites
        float limitedY = Mathf.Clamp(eulerAngles.y, horizontalAngleLimits.x, horizontalAngleLimits.y);
        float limitedX = Mathf.Clamp(eulerAngles.x, verticalAngleLimits.x, verticalAngleLimits.y);

        // Crear rotación final limitada
        Quaternion limitedLocalRotation = Quaternion.Euler(limitedX, limitedY, 0f);
        Quaternion finalWorldRotation = turretBase.rotation * limitedLocalRotation;

        // Aplicar rotación suavemente
        turretTorso.rotation = Quaternion.Slerp(turretTorso.rotation, finalWorldRotation, Time.deltaTime * rotationSpeed);
    }

    void ResetTorsoRotation()
    {
        // Convertir rotación inicial local a mundo
        Quaternion worldInitialRotation = turretBase.rotation * initialTorsoRotation;
        turretTorso.rotation = Quaternion.Slerp(turretTorso.rotation, worldInitialRotation, Time.deltaTime * rotationSpeed);
    }

    void Shoot()
    {
        GameObject bullet = ObjectPool.Instance.GetPooledObject(bulletTag);
        if (bullet != null)
        {
            //AudioManager.Instance.PlaySound3D("TurretShoot", cannonBarrel.transform.position);
            bullet.transform.position = cannonBarrel.position;
            bullet.transform.rotation = cannonBarrel.rotation;
            bullet.SetActive(true);
        }
    }

    float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }

    
    void OnDrawGizmosSelected()
    {
        if (turretBase == null) return;

        // Dibujar rango de detección
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, shootRange);

        // Dibujar límites de rotación horizontal
        Gizmos.color = Color.red;
        Vector3 leftLimit = Quaternion.AngleAxis(horizontalAngleLimits.x, turretBase.up) * turretBase.forward;
        Vector3 rightLimit = Quaternion.AngleAxis(horizontalAngleLimits.y, turretBase.up) * turretBase.forward;

        Gizmos.DrawRay(turretBase.position, leftLimit * shootRange);
        Gizmos.DrawRay(turretBase.position, rightLimit * shootRange);
    }
}