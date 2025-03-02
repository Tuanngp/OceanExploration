using UnityEngine;
using UnityEngine.UI;

public class MonsterHealthBar : MonoBehaviour
{
    private MonsterAI monster;
    private Image healthBar;

    void Start()
    {
        healthBar = GetComponent<Image>();

        // Tìm MonsterAI từ cha của HealthBarMonster (Monster)
        Transform monsterTransform = transform.parent?.parent; // Monster là cha của HealthBarMonster

        if (monsterTransform != null)
        {
            monster = monsterTransform.GetComponent<MonsterAI>(); // Lấy MonsterAI từ Monster
        }

        if (monster == null)
        {
            Debug.LogError($"⚠ Không tìm thấy MonsterAI! Kiểm tra lại cấu trúc Prefab của {gameObject.name}");
        }

        if (healthBar == null)
        {
            Debug.LogError($"⚠ Không tìm thấy Image HealthBar! {gameObject.name}");
        }
    }

    void Update()
    {
        healthBar.fillAmount = (float)monster.GetCurrentHealth() / monster.GetMaxHealth();
    }
}
