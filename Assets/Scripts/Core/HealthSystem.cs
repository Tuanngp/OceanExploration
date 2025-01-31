using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    public delegate void OnHealthChanged(float health);
    public event OnHealthChanged OnHealthChangedEvent;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); // Đảm bảo máu không âm
        OnHealthChangedEvent?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Submarine destroyed!");
        // Xử lý khi tàu ngầm bị phá hủy (ví dụ: hiệu ứng nổ, game over)
    }
}
