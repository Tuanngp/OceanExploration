using UnityEngine;
using System.Linq; // Để sử dụng LINQ

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Nhân vật để camera theo dõi
    public Transform backgroundContainer; // GameObject chứa tất cả các ảnh nền
    public float smoothSpeed = 0.1f; // Độ mượt khi camera di chuyển

    private Vector2 minLimit;
    private Vector2 maxLimit;

    private void Start()
    {
        if (backgroundContainer == null)
        {
            Debug.LogError("Chưa gán BackgroundContainer!");
            return;
        }

        // Lấy tất cả SpriteRenderer trong backgroundContainer
        SpriteRenderer[] sprites = backgroundContainer.GetComponentsInChildren<SpriteRenderer>();

        if (sprites.Length == 0)
        {
            Debug.LogError("Không tìm thấy ảnh nền nào trong BackgroundContainer!");
            return;
        }

        // Tìm min Y (cạnh dưới) và max Y (cạnh trên) của tất cả các sprite
        minLimit.y = sprites.Min(sprite => sprite.bounds.min.y);
        maxLimit.y = sprites.Max(sprite => sprite.bounds.max.y);

        // Giới hạn theo chiều ngang dựa trên vị trí nhân vật hoặc bản đồ
        minLimit.x = player.position.x - 10f; // Hoặc lấy từ map
        maxLimit.x = player.position.x + 10f; // Hoặc lấy từ map
    }

    private void LateUpdate()
    {
        if (player == null) return;

        // Lấy vị trí mong muốn của camera theo nhân vật
        Vector3 desiredPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

        // Giới hạn camera trong khoảng min/max
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minLimit.x, maxLimit.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minLimit.y, maxLimit.y);

        // Dịch chuyển Camera mượt mà
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
