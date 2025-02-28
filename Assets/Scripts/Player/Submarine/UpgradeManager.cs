using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("Resource Settings")]
    public int rareResources = 1000;

    [Header("Upgrade Costs")]
    public int speedUpgradeCost = 50;
    public int healthUpgradeCost = 75;
    public int weaponUpgradeCost = 60;

    [Header("Upgrade Levels")]
    public int speedLevel = 1;
    public int healthLevel = 1;
    public int weaponLevel = 1;
    public int maxLevel = 5;

    [Header("Weapon Damage Settings")]
    [SerializeField] private float damageIncreasePerLevel = 5f;

    [Header("References")]
    private MovementHandler movementHandler;
    private HealthBarController healthBarController;
    private ShootingHandler shootingHandler;

    private void Awake()
    {
        movementHandler = GetComponent<MovementHandler>();
        healthBarController = GetComponent<HealthBarController>();
        shootingHandler = GetComponent<ShootingHandler>();
    }

    public void AddResources(int amount)
    {
        rareResources += amount;
        Debug.Log($"Collected {amount} resources. Total: {rareResources}");
    }

    public bool CanUpgrade(int cost) => rareResources >= cost;

    public void UpgradeSpeed()
    {
        if (speedLevel >= maxLevel || !CanUpgrade(speedUpgradeCost)) return;

        rareResources -= speedUpgradeCost;
        speedLevel++;
        movementHandler.baseSpeed += 2f; // Tăng tốc độ cơ bản
        movementHandler.maxSpeedMultiplier += 0.2f; // Tăng tốc độ tối đa
        speedUpgradeCost += 25; // Tăng chi phí cho lần nâng cấp sau
        Debug.Log($"Speed upgraded to level {speedLevel}");
    }

    public void UpgradeHealth()
    {
        if (healthLevel >= maxLevel || !CanUpgrade(healthUpgradeCost)) return;

        rareResources -= healthUpgradeCost;
        healthLevel++;
        healthBarController.maxHealth += 20f; // Tăng máu tối đa
        healthBarController.IncreaseHealth(20f); // Hồi phục một phần máu
        healthUpgradeCost += 30; // Tăng chi phí cho lần nâng cấp sau
        Debug.Log($"Health upgraded to level {healthLevel}");
    }

    public void UpgradeWeapon()
    {
        if (weaponLevel >= maxLevel || !CanUpgrade(weaponUpgradeCost)) return;

        rareResources -= weaponUpgradeCost;
        weaponLevel++;
        shootingHandler.projectileSpeed += 2f;
        shootingHandler.fireRate *= 0.9f;
        shootingHandler.UpdateDamage(shootingHandler.currentDamage + damageIncreasePerLevel); // Tăng sát thương
        weaponUpgradeCost += 20;
        Debug.Log($"Weapon upgraded to level {weaponLevel}. Damage: {shootingHandler.currentDamage}");
    }
}