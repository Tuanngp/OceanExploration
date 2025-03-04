using UnityEngine;

public class BossShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float fireRate = 1.5f;

    private float nextFireTime;
    private Transform player;
    private BossMovement bossMovement;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        bossMovement = GetComponent<BossMovement>();
    }

    void Update()
    {
        if (player == null || bossMovement == null) return;

        if (bossMovement.IsIdle() && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    public void Shoot()
    {
        if (!projectilePrefab || player == null) return;

        Vector3 bossPosition = transform.position;
        Vector3 direction = (player.position - bossPosition).normalized;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);

        GameObject projectile = Instantiate(projectilePrefab, bossPosition, rotation);
        if (projectile.TryGetComponent(out Rigidbody2D rb))
        {
            rb.linearVelocity = direction * projectileSpeed;
        }
    }
}
