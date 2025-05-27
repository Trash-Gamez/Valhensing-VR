using UnityEngine;

public class ActivateEnemyOnPlayerRange : MonoBehaviour
{
    [Header("Configuración")]
    public Transform player;              // Referencia al jugador
    public GameObject enemyToActivate;   // Enemigo que se activará
    public float activationRange = 10f;  // Distancia a la que se activa

    private bool hasActivated = false;   // Para evitar activar múltiples veces

    void Update()
    {
        if (player == null || enemyToActivate == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (!hasActivated && distance <= activationRange)
        {
            ActivateEnemy();
            hasActivated = true;
        }
    }

    void ActivateEnemy()
    {
        if (enemyToActivate != null)
        {
            enemyToActivate.SetActive(true);
            Debug.Log("Enemigo activado!");
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, activationRange);
    }
}