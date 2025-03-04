using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;
    public float detectionRange = 60f;
    private Rigidbody2D rb;
    private BossAnimation bossAnimation;
    private bool isMoving = false;
    private float moveTime = 2f;
    private float moveTimer;
    private bool isIdle = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bossAnimation = GetComponent<BossAnimation>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > detectionRange)
        {
            // Boss vẫn ở trạng thái IDLE khi chưa phát hiện tàu
            isMoving = false;
            isIdle = true;
            rb.linearVelocity = Vector2.zero;
            bossAnimation.SetMoving(false);
            return;
        }

        // Khi phát hiện tàu và Boss đang ở trạng thái IDLE, bắt đầu di chuyển
        if (isIdle)
        {
            StartMoving();
        }

        if (isMoving)
        {
            MoveToAvoidProjectiles();
            moveTimer -= Time.deltaTime;

            if (moveTimer <= 0)
            {
                StopAndShoot();
            }
        }
    }

    void MoveToAvoidProjectiles()
    {
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");

        Vector2 dodgeDirection = Vector2.zero;
        float minDistance = float.MaxValue;
        GameObject nearestProjectile = null;

        foreach (GameObject projectile in projectiles)
        {
            float distanceToProjectile = Vector2.Distance(transform.position, projectile.transform.position);
            if (distanceToProjectile < minDistance)
            {
                minDistance = distanceToProjectile;
                nearestProjectile = projectile;
            }
        }

        if (nearestProjectile != null && minDistance < 10f)
        {
            Vector2 directionAway = (transform.position - nearestProjectile.transform.position).normalized;
            dodgeDirection = directionAway;
        }
        else
        {
            dodgeDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

        rb.linearVelocity = dodgeDirection * speed;
        bossAnimation.SetMoving(true);
    }

    void StopAndShoot()
    {
        rb.linearVelocity = Vector2.zero;
        isMoving = false;
        isIdle = true;
        bossAnimation.SetMoving(false);
        bossAnimation.TriggerAttack();
        // Kích hoạt bắn đạn sau khi dừng lại
        GetComponent<BossShooting>()?.Shoot();
    }

    void StartMoving()
    {
        isMoving = true;
        isIdle = false;
        moveTimer = moveTime;
    }

    public bool IsIdle()
    {
        return isIdle;
    }
}
