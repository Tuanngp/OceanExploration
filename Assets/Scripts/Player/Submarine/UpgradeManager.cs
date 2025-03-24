using UnityEngine;

[System.Serializable]
public class ShipData
{
    public Sprite shipSprite;
    public int price;
    public bool isOwned;
    public float baseSpeed;
    public float baseHealth;
    public float baseDamage;
}

public class UpgradeManager : MonoBehaviour
{
    [Header("Resource Settings")]
    public int rareResources = 100;

    [Header("Upgrade Costs")]
    public int speedUpgradeCost = 50;
    public int healthUpgradeCost = 75;
    public int weaponUpgradeCost = 60;

    [Header("Upgrade Levels")]
    public int speedLevel = 1;
    public int healthLevel = 1;
    public int weaponLevel = 1;
    public int maxLevel = 5;

    [Header("Stat Values")]
    [SerializeField] private float speedIncreasePerLevel = 2f;
    [SerializeField] private float healthIncreasePerLevel = 20f;
    [SerializeField] private float damageIncreasePerLevel = 5f;

    [Header("Ship Parts")]
    public SpriteRenderer shipSpriteRenderer;
    public ShipData[] ships;
    public int currentShipPartIndex = 0;
    public int selectedShipIndex = 0;

    [Header("References")]
    private MovementHandler movementHandler;
    private HealthBarController healthBarController;
    private ShootingHandler shootingHandler;

    private float currentBaseSpeed;
    private float currentBaseHealth;
    private float currentBaseDamage;

    private void Awake()
    {
        movementHandler = GetComponent<MovementHandler>();
        healthBarController = GetComponent<HealthBarController>();
        shootingHandler = GetComponent<ShootingHandler>();

        if (ships.Length > 0)
        {
            ships[0].isOwned = true;
            selectedShipIndex = 0;
            currentBaseSpeed = ships[0].baseSpeed;
            currentBaseHealth = ships[0].baseHealth;
            currentBaseDamage = ships[0].baseDamage;
        }

        UpdateShipStats();
        UpdateShipPart();
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
        speedUpgradeCost += 25;
        UpdateShipStats();
    }

    public void UpgradeHealth()
    {
        if (healthLevel >= maxLevel || !CanUpgrade(healthUpgradeCost)) return;

        rareResources -= healthUpgradeCost;
        healthLevel++;
        healthUpgradeCost += 30;
        UpdateShipStats();
    }

    public void UpgradeWeapon()
    {
        if (weaponLevel >= maxLevel || !CanUpgrade(weaponUpgradeCost)) return;

        rareResources -= weaponUpgradeCost;
        weaponLevel++;
        weaponUpgradeCost += 20;
        UpdateShipStats();
        Debug.Log($"Weapon upgraded to level {weaponLevel}. Damage: {shootingHandler.currentDamage}");
    }

    public void NextShipPart()
    {
        currentShipPartIndex = (currentShipPartIndex + 1) % ships.Length;
        UpdateShipPart();
    }

    public void PreviousShipPart()
    {
        currentShipPartIndex = (currentShipPartIndex - 1 + ships.Length) % ships.Length;
        UpdateShipPart();
    }

    private void UpdateShipPart()
    {
        if (ships.Length > 0 && currentShipPartIndex == selectedShipIndex)
        {
            shipSpriteRenderer.sprite = ships[currentShipPartIndex].shipSprite;
        }
    }

    public bool CanBuyShip(int index)
    {
        return rareResources >= ships[index].price && !ships[index].isOwned;
    }

    public void BuyShip(int index)
    {
        if (!CanBuyShip(index)) return;

        rareResources -= ships[index].price;
        ships[index].isOwned = true;
    }

    public void SelectShip(int index)
    {
        if (!ships[index].isOwned) return;

        selectedShipIndex = index;
        currentShipPartIndex = index;
        shipSpriteRenderer.sprite = ships[selectedShipIndex].shipSprite;

        currentBaseSpeed = ships[index].baseSpeed;
        currentBaseHealth = ships[index].baseHealth;
        currentBaseDamage = ships[index].baseDamage;

        UpdateShipStats();
    }

    public void UpdateShipStats()
    {
        float newSpeed = currentBaseSpeed + (speedLevel - 1) * speedIncreasePerLevel;
        float newHealth = currentBaseHealth + (healthLevel - 1) * healthIncreasePerLevel;
        float newDamage = currentBaseDamage + (weaponLevel - 1) * damageIncreasePerLevel;

        movementHandler.baseSpeed = newSpeed;
        movementHandler.maxSpeedMultiplier = 1f + (speedLevel - 1) * 0.2f;

        healthBarController.maxHealth = newHealth;
        
        shootingHandler.currentDamage = newDamage;
        shootingHandler.currentProjectileSpeed = 5f + (weaponLevel - 1) * 2f;
        shootingHandler.currentFireRate = 0.5f - (weaponLevel - 1) * 0.01f;
    }

    public float GetCurrentSpeed() => currentBaseSpeed + (speedLevel - 1) * speedIncreasePerLevel;
    public float GetCurrentHealth() => currentBaseHealth + (healthLevel - 1) * healthIncreasePerLevel;
    public float GetCurrentDamage() => currentBaseDamage + (weaponLevel - 1) * damageIncreasePerLevel;
}