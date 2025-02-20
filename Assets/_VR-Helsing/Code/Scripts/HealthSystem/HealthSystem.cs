using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Received damage: " + damageAmount);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log("Healed: " + healAmount);
    }

    private void Die()
    {
        Debug.Log("Died.");
    }
}
