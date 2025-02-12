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
    public float damagePerHit = 10f;
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
        if (other.CompareTag("Enemy")) 
        {
            Debug.Log("Enemy detected! Decreasing health.");
            DecreaseHealth(damagePerHit);
        }
    }
    public void DecreaseMana(float amount)
    {
        currentMana = Mathf.Clamp(currentMana - amount, 0, maxMana);
        Debug.Log("Current mana: " + currentMana);

        float manaRatio = currentMana / maxMana;

        if (manaRatio > 0.5f)
        {
            manaLeft.fillAmount = 1f;
            manaRight.fillAmount = (manaRatio - 0.5f) * 2;
        }
        else
        {
            manaLeft.fillAmount = manaRatio * 2;
            manaRight.fillAmount = 0f;
        }
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        Debug.Log("Current health: " + currentHealth);

        float healthRatio = currentHealth / maxHealth;

        if (healthRatio > 0.5f)
        {
            healthLeft.fillAmount = 1f;
            healthRight.fillAmount = (healthRatio - 0.5f) * 2;
        }
        else
        {
            healthLeft.fillAmount = healthRatio * 2;
            healthRight.fillAmount = 0f;
        }
    }

    public void DecreaseStamina(float amount)
    {
        currentStamina = Mathf.Clamp(currentStamina - amount, 0, maxStamina);
        Debug.Log("Current stamina: " + currentStamina);

        float staminaRatio = currentStamina / maxStamina;
        staminaPip.fillAmount = staminaRatio;
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log("Current health: " + currentHealth);

        float healthRatio = currentHealth / maxHealth;

        if (healthRatio > 0.5f)
        {
            healthLeft.fillAmount = 1f;
            healthRight.fillAmount = (healthRatio - 0.5f) * 2;
        }
        else
        {
            healthLeft.fillAmount = healthRatio * 2;
            healthRight.fillAmount = 0f;
        }
    }

    public void IncreaseMana(float amount)
    {
        currentMana = Mathf.Clamp(currentMana + amount, 0, maxMana);
        Debug.Log("Current mana: " + currentMana);

        float manaRatio = currentMana / maxMana;

        if (manaRatio > 0.5f)
        {
            manaLeft.fillAmount = 1f;
            manaRight.fillAmount = (manaRatio - 0.5f) * 2;
        }
        else
        {
            manaLeft.fillAmount = manaRatio * 2;
            manaRight.fillAmount = 0f;
        }
    }

    public void IncreaseStamina(float amount)
    {
        currentStamina = Mathf.Clamp(currentStamina + amount, 0, maxStamina);
        Debug.Log("Current stamina: " + currentStamina);

        float staminaRatio = currentStamina / maxStamina;
        staminaPip.fillAmount = staminaRatio;
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
