using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementHandler : MonoBehaviour
{
    public Transform background;  // Kéo background vào đây từ Unity
    private Vector2 minBounds, maxBounds;

    [Header("Movement Settings")]
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float accelerationRate = 2f;
    [SerializeField] private float decelerationRate = 1f;
    [SerializeField] private float maxSpeedMultiplier = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float currentSpeedMultiplier = 1f;
    private float playerHalfWidth, playerHalfHeight;

    private void Awake() => rb = GetComponent<Rigidbody2D>();

    private void Start()
    {
        // Lấy kích thước của background
        SpriteRenderer bgRenderer = background.GetComponent<SpriteRenderer>();
        SpriteRenderer playerRenderer = GetComponentInChildren<SpriteRenderer>();


        if (bgRenderer != null && playerRenderer != null)
        {
            float bgHalfWidth = bgRenderer.bounds.size.x / 2;
            float bgHalfHeight = bgRenderer.bounds.size.y / 2;

            // Lấy nửa chiều rộng và chiều cao của nhân vật
            playerHalfWidth = playerRenderer.bounds.size.x / 2;
            playerHalfHeight = playerRenderer.bounds.size.y / 2;
            Debug.Log(playerHalfWidth + " " + playerHalfHeight);
            // Giới hạn mới có tính đến kích thước nhân vật
            minBounds = new Vector2(
                background.position.x - bgHalfWidth + playerHalfWidth,
                background.position.y - bgHalfHeight + playerHalfHeight
            );

            maxBounds = new Vector2(
                background.position.x + bgHalfWidth - playerHalfWidth,
                background.position.y + bgHalfHeight - playerHalfHeight
            );
        }
    }

    public void UpdateMovement(Vector2 input)
    {
        moveDirection = input.normalized;

        if (Input.GetKey(KeyCode.LeftShift))
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

    public void FixedUpdateMovement()
    {
        if (moveDirection != Vector2.zero)
        {
            float currentSpeed = baseSpeed * currentSpeedMultiplier;
            Vector2 newPosition = rb.position + moveDirection * currentSpeed * Time.fixedDeltaTime;

            // Giới hạn vị trí nhân vật
            newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
            newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);

            rb.MovePosition(newPosition);
        }
    }
}
