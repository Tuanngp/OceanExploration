using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WarningSystem : MonoBehaviour
{
    public static WarningSystem Instance;

    public Image warningImagePrefab;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowWarning()
    {
        if (warningImagePrefab == null)
        {
            Debug.LogError("WarningSystem - Chưa gán warningImagePrefab!");
            return;
        }

        Transform canvasTransform = GameObject.Find("UI").transform;
        Image warningImage = Instantiate(warningImagePrefab, canvasTransform);

        SetupWarningPosition(warningImage);
        StartCoroutine(WarningBlinkAndDestroy(warningImage));
    }

    private void SetupWarningPosition(Image warningImage)
    {
        RectTransform rt = warningImage.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 1);
        rt.anchorMax = new Vector2(0, 1);
        rt.pivot = new Vector2(0, 1);
        rt.anchoredPosition = new Vector2(30, -100);
    }

    private IEnumerator WarningBlinkAndDestroy(Image warningImage)
    {
        float elapsedTime = 0f;
        bool isVisible = true;

        while (elapsedTime < 1.5f)
        {
            isVisible = !isVisible;
            warningImage.enabled = isVisible;

            yield return new WaitForSeconds(0.3f);
            elapsedTime += 0.3f;
        }

        Destroy(warningImage.gameObject);
    }
}
