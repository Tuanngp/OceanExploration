using System.Collections;
using UnityEngine;

public class RareCoinEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(PulseEffect());
    }

    IEnumerator PulseEffect()
    {
        while (true)
        {
            yield return PulseColor(Color.white, Color.yellow, 1f);
        }
    }

    IEnumerator PulseColor(Color startColor, Color endColor, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            spriteRenderer.color = Color.Lerp(startColor, endColor, Mathf.PingPong(time * 2f, 1f));
            time += Time.deltaTime;
            yield return null;
        }
    }
}

