using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public int damage = 35;
    public float lifeTime = 10f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            damageable?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
