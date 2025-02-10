using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    private float duration = 120f; // 2 phút = 120 giây

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        StartCoroutine(FillProgressBar());
    }

    private IEnumerator FillProgressBar()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            slider.value = Mathf.Lerp(0, 1, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        slider.value = 1f; // Đảm bảo thanh đầy 100% sau 2 phút
    }
}
