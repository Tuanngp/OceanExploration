// Gắn script này vào Movement GameObject
using UnityEngine;

public class MovementComponent : MonoBehaviour, IMoveable
{
    [SerializeField] private float moveSpeed = 5f; // Điều chỉnh trong Inspector
    [SerializeField] private float rotationSpeed = 200f;

    private Rigidbody2D rb;

    private void Awake()
    {
        // Lấy reference đến Rigidbody2D của parent
        rb = GetComponentInParent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        // Di chuyển theo hướng đã chuẩn hóa
        Vector2 movement = direction.normalized * (moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + movement);
    }

    public void Rotate(float angle)
    {
        // Xoay theo góc đã tính
        float rotation = -angle * rotationSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotation);
    }
}