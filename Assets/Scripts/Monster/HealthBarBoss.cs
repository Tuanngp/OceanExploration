using UnityEngine;
using UnityEngine.UI;

public class HealthBarBoss : MonoBehaviour
{
    [SerializeField] private Image health; 
    private BossAI boss;  

    void Start()
    {
        if (health == null)
        {
            Debug.LogError("Không tìm thấy Image cho thanh máu!");
        }
        boss = GetComponentInParent<BossAI>(); 
        if (boss == null)
        {
            Debug.LogError("HealthBarBoss: Không tìm thấy BossAI!");
        }
    }

    public void UpdateHealthBar()
    {
        if (boss != null && health != null)
        {
            health.fillAmount = (float)boss.GetCurrentHealth() / boss.GetMaxHealth();
        }
    }
}
