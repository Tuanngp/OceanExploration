using UnityEngine;

[RequireComponent(typeof(MonsterAnimation))]
[RequireComponent(typeof(MonsterMovement))]
public class BossAI : MonsterAI
{
    private HealthBarBoss healthBar;
    void Start()
    {
        monsterAnimation = GetComponent<MonsterAnimation>();
        monsterMovement = GetComponent<MonsterMovement>();

        maxHealth = 5000;
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<HealthBarBoss>(); // Tìm health bar trong con
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    protected override void Die()
    {
        base.Die();
        Debug.Log("Boss bị tiêu diệt!");
        SpawnSpecialReward();
    }

    private void SpawnSpecialReward()
    {
        Debug.Log("Spawn phần thưởng đặc biệt cho boss!");
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(); 
        }
    }
}
