using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [Header("UI Elements")]
    public RectTransform healthBar;

    [Header("Health Settings")]
    private Image healthLeft;
    private Image healthRight;
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Mana Settings")]
    private Image manaLeft;
    private Image manaRight;
    public float maxMana = 100f;
    public float currentMana;

    [Header("Stamina Settings")]
    private Image staminaPip;
    public float maxStamina = 100f;
    public float currentStamina;

    [Header("Damage Settings")]
    public float damagePerHit = 10f; // Tăng lên 600 để mỗi khung hình trừ khoảng 10 HP

    [SerializeField] private GameObject deathEffectPrefab; // Hiệu ứng chết

    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        currentStamina = maxStamina;
        healthLeft = healthBar.Find("Hub/Health/HealthLeft").GetComponent<Image>();
        healthRight = healthBar.Find("Hub/Health/HealthRight").GetComponent<Image>();
        manaLeft = healthBar.Find("Hub/Mana/ManaLeft").GetComponent<Image>();
        manaRight = healthBar.Find("Hub/Mana/ManaRight").GetComponent<Image>();
        staminaPip = healthBar.Find("Hub/Stamina pip/Stamina pip").GetComponent<Image>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // Nếu quái vật chạm vào submarine
        {
            Debug.Log("Enemy detected! Decreasing health.");
            DecreaseHealth(damagePerHit * Time.deltaTime);
        }
    }

    public void DecreaseHealth(float amount)
    {

        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        Debug.Log("Current hp: " + currentHealth);

        if (healthRight.fillAmount > 0)
        {
            healthRight.fillAmount = Mathf.Max(0, healthRight.fillAmount - amount);
        }
        else if (healthLeft.fillAmount > 0)
        {
            healthLeft.fillAmount = Mathf.Max(0, healthLeft.fillAmount - amount);
        }

        if (healthLeft.fillAmount <= 0.2)
        {
            GameObject deathEffect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(deathEffect, 3f);
            Invoke(nameof(DestroyObject), 0.5f); 
        }
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
    public void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        if (healthLeft.fillAmount < 1)
        {
            healthLeft.fillAmount = Mathf.Min(1, healthLeft.fillAmount + amount);
        }
        else if (healthRight.fillAmount < 1)
        {
            healthRight.fillAmount = Mathf.Min(1, healthRight.fillAmount + amount);
        }
    }

    public void DecreaseMana(float amount)
    {
        currentMana = Mathf.Clamp(currentMana - amount, 0, maxMana);
        Debug.Log("Current mana: " + currentMana);
        if (manaRight.fillAmount > 0)
        {
            manaRight.fillAmount = Mathf.Max(0, manaRight.fillAmount - amount);
        }
        else if (manaLeft.fillAmount > 0)
        {
            manaLeft.fillAmount = Mathf.Max(0, manaLeft.fillAmount - amount);
        }
    }

    public void IncreaseMana(float amount)
    {
        currentMana = Mathf.Clamp(currentMana + amount, 0, maxMana);
        Debug.Log("Current mana: " + currentMana);

        if (manaLeft.fillAmount < 1)
        {
            manaLeft.fillAmount = Mathf.Min(1, manaLeft.fillAmount + amount);
        }
        else if (manaRight.fillAmount < 1)
        {
            manaRight.fillAmount = Mathf.Min(1, manaRight.fillAmount + amount);
        }
    }

    void UpdateBar(RectTransform left, RectTransform right, float current, float max)
    {
        float scale = current / max;
        left.localScale = new Vector3(scale, 1f, 1f);
        right.localScale = new Vector3(scale, 1f, 1f);
    }

    void UpdateStamina(RectTransform stamina, float current, float max)
    {
        float scale = current / max;
        stamina.localScale = new Vector3(scale, 1f, 1f);
    }

    public void ChangeMana(float amount)
    {
        currentMana = Mathf.Clamp(currentMana + amount, 0, maxMana);
    }

    public void ChangeStamina(float amount)
    {
        currentStamina = Mathf.Clamp(currentStamina + amount, 0, maxStamina);
    }
}
