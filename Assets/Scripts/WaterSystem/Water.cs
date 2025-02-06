using UnityEngine;

public class Water : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>())
        {
            collision.GetComponent<WaterPhysics>()?.EnterWater();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>())
        {
            collision.GetComponent<WaterPhysics>()?.ExitWater();
        }
    }
}
