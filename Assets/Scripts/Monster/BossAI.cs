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

        if(victoryCanvas != null)
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
        base.TakeDamage(damage);
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar();
        }
    }
}
