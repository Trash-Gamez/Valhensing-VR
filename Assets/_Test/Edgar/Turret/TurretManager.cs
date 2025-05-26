using UnityEngine;

public class TurretManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform player; // Asignar manualmente o dejar vacío para buscar por tag

    [Header("Configuración")]
    [SerializeField] private bool setupOnStart = true; // Configurar torretas al iniciar
    [SerializeField] private bool includeInactiveObjects = false; // Incluir torretas inactivas

    void Start()
    {
        if (setupOnStart)
        {
            SetupAllTurrets();
        }
    }

    [ContextMenu("Configurar todas las torretas")]
    public void SetupAllTurrets()
    {
        // Buscar jugador si no está asignado
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
                Debug.Log("Jugador encontrado automáticamente: " + player.name);
            }
            else
            {
                Debug.LogError("No se encontró jugador con tag 'Player'");
                return;
            }
        }

        // Encontrar todas las torretas en la escena
        Turret[] allTurrets = FindObjectsOfType<Turret>(includeInactiveObjects);

        int turretsConfigured = 0;
        int turretsAlreadyConfigured = 0;

        foreach (Turret turret in allTurrets)
        {
            if (turret.player == null)
            {
                turret.player = player;
                turretsConfigured++;
                Debug.Log($"Torreta configurada: {turret.gameObject.name}");
            }
            else
            {
                turretsAlreadyConfigured++;
            }
        }

        Debug.Log($"Configuración completada: {turretsConfigured} torretas configuradas, {turretsAlreadyConfigured} ya tenían jugador asignado");
    }

    // Método para configurar una torreta específica
    public void SetupTurret(Turret turret)
    {
        if (turret == null) return;

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        if (player != null)
        {
            turret.player = player;
            Debug.Log($"Torreta individual configurada: {turret.gameObject.name}");
        }
    }

    // Método público para reconfigurar todas las torretas (útil si el jugador cambia)
    public void ReconfigureAllTurrets()
    {
        SetupAllTurrets();
    }
}