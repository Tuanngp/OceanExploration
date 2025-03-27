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
        healthBar = GetComponentInChildren<HealthBarBoss>();

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

        // Spawn máu ngay tại vị trí quái khi nó chết
        BloodSpawner.Instance?.SpawnBlood(transform.position);

        SpawnSpecialReward();
        //    ShowVictoryUI();
    }



    private void SpawnSpecialReward()
    {
        // Debug.Log("Spawn phần thưởng đặc biệt cho boss!");
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
