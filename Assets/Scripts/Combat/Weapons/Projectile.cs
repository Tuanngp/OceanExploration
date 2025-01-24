using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private int damage = 10;

    private void Start()
    {
        // Tự hủy sau một thời gian
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Xử lý va chạm
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent(out IDamageable target))
            {
                target.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}

internal interface IDamageable
{
    void TakeDamage(int damage);
}