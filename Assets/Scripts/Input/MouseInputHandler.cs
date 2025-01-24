using UnityEngine;

public class MouseInputHandler : MonoBehaviour
{
    private Camera mainCamera;
    private IMoveable movement;
    private IShootable shooting;

    private void Awake()
    {
        mainCamera = Camera.main;
        // Lấy references từ các components
        movement = GetComponentInChildren<IMoveable>();
        shooting = GetComponentInChildren<IShootable>();
    }

    private void Update()
    {
        HandleMovementInput();
        HandleShootingInput();
    }

    private void HandleMovementInput()
    {
        // Chuyển đổi vị trí chuột từ screen space sang world space
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - (Vector2)transform.position;

        // Tính góc xoay
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Tính khoảng cách để di chuyển
        float distanceToMouse = direction.magnitude;
        Vector2 moveDirection = Vector2.zero;

        if (distanceToMouse > 1f) // Dead zone
        {
            moveDirection = direction.normalized;
        }

        // Áp dụng chuyển động
        movement?.Move(moveDirection);
        movement?.Rotate(angle - transform.rotation.eulerAngles.z);
    }

    private void HandleShootingInput()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            Vector2 targetPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            shooting?.Shoot(targetPosition);
        }
    }
}