using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    [SerializeField] private Transform player; // Nhân vật (tàu ngầm)
    [SerializeField] private float parallaxSpeed = 0.5f; // Tốc độ cuộn background
    private Vector2 startPosition; // Vị trí ban đầu của background
    private float backgroundWidth; // Chiều rộng của background (sprite)

    private void Start()
    {
        // Lưu vị trí ban đầu và lấy kích thước background
        startPosition = transform.position;
        backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        // Di chuyển background ngược lại với hướng của nhân vật
        float distance = player.position.x * parallaxSpeed;

        // Thay đổi vị trí background
        transform.position = new Vector3(startPosition.x + distance, transform.position.y, transform.position.z);

        // Khi nhân vật vượt qua giới hạn, lặp background
        if (player.position.x > startPosition.x + backgroundWidth)
        {
            startPosition.x += backgroundWidth; // Di chuyển background qua bên phải
        }
        else if (player.position.x < startPosition.x - backgroundWidth)
        {
            startPosition.x -= backgroundWidth; // Di chuyển background qua bên trái
        }
    }
}
