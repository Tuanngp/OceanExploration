using UnityEngine;

[RequireComponent(typeof(MovementHandler))]
[RequireComponent(typeof(RotationHandler))]
[RequireComponent(typeof(ShootingHandler))]
[RequireComponent(typeof(UpgradeManager))]
[RequireComponent(typeof(UpgradeUI))]

public class SubmarineController : MonoBehaviour
{
    private MovementHandler movementHandler;
    private RotationHandler rotationHandler;
    private ShootingHandler shootingHandler;
    private UpgradeManager upgradeManager;
    private UpgradeUI upgradeUI;
    private void Awake()
    {
        movementHandler = GetComponent<MovementHandler>();
        rotationHandler = GetComponent<RotationHandler>();
        shootingHandler = GetComponent<ShootingHandler>();
        upgradeManager = GetComponent<UpgradeManager>();
        upgradeUI = GetComponent<UpgradeUI>();
    }

    private void Update()
    {
        HandleInput();
        shootingHandler.HandleShooting();
        if (Input.GetKeyDown(KeyCode.U))
        {
            upgradeUI.OpenPanel();
        }
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
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            ScoreManager.instance.AddScore(10);
        }
        else if (other.CompareTag("RareResource")) // Thêm tag cho tài nguyên quý hiếm
        {
            Destroy(other.gameObject);
            upgradeManager.AddResources(10); // Thu thập 10 tài nguyên
        }
    }
}