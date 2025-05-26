using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EnemyActivationZone : MonoBehaviour
{
    public GameObject enemyToActivate;
    public bool stayActive = true;

    private bool isActivated = false;
    private Transform player;

    void Awake()
    {
        BoxCollider col = GetComponent<BoxCollider>();
        col.isTrigger = true;

        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
            Debug.LogError("No se encontró un jugador con el tag 'Player'");
        else
            Debug.Log("Jugador encontrado: " + player.name);

        if (enemyToActivate == null)
            Debug.LogWarning("enemyToActivate NO asignado");
        else
            Debug.Log("enemyToActivate asignado: " + enemyToActivate.name);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entró algo al trigger: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador entró al trigger");
            ActivateEnemy();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!stayActive && other.CompareTag("Player"))
        {
            Debug.Log("Jugador salió del trigger");
            DeactivateEnemy();
        }
    }

    void ActivateEnemy()
    {
        if (isActivated || enemyToActivate == null) return;

        enemyToActivate.SetActive(true);
        isActivated = true;

        Debug.Log($" Enemigo '{enemyToActivate.name}' ACTIVADO");
    }

    void DeactivateEnemy()
    {
        if (!isActivated || enemyToActivate == null) return;

        enemyToActivate.SetActive(false);
        isActivated = false;

        Debug.Log($" Enemigo '{enemyToActivate.name}' DESACTIVADO");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        BoxCollider boxCol = GetComponent<BoxCollider>();
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.DrawWireCube(boxCol.center, boxCol.size);
    }
}