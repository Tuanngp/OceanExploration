using UnityEngine;
[RequireComponent(typeof(HealthBarController))]
public class ShootingHandler : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] public GameObject projectilePrefab;
    [SerializeField] public Transform firePoint;
    [SerializeField] public float projectileSpeed = 10f;
    [SerializeField] public float fireRate = 0.5f;
    [SerializeField] private float baseDamage = 10f;
    public float currentDamage;
    public float currentFireRate;
    public float currentProjectileSpeed;

    private float nextFireTime;
    private UpgradeManager upgradeManager;
    void Start()
    {
        currentDamage = baseDamage;
        currentFireRate = fireRate;
        currentProjectileSpeed = projectileSpeed;
        upgradeManager = GetComponent<UpgradeManager>();
    }

    public void HandleShooting()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + currentFireRate;
        }
    }

    private void Shoot()
    {
        if (!projectilePrefab || !firePoint) return;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Animator projectileAnimator = projectile.GetComponent<Animator>();
        if (projectileAnimator)
        {
            projectileAnimator.SetInteger("fly", upgradeManager.selectedShipIndex);
        }
        if (projectile.TryGetComponent(out Rigidbody2D rb))
        {
            rb.linearVelocity = firePoint.right * currentProjectileSpeed;
        }

        if (projectile.TryGetComponent(out Projectile projectileScript))
        {
            projectileScript.SetDamage(currentDamage);
        }
    }
}