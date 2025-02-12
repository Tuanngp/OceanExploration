using UnityEngine;

[RequireComponent(typeof(MovementHandler))]
[RequireComponent(typeof(RotationHandler))]
[RequireComponent(typeof(ShootingHandler))]
public class SubmarineController : MonoBehaviour
{
    private MovementHandler movementHandler;
    private RotationHandler rotationHandler;
    private ShootingHandler shootingHandler;

    private void Awake()
    {
        movementHandler = GetComponent<MovementHandler>();
        rotationHandler = GetComponent<RotationHandler>();
        shootingHandler = GetComponent<ShootingHandler>();
    }

    private void Update()
    {
        HandleInput();
        shootingHandler.HandleShooting();
    }

    private void FixedUpdate()
    {
        rotationHandler.RotateTowardsMouse();
        movementHandler.FixedUpdateMovement();
    }

    private void HandleInput()
    {
        Vector2 input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        movementHandler.UpdateMovement(input);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin")) // Kiểm tra nếu va chạm với coin
        {
            Destroy(other.gameObject); // Xóa coin khi chạm vào
            ScoreManager.instance.AddScore(10);
        }
    }
}