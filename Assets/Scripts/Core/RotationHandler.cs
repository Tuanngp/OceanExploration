using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RotationHandler : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 360f;

    private Camera mainCamera;
    private Rigidbody2D rb;

    private void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    public void RotateTowardsMouse()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - rb.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float currentAngle = rb.rotation;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(newAngle);

        UpdateSpriteFlip(newAngle);
    }

    private void UpdateSpriteFlip(float angle)
    {
        transform.localScale = (angle > 90 || angle < -90)
            ? new Vector3(1, -1, 1)
            : new Vector3(1, 1, 1);
    }
}