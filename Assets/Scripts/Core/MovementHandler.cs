using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementHandler : MonoBehaviour
{
    public Transform background;
    private Vector2 minBounds, maxBounds;
    private Animator animator;

    [Header("Movement Settings")]
    [SerializeField] public float baseSpeed = 20f;
    [SerializeField] private float accelerationRate = 2f;
    [SerializeField] private float decelerationRate = 1f;
    [SerializeField] public float maxSpeedMultiplier = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float currentSpeedMultiplier = 1f;
    private float playerHalfWidth, playerHalfHeight;
    private HealthBarController healthBarController;
    private UpgradeManager upgradeManager;
    private void Awake() => rb = GetComponent<Rigidbody2D>();

    private void Start()
    {
        healthBarController = GetComponent<HealthBarController>();
        animator = GetComponent<Animator>();
        upgradeManager = GetComponent<UpgradeManager>();
        // Lấy kích thước nhân vật
        SpriteRenderer playerRenderer = GetComponentInChildren<SpriteRenderer>();
        if (playerRenderer != null)
        {
            playerHalfWidth = playerRenderer.bounds.extents.x;
            playerHalfHeight = playerRenderer.bounds.extents.y;
        }
    }

    private void CalculateBounds()
    {
        if (background == null) return;

        var spriteRenderers = background.GetComponentsInChildren<SpriteRenderer>();
        if (spriteRenderers.Length == 0) return;

        float minX = spriteRenderers.Min(sr => sr.bounds.min.x);
        float maxX = spriteRenderers.Max(sr => sr.bounds.max.x);
        float minY = spriteRenderers.Min(sr => sr.bounds.min.y);
        float maxY = spriteRenderers.Max(sr => sr.bounds.max.y);

        // Giới hạn có tính đến kích thước nhân vật
        minBounds = new Vector2(minX + playerHalfWidth, minY + playerHalfHeight);
        maxBounds = new Vector2(maxX - playerHalfWidth, maxY - playerHalfHeight);
    }

    public void UpdateMovement(Vector2 input)
    {
        moveDirection = input.normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetTrigger("boost" + upgradeManager.selectedShipIndex);
            if (healthBarController.currentMana <= 0f) return;
            currentSpeedMultiplier = Mathf.Min(
                currentSpeedMultiplier + accelerationRate * Time.deltaTime,
                maxSpeedMultiplier
            );
            healthBarController.DecreaseMana(2 * Time.deltaTime);
        }
        else
        {
            currentSpeedMultiplier = Mathf.Max(
                currentSpeedMultiplier - decelerationRate * Time.deltaTime,
                1f
            );
        }
    }

    public void FixedUpdateMovement()
    {
        // Tìm giới hạn tổng thể từ các ảnh nền
        CalculateBounds();
        if (moveDirection != Vector2.zero)
        {
            float currentSpeed = baseSpeed * currentSpeedMultiplier;
            Vector2 newPosition = rb.position + moveDirection * currentSpeed * Time.fixedDeltaTime;

            // Giới hạn vị trí nhân vật
            newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);
            newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, float.MaxValue);

            rb.MovePosition(newPosition);
        }
    }
}
