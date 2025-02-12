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
        if (projectile.TryGetComponent(out Rigidbody2D rb))
        {
            rb.linearVelocity = firePoint.right * projectileSpeed;
        }
    }
}