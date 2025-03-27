using UnityEngine;

public class BloodDrop : MonoBehaviour
{
    private Rigidbody2D rb;
    private HealthBarController healthBarController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            healthBarController = other.GetComponent<HealthBarController>();
            Debug.Log("Player detected! Increasing health." + healthBarController);
            healthBarController.IncreaseHealth(20); // Tăng máu cho tàu
            Destroy(gameObject); // Hủy giọt máu
        }
    }
}
