using UnityEngine;
[RequireComponent(typeof(HealthBarController))]
public class ShootingHandler : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float fireRate = 0.5f;

    private float nextFireTime;
    private EnergySystem energySystem;

    private HealthBarController healthBarController;

    void Start()
    {
        healthBarController = GetComponent<HealthBarController>();
    }

    private void Awake() => energySystem = GetComponent<EnergySystem>();

    public void HandleShooting()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && energySystem.CanShoot())
        {
            Shoot();
            energySystem.ConsumeEnergyForShooting();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        if (!projectilePrefab || !firePoint) return;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        if (projectile.TryGetComponent(out Rigidbody2D rb))
        {
            rb.linearVelocity = firePoint.right * projectileSpeed;
            healthBarController.DecreaseMana(2 * Time.deltaTime);
        }
    }
}