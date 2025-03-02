using UnityEngine;

[RequireComponent(typeof(MonsterAnimation))]
[RequireComponent(typeof(MonsterMovement))]
public class MonsterAI : MonoBehaviour, IDamageable
{
    private MonsterAnimation monsterAnimation;
    private MonsterMovement monsterMovement;
    private bool isDead = false;
    private int maxHealth = 100;
    private int currentHealth = 100;
    private static int killCount = 0;
    private static MonsterProgress monsterProgress;

    void Start()
    {
        monsterAnimation = GetComponent<MonsterAnimation>();
        monsterMovement = GetComponent<MonsterMovement>();

        if (monsterProgress == null)
        {
            GameObject progressObj = GameObject.Find("KillProgressBar");
            if (progressObj != null)
                monsterProgress = progressObj.GetComponent<MonsterProgress>();
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Monster bị bắn, máu còn lại: " + currentHealth);
        monsterAnimation?.ChangeColor(Color.red);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            if (monsterAnimation != null)
            {
                monsterAnimation?.Invoke(nameof(monsterAnimation.ResetColor), 0.2f);
            }

        }
    }

    private void Die()
    {
        isDead = true;
        monsterAnimation?.TriggerDeath();

        killCount++;
        Debug.Log(killCount);
        monsterProgress?.UpdateProgress(killCount, SpawnMonsters.maxMonsters + 1);

        Destroy(gameObject, 0.8f);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        if (other.CompareTag("Player"))
        {
            monsterAnimation?.TriggerAttack();
            Debug.Log("Monster's attacking");
        }
    }

    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
}

