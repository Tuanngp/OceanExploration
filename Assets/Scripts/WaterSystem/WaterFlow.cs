using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    public Vector2 flowDirection = new Vector2(1f, 0f); // Hướng dòng chảy
    public float flowStrength = 1f;  // Độ mạnh của dòng chảy

    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.AddForce(flowDirection * flowStrength);
        }
    }
}
