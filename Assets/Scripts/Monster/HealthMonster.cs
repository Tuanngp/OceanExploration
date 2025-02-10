using UnityEngine;
using UnityEngine.UI;

public class HealthMonster : MonoBehaviour
{
    private Image _healthBar;
    [SerializeField] private Canvas healthCanvas; // Gán Canvas vào Inspector
    void Start()
    {
        if (healthCanvas != null)
        {
            Transform healthBarParent = healthCanvas.transform.Find("HealthBarMonster"); // Tìm Object chứa Image
            if (healthBarParent != null)
            {
                _healthBar = healthBarParent.Find("HealthBarMonster")?.GetComponent<Image>();
            } 
        } 
        if (_healthBar == null)
        {
            Debug.LogError("Không tìm thấy HealthBarMonster (Image) trong Canvas!");
        }
    }

    public void UpdateHeath(float currentHealth, float maxHealth)
    {
        _healthBar.fillAmount = currentHealth / maxHealth;
    }
    public void SetMaxHealth(float maxHealth)
    {
        _healthBar.fillAmount = 1f;
    }
}
