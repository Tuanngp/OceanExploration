using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject upgradePanel;
    public TextMeshProUGUI resourceText;
    public Button speedButton, healthButton, weaponButton;
    public TextMeshProUGUI speedCostText, healthCostText, weaponCostText;
    public TextMeshProUGUI speedValueText, healthValueText, weaponValueText;
    public TextMeshProUGUI speedLevelText, healthLevelText, weaponLevelText;
    public Button closeButton;
    public Button previousShipButton, nextShipButton;

    [Header("Ship Preview UI")]
    public Image shipPreviewImage;
    public TextMeshProUGUI shipPriceText;
    public Button buyOrSelectButton;
    public TextMeshProUGUI buyOrSelectButtonText;

    [Header("References")]
    public UpgradeManager upgradeManager;

    void Start()
    {
        upgradeManager = GetComponent<UpgradeManager>();
        speedButton.onClick.AddListener(UpgradeSpeed);
        healthButton.onClick.AddListener(UpgradeHealth);
        weaponButton.onClick.AddListener(UpgradeWeapon);
        closeButton.onClick.AddListener(ClosePanel);
        previousShipButton.onClick.AddListener(upgradeManager.PreviousShipPart);
        nextShipButton.onClick.AddListener(upgradeManager.NextShipPart);
        buyOrSelectButton.onClick.AddListener(OnBuyOrSelectShip);

        speedCostText = speedButton.GetComponentInChildren<TextMeshProUGUI>();
        healthCostText = healthButton.GetComponentInChildren<TextMeshProUGUI>();
        weaponCostText = weaponButton.GetComponentInChildren<TextMeshProUGUI>();

        upgradePanel.SetActive(false);
    }

    void Update()
    {
        resourceText.text = $"Resources: {upgradeManager.rareResources}";

        speedCostText.text = $"{upgradeManager.speedUpgradeCost}";
        healthCostText.text = $"{upgradeManager.healthUpgradeCost}";
        weaponCostText.text = $"{upgradeManager.weaponUpgradeCost}";

        speedValueText.text = $"{upgradeManager.GetCurrentSpeed()}";
        healthValueText.text = $"{upgradeManager.GetCurrentHealth()}";
        weaponValueText.text = $"{upgradeManager.GetCurrentDamage()}";

        // Cập nhật cấp độ hiện tại
        // speedLevelText.text = $"Level {upgradeManager.speedLevel}/{upgradeManager.maxLevel}";
        // healthLevelText.text = $"Level {upgradeManager.healthLevel}/{upgradeManager.maxLevel}";
        // weaponLevelText.text = $"Level {upgradeManager.weaponLevel}/{upgradeManager.maxLevel}";

        speedButton.interactable = upgradeManager.CanUpgrade(upgradeManager.speedUpgradeCost) && upgradeManager.speedLevel < upgradeManager.maxLevel;
        healthButton.interactable = upgradeManager.CanUpgrade(upgradeManager.healthUpgradeCost) && upgradeManager.healthLevel < upgradeManager.maxLevel;
        weaponButton.interactable = upgradeManager.CanUpgrade(upgradeManager.weaponUpgradeCost) && upgradeManager.weaponLevel < upgradeManager.maxLevel;

        UpdateShipPreview();
    }

    void UpdateShipPreview()
    {
        if (upgradeManager.ships.Length == 0) return;

        shipPreviewImage.sprite = upgradeManager.ships[upgradeManager.currentShipPartIndex].shipSprite;

        bool isOwned = upgradeManager.ships[upgradeManager.currentShipPartIndex].isOwned;
        if (isOwned)
        {
            shipPriceText.text = "Owned";
            buyOrSelectButtonText.text = "Select";
            buyOrSelectButton.interactable = upgradeManager.currentShipPartIndex != upgradeManager.selectedShipIndex;
        }
        else
        {
            int price = upgradeManager.ships[upgradeManager.currentShipPartIndex].price;
            shipPriceText.text = $"{price}";
            buyOrSelectButtonText.text = "Buy";
            buyOrSelectButton.interactable = upgradeManager.CanBuyShip(upgradeManager.currentShipPartIndex);
        }
    }

    void OnBuyOrSelectShip()
    {
        int index = upgradeManager.currentShipPartIndex;
        if (upgradeManager.ships[index].isOwned)
        {
            upgradeManager.SelectShip(index);
        }
        else
        {
            upgradeManager.BuyShip(index);
        }
        UpdateShipPreview();
    }

    void UpgradeSpeed() => upgradeManager.UpgradeSpeed();
    void UpgradeHealth() => upgradeManager.UpgradeHealth();
    void UpgradeWeapon() => upgradeManager.UpgradeWeapon();

    public void OpenPanel()
    {
        upgradePanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    void ClosePanel()
    {
        upgradePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}