using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [Range(0,10)]public float speed = 10f;
    public float lifeTime = 5f;
    public int damage = 10;
    private float timer = 0f;

    [Header("References")]
    private PoolManager poolManager;

    private void OnEnable()
    {
        timer = 0f;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            poolManager.ReturnObject(gameObject);
        }
    }

    public void SetPool(PoolManager pool)
    {
        poolManager = pool;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
        poolManager.ReturnObject(gameObject);
    }
}