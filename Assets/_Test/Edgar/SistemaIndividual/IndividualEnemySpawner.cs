using UnityEngine;

public class IndividualEnemySpawner : MonoBehaviour
{
    [Header("Configuración del Spawn")]
    [SerializeField] private GameObject enemyPrefab; // El prefab específico de este spawner
    [SerializeField] private float detectionRange = 8f; // Rango de detección del jugador
    [SerializeField] private bool spawnOnlyOnce = false; // Si solo debe aparecer una vez

    [Header("Referencias")]
    [SerializeField] private Transform player; // Referencia al jugador

    [Header("Debug")]
    [SerializeField] private bool showGizmos = true; // Mostrar el rango en la escena

    private GameObject spawnedEnemy; // El enemigo que se ha instanciado
    private bool hasSpawned = false; // Control si ya ha aparecido alguna vez
    private bool playerInRange = false; // Si el jugador está en rango

    void Start()
    {
        // Si no se asigna el jugador, buscar por tag
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        // Validar que tengamos el prefab
        if (enemyPrefab == null)
        {
            Debug.LogWarning("No hay prefab de enemigo asignado en " + gameObject.name);
        }
    }

    void Update()
    {
        if (player == null || enemyPrefab == null) return;

        // Calcular distancia al jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        bool wasInRange = playerInRange;
        playerInRange = distanceToPlayer <= detectionRange;

        // Si el jugador entra en rango y debe spawnear
        if (playerInRange && !wasInRange && ShouldSpawn())
        {
            SpawnEnemy();
        }

        // El enemigo ya no se destruye cuando el jugador sale del rango
        // Una vez spawneado, permanece para siempre
    }

    private bool ShouldSpawn()
    {
        // No spawner si ya hay un enemigo activo
        if (spawnedEnemy != null) return false;

        // Si solo debe aparecer una vez y ya apareció
        if (spawnOnlyOnce && hasSpawned) return false;

        return true;
    }

    private void SpawnEnemy()
    {
        // Instanciar el enemigo en la posición de este GameObject
        spawnedEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        hasSpawned = true;

        // Configurar referencias del enemigo spawneado
        SetupSpawnedEnemy();

        Debug.Log($"Enemigo spawneado: {enemyPrefab.name} en {transform.position}");
    }

    private void SetupSpawnedEnemy()
    {
        if (spawnedEnemy == null)
        {
            Debug.LogError("spawnedEnemy es null!");
            return;
        }

        if (player == null)
        {
            Debug.LogError("player es null!");
            return;
        }

        Debug.Log($"Configurando enemigo spawneado: {spawnedEnemy.name}");

        // Buscar y configurar script de Turret
        // hacer esto con todos los enemigos

        Turret turretScript = spawnedEnemy.GetComponent<Turret>();
        if (turretScript != null)
        {
            Debug.Log("Script Turret encontrado, asignando jugador...");
            turretScript.player = player;
        }
        else
        {
            Debug.LogWarning("No se encontró script Turret en el enemigo spawneado");

            // Mostrar todos los componentes para debug
            Component[] components = spawnedEnemy.GetComponents<Component>();
            Debug.Log("Componentes encontrados:");
            foreach (Component comp in components)
            {
                Debug.Log("- " + comp.GetType().Name);
            }
        }
    }

    private void DestroySpawnedEnemy()
    {
        if (spawnedEnemy != null)
        {
            Destroy(spawnedEnemy);
            spawnedEnemy = null;
            Debug.Log("Enemigo destruido - jugador fuera del rango");
        }
    }

    // Método público para forzar el spawn
    public void ForceSpawn()
    {
        if (ShouldSpawn())
        {
            SpawnEnemy();
        }
    }

    // Método público para destruir el enemigo (solo si necesitas forzarlo externamente)
    public void ForceDestroyEnemy()
    {
        DestroySpawnedEnemy();
    }

    // Método para resetear el spawner
    public void ResetSpawner()
    {
        DestroySpawnedEnemy();
        hasSpawned = false;
    }

    // Verificar si el enemigo está actualmente spawneado
    public bool IsEnemySpawned()
    {
        return spawnedEnemy != null;
    }

    // Dibujar el rango de detección en la escena
    void OnDrawGizmos()
    {
        if (!showGizmos) return;

        // Color diferente si el jugador está en rango
        Gizmos.color = playerInRange ? Color.red : Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Mostrar icono del spawner
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 0.5f);
    }

    void OnDrawGizmosSelected()
    {
        if (!showGizmos) return;

        // Área de detección semi-transparente cuando está seleccionado
        Gizmos.color = new Color(0, 1, 1, 0.1f);
        Gizmos.DrawSphere(transform.position, detectionRange);
    }
}