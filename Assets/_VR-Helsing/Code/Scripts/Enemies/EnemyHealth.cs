using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    
    public int maxHealth = 60;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; 
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " recibió " + damage + " de daño. Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " ha muerto.");
        this.transform.parent.gameObject.SetActive(false);
    }
}