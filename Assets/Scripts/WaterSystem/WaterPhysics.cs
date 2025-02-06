using UnityEngine;

public class WaterPhysics : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool inWater = false;
    public float buoyancy = 5f;  // Lực nổi
    public float waterDrag = 2f; // Lực cản trong nước

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void EnterWater()
    {
        inWater = true;
        rb.linearDamping = waterDrag;  // Tăng lực cản khi vào nước
    }

    public void ExitWater()
    {
        inWater = false;
        rb.linearDamping = 0; // Trả lại lực cản ban đầu
    }

    private void FixedUpdate()
    {
        if (inWater)
        {
            rb.AddForce(Vector2.up * buoyancy, ForceMode2D.Force);
        }
    }
}
