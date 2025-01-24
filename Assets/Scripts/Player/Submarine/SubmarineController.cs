using UnityEngine;

public class SubmarineController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float rotationSpeed = 360f;

    [Header("Shooting Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float fireRate = 0.5f;

    [Header("Energy System")]
    [SerializeField] private float maxFuel = 100f;
    [SerializeField] private float currentFuel;
    [SerializeField] private float baseFuelConsumption = 0.5f;
    [SerializeField] private float shootingFuelCost = 1f;
    [SerializeField] private float emergencyFuelThreshold = 20f;
    [SerializeField] private float depthEfficiencyMultiplier = 1.2f;
    [SerializeField] private float depthThreshold = 50f;

    [Header("Speed Settings")]
    [SerializeField] private float maxSpeedMultiplier = 2f;
    [SerializeField] private float accelerationRate = 2f;
    [SerializeField] private float decelerationRate = 1f;

    private Camera mainCamera;
    private Rigidbody2D rb;
    private float nextFireTime;
    private Vector2 moveDirection;
    private Vector2 mousePosition;
    private float currentSpeedMultiplier = 1f;
    private bool isEmergencyMode;

    // UI Events
    public delegate void FuelUpdateHandler(float fuelPercentage);
    public static event FuelUpdateHandler OnFuelUpdate;
    public static event System.Action OnEmergencyFuel;
    public static event System.Action OnNormalFuel;

    private void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        currentFuel = maxFuel;

        if (!firePoint)
        {
            Debug.LogError("Fire Point is not assigned to Submarine!");
        }
    }

    private void Update()
    {
        UpdateEnergySystem();
        HandleInput();
        HandleShooting();
        UpdateUI();
    }

    private void FixedUpdate()
    {
        if (moveDirection != Vector2.zero && currentFuel > 0)
        {
            Move();
        }

        RotateTowardsMouse();
    }

    private void UpdateEnergySystem()
    {
        // Calculate depth multiplier
        float depthMultiplier = transform.position.y < -depthThreshold ?
            depthEfficiencyMultiplier : 1f;

        // Calculate movement fuel consumption
        float movementConsumption = 0f;
        if (moveDirection.magnitude > 0)
        {
            movementConsumption = baseFuelConsumption * currentSpeedMultiplier * depthMultiplier * Time.deltaTime;
        }

        // Apply fuel consumption
        if (currentFuel > 0)
        {
            currentFuel = Mathf.Max(0, currentFuel - movementConsumption);
        }

        // Check emergency status
        CheckEmergencyStatus();
    }

    private void HandleInput()
    {
        moveDirection = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        mousePosition = GetMousePosition();

        // Handle speed boost
        if (Input.GetKey(KeyCode.LeftShift) && !isEmergencyMode && currentFuel > emergencyFuelThreshold)
        {
            currentSpeedMultiplier = Mathf.Min(
                currentSpeedMultiplier + accelerationRate * Time.deltaTime,
                maxSpeedMultiplier
            );
        }
        else
        {
            currentSpeedMultiplier = Mathf.Max(
                currentSpeedMultiplier - decelerationRate * Time.deltaTime,
                1f
            );
        }
    }

    private Vector2 GetMousePosition()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Move()
    {
        float currentSpeed = baseSpeed * currentSpeedMultiplier;
        rb.MovePosition(rb.position + moveDirection * currentSpeed * Time.fixedDeltaTime);
    }

    private void RotateTowardsMouse()
    {
        Vector2 direction = mousePosition - rb.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float currentAngle = rb.rotation;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(newAngle);

        // Flip submarine sprite based on angle
        if (newAngle > 90 || newAngle < -90)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && currentFuel >= shootingFuelCost)
        {
            Shoot();
            currentFuel -= shootingFuelCost;
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        if (projectilePrefab && firePoint)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            if (projectile.TryGetComponent(out Rigidbody2D projectileRb))
            {
                Vector2 shootDirection = firePoint.right;
                projectileRb.linearVelocity = shootDirection * projectileSpeed;
            }
        }
    }

    private void CheckEmergencyStatus()
    {
        if (currentFuel <= emergencyFuelThreshold && !isEmergencyMode)
        {
            isEmergencyMode = true;
            OnEmergencyFuel?.Invoke();
            maxSpeedMultiplier = 1f; // Limit max speed in emergency
        }
        else if (currentFuel > emergencyFuelThreshold && isEmergencyMode)
        {
            isEmergencyMode = false;
            OnNormalFuel?.Invoke();
            maxSpeedMultiplier = 2f; // Restore normal max speed
        }
    }

    private void UpdateUI()
    {
        float fuelPercentage = (currentFuel / maxFuel) * 100f;
        OnFuelUpdate?.Invoke(fuelPercentage);
    }

    public void AddFuel(float amount)
    {
        currentFuel = Mathf.Min(currentFuel + amount, maxFuel);
    }

    public float GetFuelPercentage()
    {
        return (currentFuel / maxFuel) * 100f;
    }
}