using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [Header("Health Settings")]
    public Image healthLeft;
    public Image healthRight;
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Mana Settings")]
    public Image manaLeft;
    public Image manaRight;
    public float maxMana = 100f;
    public float currentMana;

    [Header("Stamina Settings")]
    public Image staminaPip;
    public float maxStamina = 100f;
    public float currentStamina;

    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        currentStamina = maxStamina;
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if (healthRight.fillAmount > 0)
        {
            healthRight.fillAmount = Mathf.Max(0, healthRight.fillAmount - amount);
        }
        else if (healthLeft.fillAmount > 0)
        {
            healthLeft.fillAmount = Mathf.Max(0, healthLeft.fillAmount - amount);
        }
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
        currentMana = Mathf.Clamp(currentMana + amount, 0, maxMana);
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
