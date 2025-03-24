using UnityEditor;
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

    private float nextFireTime;
    private Animator projectileAnimator;
    private UpgradeManager upgradeManager;
    void Start()
    {
        currentDamage = baseDamage;
        projectileAnimator = projectilePrefab.GetComponent<Animator>();
        upgradeManager = GetComponent<UpgradeManager>();
    }

    public void HandleShooting()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        if (!projectilePrefab || !firePoint) return;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Animator projectileAnimator = projectile.GetComponent<Animator>();
        Debug.Log("Index:" + upgradeManager.selectedShipIndex);
        if (projectileAnimator)
        {
            projectileAnimator.SetInteger("fly", upgradeManager.selectedShipIndex);
            Debug.Log("Get fly" + projectileAnimator.GetInteger("fly"));
        }
        else
        {
            Debug.LogError("Projectile không có Animator!");
        }
        if (projectile.TryGetComponent(out Rigidbody2D rb))
        {
            rb.linearVelocity = firePoint.right * projectileSpeed;
        }

        if (projectile.TryGetComponent(out Projectile projectileScript))
        {
            projectileScript.SetDamage(currentDamage);
        }
    }

    public void UpdateDamage(float newDamage)
    {
        currentDamage = newDamage;
    }
}