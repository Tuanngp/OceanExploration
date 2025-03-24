using System;
using System.Collections.Generic;

[Serializable]
public class SubmarineData
{
    // Resource and currency
    public int rareResources;
    public int score;
    
    // Upgrade levels
    public int speedLevel;
    public int healthLevel;
    public int weaponLevel;
    
    // Upgrade costs
    public int speedUpgradeCost;
    public int healthUpgradeCost;
    public int weaponUpgradeCost;
    
    // Ship data
    public int currentShipPartIndex;
    public int selectedShipIndex;
    public List<ShipInfo> ownedShips = new List<ShipInfo>();
    
    // Position data (optional, for respawning)
    public float positionX;
    public float positionY;
    
    // Health data
    public float currentHealth;
    public float maxHealth;
    
    // Weapon data
    public float projectileSpeed;
    public float fireRate;
    public float currentDamage;
    
    // Movement data
    public float movementSpeed;

    
    [Serializable]
    public class ShipInfo
    {
        public int shipIndex;
        public bool isOwned;
        
        public ShipInfo(int index, bool owned)
        {
            shipIndex = index;
            isOwned = owned;
        }
    }
} 