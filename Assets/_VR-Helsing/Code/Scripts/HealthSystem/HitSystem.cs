using UnityEngine;

public class HitSystem : MonoBehaviour
{
    public int hitDamage = 10;
    public LayerMask hitboxLayer;
    public LayerMask hurtboxLayer;

    public void ApplyDamage(GameObject target)
    {
        HealthSystem healthSystem = target.GetComponent<HealthSystem>();
        if (healthSystem != null)
        {
            healthSystem.TakeDamage(hitDamage);
            Debug.Log("Hurtbox: Damage applied with hitDamage: " + hitDamage);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & hitboxLayer) != 0)
        {
            Debug.Log("Hitbox: Detected contact with " + other.gameObject.name);
        }
      
        else if (((1 << other.gameObject.layer) & hurtboxLayer) != 0)
        {
            ApplyDamage(other.gameObject);
        }
    }
}
