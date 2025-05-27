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

    void Start()
    {

        if (turretTorso == null)
        {
            Debug.LogError("turretTorso no asignado en el scriptable object.");
            enabled = false;
            return;
        }

        initialTorsoRotation = turretTorso.localRotation;
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

        Vector3 directionToPlayer = (player.position - turretTorso.position).normalized;

        // Calcular ángulos localizados
        Vector3 forward = turretTorso.forward;
        Quaternion inverseParent = Quaternion.Inverse(turretTorso.parent.rotation);
        Vector3 localDir = inverseParent * directionToPlayer;

        float angleY = NormalizeAngle(Vector3.SignedAngle(forward, localDir, Vector3.up));
        float angleX = NormalizeAngle(Vector3.SignedAngle(forward, localDir, Vector3.right));

        bool inHorizontal = angleY >= horizontalAngleLimits.x && angleY <= horizontalAngleLimits.y;
        bool inVertical = angleX >= verticalAngleLimits.x && angleX <= verticalAngleLimits.y;

        return inHorizontal && inVertical;
    }

    void RotateTorsoTowardsPlayer()
    {
        Vector3 direction = (player.position - turretTorso.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Limitar rotación al espacio local del padre
        Quaternion deltaRotation = Quaternion.Inverse(turretTorso.parent.rotation) * targetRotation;
        Vector3 eulerAngles = deltaRotation.eulerAngles;

        eulerAngles.x = NormalizeAngle(eulerAngles.x);
        eulerAngles.y = NormalizeAngle(eulerAngles.y);

        float limitedY = Mathf.Clamp(eulerAngles.y, horizontalAngleLimits.x, horizontalAngleLimits.y);
        float limitedX = Mathf.Clamp(eulerAngles.x, verticalAngleLimits.x, verticalAngleLimits.y);

        Quaternion finalRotation = Quaternion.Euler(limitedX, limitedY, 0f);
        turretTorso.rotation = Quaternion.Slerp(turretTorso.rotation, finalRotation, Time.deltaTime * rotationSpeed);
    }

    void ResetTorsoRotation()
    {
        turretTorso.rotation = Quaternion.Slerp(turretTorso.rotation, initialTorsoRotation, Time.deltaTime * rotationSpeed);
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
        if (angle > 180f) angle -= 360f;
        return angle;
    }
}