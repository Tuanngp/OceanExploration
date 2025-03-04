using System.Collections;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    private Animator animator;
    private bool isDead = false;
    private float currentHealth = 100;
    private bool isMoving = false;
    private bool hasSpottedPlayer = false;
    public GameObject powerUpPrefab;  // Kéo viên thuốc vô đây (Inspector)
    [SerializeField] private float damageResistance = 0f;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (target != null)
        {
            if (!hasSpottedPlayer && IsTargetVisible())
            {
                hasSpottedPlayer = true;
                if (WarningSystem.Instance != null)
                {
                    WarningSystem.Instance.ShowWarning();
                    Debug.Log("Monster đã phát hiện tàu, bắt đầu đuổi theo!");
                }
                else
                {
                    Debug.LogError("WarningSystem chưa khởi tạo! Check lại scene.");
                }
            }

            if (hasSpottedPlayer)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                isMoving = direction.magnitude > 0.01f;

                if (isMoving)
                {
                    MoveToTarget();
                    animator.SetBool("isMoving", true);
                }
                else
                {
                    animator.SetBool("isMoving", false);
                }
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void MoveToTarget()
    {
        if (target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private bool IsTargetVisible()
    {
        if (target == null) return false;

        float distance = Vector3.Distance(transform.position, target.position);
        return distance < 60f;
    }

    private void ChangeColor(Color newColor)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = newColor;
        }
    }

    private void ResetColor()
    {
        ChangeColor(Color.white);
    }

    public void TakeDamage(float damage)
    {
        float reducedDamage = damage * (1f - damageResistance);
        currentHealth -= reducedDamage;
        Debug.Log("Monster bị bắn, máu còn lại: " + currentHealth);
        ChangeColor(Color.red);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            Invoke(nameof(ResetColor), 0.2f);
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("death");

        // Xóa khỏi danh sách active monsters
        SpawnMonsters.ActiveMonsters.Remove(this);

        if (Random.value < 0.2f)
        {
            SpawnPowerUp();
        }

        Destroy(gameObject, 0.1f);
    }

    private void SpawnPowerUp()
    {
        GameObject powerUp = Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
        float randomScale = Random.Range(1.5f, 3f);
        powerUp.transform.localScale = new Vector3(randomScale, randomScale, 1f);
        powerUp.AddComponent<PowerUpRotation>();  // Xoay nhẹ
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        if (other.CompareTag("Projectile"))
        {
            TakeDamage(10);
        }

        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("attack");
            Debug.Log("Monster's attacking");
        }
    }
}
