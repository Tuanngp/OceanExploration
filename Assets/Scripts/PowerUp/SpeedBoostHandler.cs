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

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void ActivateSpeedBoost(float duration, float multiplier)
    {
        if (speedBoostCoroutine != null)
        {
            StopCoroutine(speedBoostCoroutine); 
        }
        speedBoostCoroutine = StartCoroutine(SpeedBoostRoutine(duration, multiplier));
    }

    private IEnumerator SpeedBoostRoutine(float duration, float multiplier)
    {
        float originalMultiplier = movementHandler.maxSpeedMultiplier;  
        movementHandler.maxSpeedMultiplier *= multiplier;          

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.yellow; 
        }

        yield return new WaitForSeconds(duration);

        movementHandler.maxSpeedMultiplier = originalMultiplier;
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }
}
