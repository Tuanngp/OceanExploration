using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    [Header("Energy Settings")]
    [SerializeField] private float maxFuel = 100f;
    [SerializeField] private float baseFuelConsumption = 0.5f;
    [SerializeField] private float shootingFuelCost = 1f;
    [SerializeField] private float emergencyFuelThreshold = 20f;
    [SerializeField] private float depthEfficiencyMultiplier = 1.2f;
    [SerializeField] private float depthThreshold = 50f;

    private float currentFuel;
    public bool IsEmergencyMode { get; private set; }

    // Events
    public delegate void FuelUpdateHandler(float fuelPercentage);
    public event FuelUpdateHandler OnFuelUpdate;
    public event System.Action OnEmergencyFuel;
    public event System.Action OnNormalFuel;

    private void Start() => currentFuel = maxFuel;

    public void UpdateEnergy(Vector2 movementInput)
    {
        float depthMultiplier = transform.position.y < -depthThreshold ? depthEfficiencyMultiplier : 1f;
        float consumption = movementInput.magnitude > 0
            ? baseFuelConsumption * depthMultiplier * Time.deltaTime
            : 0;

        currentFuel = Mathf.Max(0, currentFuel - consumption);
        CheckEmergencyStatus();
        UpdateUI();
    }

    public bool CanShoot() => currentFuel >= shootingFuelCost;

    public void ConsumeEnergyForShooting() => currentFuel -= shootingFuelCost;

    private void CheckEmergencyStatus()
    {
        if (currentFuel <= emergencyFuelThreshold && !IsEmergencyMode)
        {
            IsEmergencyMode = true;
            OnEmergencyFuel?.Invoke();
        }
        else if (currentFuel > emergencyFuelThreshold && IsEmergencyMode)
        {
            IsEmergencyMode = false;
            OnNormalFuel?.Invoke();
        }
    }

    private void UpdateUI() => OnFuelUpdate?.Invoke((currentFuel / maxFuel) * 100f);

    public void AddFuel(float amount) => currentFuel = Mathf.Min(currentFuel + amount, maxFuel);
}