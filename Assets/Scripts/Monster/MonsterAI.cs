using UnityEngine;

[RequireComponent(typeof(MonsterAnimation))]
[RequireComponent(typeof(MonsterMovement))]
public class MonsterAI : MonoBehaviour
{
    protected MonsterAnimation monsterAnimation;
    protected MonsterMovement monsterMovement;
    private static MonsterProgress monsterProgress;

    private bool isDead = false;
    protected int maxHealth = 100;
    protected float currentHealth = 10;
    private static int killCount = 0;

    public GameObject powerUpPrefab;
    [SerializeField] private float damageResistance = 0f;
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

    public virtual void TakeDamage(float damage)
    {
        float reducedDamage = damage * (1f - damageResistance);
        currentHealth -= reducedDamage;
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

    protected virtual void Die()
    {
        isDead = true;
        monsterAnimation?.TriggerDeath();

        killCount++;
        Debug.Log(killCount);
        monsterProgress?.UpdateProgress(killCount, SpawnMonsters.maxMonsters + 1);

        Destroy(gameObject, 0.3f);
    }

    public static int GetKillCount()
    {
        return killCount;
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

        if (other.CompareTag("Player"))
        {
            monsterAnimation?.TriggerAttack();
            Debug.Log("Monster's attacking");
        }
    }
}
