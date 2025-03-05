using UnityEngine;

[RequireComponent(typeof(MonsterAnimation))]
[RequireComponent(typeof(MonsterMovement))]
public class BossAI : MonsterAI
{
    private MonsterAnimation monsterAnimation;
    private MonsterMovement monsterMovement;
    void Start()
    {
        monsterAnimation = GetComponent<MonsterAnimation>();
        monsterMovement = GetComponent<MonsterMovement>();

        maxHealth = 500;  
        currentHealth = maxHealth;
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
}
