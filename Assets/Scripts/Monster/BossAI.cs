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
        SpawnSpecialReward();
    //    ShowVictoryUI();
    }
    


    private void SpawnSpecialReward()
    {
        Debug.Log("Spawn phần thưởng đặc biệt cho boss!");
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(100);

        if (healthBar != null)
        {
            healthBar.UpdateHealthBar();
        }
    }
}
