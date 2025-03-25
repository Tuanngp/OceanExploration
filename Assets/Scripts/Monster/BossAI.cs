using UnityEngine;

[RequireComponent(typeof(MonsterAnimation))]
[RequireComponent(typeof(MonsterMovement))]
public class BossAI : MonsterAI
{
    private HealthBarBoss healthBar;
    private GameObject victoryCanvas;
    private GameObject BottomHUBPanel;
    private GameObject KillProgressBar;
    void Start()
    {
        monsterAnimation = GetComponent<MonsterAnimation>();
        monsterMovement = GetComponent<MonsterMovement>();
        victoryCanvas = GameObject.Find("VictoryCanvas");
        BottomHUBPanel = GameObject.Find("Bottom HUB Panel");
        KillProgressBar = GameObject.Find("KillProgressBar");
        maxHealth = 5000;
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<HealthBarBoss>();

        if (victoryCanvas != null)
        {
            victoryCanvas.SetActive(false);
        }

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

        ScoreManager.instance.AddScore(300);
        // Spawn máu ngay tại vị trí quái khi nó chết
        BloodSpawner.Instance?.SpawnBlood(transform.position);

        SpawnSpecialReward();
        ShowVictoryUI();
    }
    private void ShowVictoryUI()
    {
        if (victoryCanvas != null)
        {
            victoryCanvas.SetActive(true);
            Debug.Log(victoryCanvas);
            BottomHUBPanel.SetActive(false);
            KillProgressBar.SetActive(false);
            Time.timeScale = 0f;
        }
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
