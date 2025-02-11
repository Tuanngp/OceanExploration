using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Nhân vật cần theo dõi
    public Transform backgroundParent; // Object chứa tất cả ảnh nền
    public float smoothSpeed = 0.1f; // Độ mượt khi camera di chuyển

    private float minY, maxY;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        CalculateBackgroundBounds();
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Lấy vị trí camera hiện tại
        Vector3 newPosition = transform.position;
        newPosition.x = target.position.x;
        newPosition.y = Mathf.Clamp(target.position.y, minY, maxY);

        // Di chuyển camera mượt mà
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed);
    }

    void CalculateBackgroundBounds()
    {
        if (backgroundParent == null) return;

        float minYValue = float.MaxValue, maxYValue = float.MinValue;

        foreach (Transform child in backgroundParent)
        {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer)
            {
                Bounds bounds = spriteRenderer.bounds;
                minYValue = Mathf.Min(minYValue, bounds.min.y);
                maxYValue = Mathf.Max(maxYValue, bounds.max.y);
            }
        }

        // Lấy kích thước camera để tránh vượt ra ngoài biên
        float camHeight = cam.orthographicSize;

        minY = minYValue + camHeight; // Giới hạn dưới
        maxY = maxYValue - camHeight; // Giới hạn trên
    }
}
