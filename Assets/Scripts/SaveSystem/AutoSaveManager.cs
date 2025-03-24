using UnityEngine;
using System.Collections;

public class AutoSaveManager : MonoBehaviour
{
    public static AutoSaveManager Instance { get; private set; }
    
    [Header("Auto Save Settings")]
    [SerializeField] private float autoSaveInterval = 60f; // Save every minute by default
    [SerializeField] private bool saveOnExit = true;
    
    private SubmarineController submarine;
    private UpgradeManager upgradeManager;
    private MovementHandler movementHandler;
    private ShootingHandler shootingHandler;
    private HealthBarController healthBarController;
    
    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Obsolete]
    private void Start()
    {
        // Find the submarine in the scene
        submarine = FindObjectOfType<SubmarineController>();
        
        if (submarine != null)
        {
            upgradeManager = submarine.GetComponent<UpgradeManager>();
            movementHandler = submarine.GetComponent<MovementHandler>();
            shootingHandler = submarine.GetComponent<ShootingHandler>();
            healthBarController = submarine.GetComponent<HealthBarController>();
            
            // Load any existing save data
            LoadGame();
            
            // Start auto-save coroutine
            StartCoroutine(AutoSaveCoroutine());
        }
        else
        {
            Debug.LogWarning("SubmarineController not found in scene");
        }
    }
    
    private IEnumerator AutoSaveCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveInterval);
            SaveGame();
            Debug.Log("Auto-saved game data");
        }
    }
    
    private void OnApplicationQuit()
    {
        if (saveOnExit && submarine != null)
        {
            SaveGame();
            Debug.Log("Saved game on exit");
        }
    }
    
    public void SaveGame()
    {
        if (submarine == null) return;
        
        SubmarineData data = new SubmarineData();
        
        // Get score from ScoreManager if it exists
        if (ScoreManager.instance != null)
        {
            data.score = ScoreManager.instance.GetScore();
        }
        
        // Save upgrade manager data
        data.rareResources = upgradeManager.rareResources;
        data.speedLevel = upgradeManager.speedLevel;
        data.healthLevel = upgradeManager.healthLevel;
        data.weaponLevel = upgradeManager.weaponLevel;
        data.speedUpgradeCost = upgradeManager.speedUpgradeCost;
        data.healthUpgradeCost = upgradeManager.healthUpgradeCost;
        data.weaponUpgradeCost = upgradeManager.weaponUpgradeCost;
        data.selectedShipIndex = upgradeManager.selectedShipIndex;
        data.currentShipPartIndex = upgradeManager.currentShipPartIndex;
        
        // Save owned ships
        for (int i = 0; i < upgradeManager.ships.Length; i++)
        {
            data.ownedShips.Add(new SubmarineData.ShipInfo(i, upgradeManager.ships[i].isOwned));
        }
        
        // Save position
        data.positionX = submarine.transform.position.x;
        data.positionY = submarine.transform.position.y;
        
        // Save health data
        data.currentHealth = upgradeManager.GetCurrentHealth();
        data.maxHealth = healthBarController.maxHealth;
        
        // Save weapon data
        data.projectileSpeed = shootingHandler.projectileSpeed;
        data.fireRate = shootingHandler.currentFireRate;
        data.currentDamage = upgradeManager.GetCurrentDamage();
        data.movementSpeed = upgradeManager.GetCurrentSpeed();
        
        // Save data to disk
        SaveManager.SaveGame(data);
    }
    
    public void LoadGame()
    {
        if (!SaveManager.SaveExists() || submarine == null) return;
        
        SubmarineData data = SaveManager.LoadGame();
        Debug.Log("Loaded game data");
        // Load score if ScoreManager exists
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.SetScore(data.score);
        }
        
        // Load upgrade manager data
        upgradeManager.rareResources = data.rareResources;
        upgradeManager.speedLevel = data.speedLevel;
        upgradeManager.healthLevel = data.healthLevel;
        upgradeManager.weaponLevel = data.weaponLevel;
        upgradeManager.speedUpgradeCost = data.speedUpgradeCost;
        upgradeManager.healthUpgradeCost = data.healthUpgradeCost;
        upgradeManager.weaponUpgradeCost = data.weaponUpgradeCost;
        upgradeManager.currentShipPartIndex = data.currentShipPartIndex;
        upgradeManager.selectedShipIndex = data.selectedShipIndex;

        // Load owned ships
        foreach (SubmarineData.ShipInfo shipInfo in data.ownedShips)
        {
            if (shipInfo.shipIndex < upgradeManager.ships.Length)
            {
                upgradeManager.ships[shipInfo.shipIndex].isOwned = shipInfo.isOwned;
            }
        }
        
        // Update ship stats (this will apply all the loaded upgrades)
        upgradeManager.SelectShip(data.selectedShipIndex);
        
        // Load position (optional - could be used for respawning)
        submarine.transform.position = new Vector3(data.positionX, data.positionY, submarine.transform.position.z);

        upgradeManager.UpdateShipStats();
        
        Debug.Log("Game data loaded successfully :");
    }
    
    public void DeleteSaveData()
    {
        SaveManager.DeleteSave();
        Debug.Log("Save data deleted");
    }
} 