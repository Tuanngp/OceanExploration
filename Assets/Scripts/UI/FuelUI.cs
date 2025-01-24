using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class FuelUI : MonoBehaviour
{
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject manaBar;
    private Image[] healthBarImages;
    private Image[] manaBarImages;
    private Image healthLeft;
    private Image healthRight;
    private Image manaLeft;
    private Image manaRight;
    void Start()
    {
        healthBarImages = healthBar.GetComponents<Image>();
        manaBarImages = manaBar.GetComponents<Image>();
        GetImages();
    }

    private void GetImages()
    {
        healthLeft = healthBarImages.FirstOrDefault(img => img.name == "HealthLeft");
        healthRight = healthBarImages.FirstOrDefault(img => img.name == "HealthRight");
        manaLeft = manaBarImages.FirstOrDefault(img => img.name == "ManaLeft");
        manaRight = manaBarImages.FirstOrDefault(img => img.name == "ManaRight");
    }

    public void UpdateFuel(float health, float mana)
    {
        healthLeft.fillAmount = health;
        healthRight.fillAmount = 1 - health;
        manaLeft.fillAmount = mana;
        manaRight.fillAmount = 1 - mana;
    }

    private void OnEnable()
    {
        // SubmarineController.OnFuelUpdate += UpdateFuel;
    }
}