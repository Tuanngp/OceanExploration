using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private float damage = 10;
    private bool isHit = false;
    private Animator animator;
    private UpgradeManager upgradeManager;
    private void Start()
    {
        animator = GetComponent<Animator>();
        upgradeManager = GameObject.Find("Submarine").GetComponent<UpgradeManager>();
        Destroy(gameObject, lifetime);
    }

    public void SetDamage(float damageValue)
    {
        damage = damageValue;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isHit && other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent(out MonsterAI target))
            {
                isHit = true;
                Debug.Log("Đạn bắn trúng quái vật! " + damage);
                target.TakeDamage(damage);
                animator.SetInteger("impact", upgradeManager.selectedShipIndex);
            }
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            Destroy(gameObject, 0.45f);
        }
    }
}