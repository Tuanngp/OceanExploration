using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private int damage = 10;
    private Animator animator;
    private bool isHit = false;

    private void Start()
    {
        // Tự hủy sau một thời gian
        animator = GetComponent<Animator>();
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isHit && other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent(out IDamageable target))
            {
                isHit = true;
                animator.SetTrigger("Hit");
                target.TakeDamage(damage);
            }
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            Destroy(gameObject, 0.45f);
        }
    }
}

internal interface IDamageable
{
    void TakeDamage(int damage);
}