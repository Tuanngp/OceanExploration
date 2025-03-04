using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public Transform player; // Nhân vật
    public Transform[] backgrounds; // Các phần nền
    private float backgroundWidth; // Chiều rộng mỗi ảnh nền

    private void Start()
    {
        if (backgrounds.Length == 0) return;

        // Lấy chiều rộng của một ảnh nền (giả sử tất cả có cùng kích thước)
        backgroundWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        // Xác định phần nền nào đang ở xa nhất bên trái/phải
        Transform leftMost = backgrounds[0];
        Transform rightMost = backgrounds[0];

        foreach (Transform bg in backgrounds)
        {
            if (bg.position.x < leftMost.position.x)
                leftMost = bg;
            if (bg.position.x > rightMost.position.x)
                rightMost = bg;
        }

        // Nếu nhân vật vượt qua phần nền bên phải, di chuyển phần nền bên trái ra phía trước
        if (player.position.x > rightMost.position.x - backgroundWidth / 2)
        {
            leftMost.position = new Vector3(rightMost.position.x + backgroundWidth, leftMost.position.y, leftMost.position.z);
        }
        // Nếu nhân vật vượt qua phần nền bên trái, di chuyển phần nền bên phải ra phía sau
        else if (player.position.x < leftMost.position.x + backgroundWidth / 2)
        {
            // rightMost.position = new Vector3(leftMost.position.x - backgroundWidth, rightMost.position.y, rightMost.position.z);
        }
    }
}
