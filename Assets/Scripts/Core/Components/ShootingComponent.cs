using UnityEngine;

public class ShootingComponent : MonoBehaviour, IShootable
{
    [SerializeField] private GameObject projectilePrefab; // Kéo prefab đạn vào đây
    [SerializeField] private Transform firePoint; // Reference đến điểm bắn
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float fireRate = 0.5f; // Thời gian giữa các lần bắn

    private float nextFireTime;

    private void Start()
    {
        // Kiểm tra xem đã set firePoint chưa
        if (firePoint == null)
        {
            Debug.LogError("FirePoint is not assigned to ShootingComponent!");
        }
    }

    public void Shoot(Vector2 targetPosition)
    {
        if (Time.time < nextFireTime) return;

        // Tạo đạn tại điểm bắn
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Tính hướng bắn
        Vector2 direction = ((Vector3)targetPosition - firePoint.position).normalized;

        // Áp dụng vận tốc cho đạn
        if (projectile.TryGetComponent(out Rigidbody2D rb))
        {
            rb.linearVelocity = direction * projectileSpeed;
        }

        // Cập nhật thời gian bắn tiếp theo
        nextFireTime = Time.time + fireRate;
    }
}
