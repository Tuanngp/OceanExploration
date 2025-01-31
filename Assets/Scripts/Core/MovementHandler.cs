using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementHandler : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float accelerationRate = 2f;
    [SerializeField] private float decelerationRate = 1f;
    [SerializeField] private float maxSpeedMultiplier = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float currentSpeedMultiplier = 1f;

    private void Awake() => rb = GetComponent<Rigidbody2D>();

    public void UpdateMovement(Vector2 input, bool isEmergencyMode)
    {
        moveDirection = input.normalized;

        if (Input.GetKey(KeyCode.LeftShift) && !isEmergencyMode)
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
            rb.MovePosition(rb.position + moveDirection * currentSpeed * Time.fixedDeltaTime);
        }
    }
}