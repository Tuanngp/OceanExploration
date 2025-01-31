using UnityEngine;

public class FuelPickup : MonoBehaviour
{
    [SerializeField] private float fuelAmount = 30f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out SubmarineController submarine))
        {
            // submarine.AddFuel(fuelAmount);
            Destroy(gameObject);
        }
    }
}