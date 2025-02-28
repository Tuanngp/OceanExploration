using UnityEngine;
using UnityEngine.UI;
using TMPro; // Nếu dùng TextMeshPro

public class UpgradeUI : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject upgradePanel;
    public TextMeshProUGUI resourceText;
    public Button speedButton, healthButton, weaponButton;
    public TextMeshProUGUI speedText, healthText, weaponText;
    public Button closeButton;

    [Header("References")]
    public UpgradeManager upgradeManager; // Gán GameObject chứa UpgradeManager

    void Start()
    {
        // Gắn sự kiện cho các nút
        speedButton.onClick.AddListener(UpgradeSpeed);
        healthButton.onClick.AddListener(UpgradeHealth);
        weaponButton.onClick.AddListener(UpgradeWeapon);
        closeButton.onClick.AddListener(ClosePanel);

        // Ẩn panel ban đầu
        upgradePanel.SetActive(false);

        // Gán text trong nút nếu dùng TextMeshPro
        speedText = speedButton.GetComponentInChildren<TextMeshProUGUI>();
        healthText = healthButton.GetComponentInChildren<TextMeshProUGUI>();
        weaponText = weaponButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        // Cập nhật số tài nguyên
        resourceText.text = $"Resources: {upgradeManager.rareResources}";

        // Cập nhật thông tin nút
        speedText.text = $"{upgradeManager.speedUpgradeCost}";
        healthText.text = $"{upgradeManager.healthUpgradeCost}";
        weaponText.text = $"{upgradeManager.weaponUpgradeCost}";

        // Vô hiệu hóa nút nếu không đủ tài nguyên hoặc đạt cấp tối đa
        speedButton.interactable = upgradeManager.CanUpgrade(upgradeManager.speedUpgradeCost) && upgradeManager.speedLevel < upgradeManager.maxLevel;
        healthButton.interactable = upgradeManager.CanUpgrade(upgradeManager.healthUpgradeCost) && upgradeManager.healthLevel < upgradeManager.maxLevel;
        weaponButton.interactable = upgradeManager.CanUpgrade(upgradeManager.weaponUpgradeCost) && upgradeManager.weaponLevel < upgradeManager.maxLevel;
    }

    void UpgradeSpeed() => upgradeManager.UpgradeSpeed();
    void UpgradeHealth() => upgradeManager.UpgradeHealth();
    void UpgradeWeapon() => upgradeManager.UpgradeWeapon();

    public void OpenPanel()
    {
        upgradePanel.SetActive(true);
        Time.timeScale = 0.000000000000001f; // Tạm dừng game khi mở panel (tùy chọn)
    }

    void ClosePanel()
    {
        upgradePanel.SetActive(false);
        Time.timeScale = 1f; // Tiếp tục game
    }
}