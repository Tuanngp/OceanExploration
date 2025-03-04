using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MovementHandler))]
public class SpeedBoostHandler : MonoBehaviour
{
    private MovementHandler movementHandler;
    private Coroutine speedBoostCoroutine;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Awake()
    {
        movementHandler = GetComponent<MovementHandler>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Lưu màu gốc để đổi màu hiệu ứng
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void ActivateSpeedBoost(float duration, float multiplier)
    {
        if (speedBoostCoroutine != null)
        {
            StopCoroutine(speedBoostCoroutine);  // Reset nếu đang có boost
        }
        speedBoostCoroutine = StartCoroutine(SpeedBoostRoutine(duration, multiplier));
    }

    private IEnumerator SpeedBoostRoutine(float duration, float multiplier)
    {
        float originalMultiplier = movementHandler.maxSpeedMultiplier;   // Lưu giá trị gốc
        movementHandler.maxSpeedMultiplier *= multiplier;                 // Tăng tối đa tốc độ

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.yellow;  // Hiệu ứng sáng
        }

        yield return new WaitForSeconds(duration);

        movementHandler.maxSpeedMultiplier = originalMultiplier;  // Reset lại maxSpeedMultiplier
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;  // Trả màu về gốc
        }
    }
}
