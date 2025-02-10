using UnityEngine;

public class JellyfishAI : MonoBehaviour, IDamageable
{
    public Transform submarine;
    public float moveSpeed = 2f;
    public float attackRange = 1f;
    public float attackCooldown = 2f;

    private float lastAttackTime;

    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [SerializeField] private HealthMonster healthMonster;
    [SerializeField] private GameObject healthBar; // Gán HealthBar từ Inspector
    [SerializeField] private GameObject deathEffectPrefab; // Hiệu ứng chết
    public WaveManager waveManager;


    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (submarine != null)
        {
            float distance = Vector3.Distance(transform.position, submarine.position);

            if (distance > attackRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, submarine.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthMonster.UpdateHeath(currentHealth, maxHealth); // Cập nhật thanh máu

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    //void Die()
    //{
    //    if (healthBar != null)
    //    {
    //        Destroy(healthBar); // Xóa thanh máu
    //    }

    //    Destroy(gameObject); // Xóa Jellyfish_Monster
    //}
    void Die()
    {
        if (healthBar != null)
        {
            Destroy(healthBar);
        }

        if (deathEffectPrefab != null)
        {
            GameObject deathEffect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(deathEffect, 0.75f);
        }
        waveManager?.EnemyDied(); // Báo về WaveManager khi chết
        Destroy(gameObject);
    }


}
