using UnityEngine;

[RequireComponent(typeof(MovementHandler))]
[RequireComponent(typeof(RotationHandler))]
[RequireComponent(typeof(ShootingHandler))]
[RequireComponent(typeof(EnergySystem))]
public class SubmarineController : MonoBehaviour
{
    private MovementHandler movementHandler;
    private RotationHandler rotationHandler;
    private ShootingHandler shootingHandler;
    private EnergySystem energySystem;

    private void Awake()
    {
        movementHandler = GetComponent<MovementHandler>();
        rotationHandler = GetComponent<RotationHandler>();
        shootingHandler = GetComponent<ShootingHandler>();
        energySystem = GetComponent<EnergySystem>();
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
        // energySystem.UpdateEnergy(movementHandler.GetCurrentMovement());
    }

    private void HandleInput()
    {
        Vector2 input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        movementHandler.UpdateMovement(input, energySystem.IsEmergencyMode);
    }
}