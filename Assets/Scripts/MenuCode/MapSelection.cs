using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MapSelection : MonoBehaviour
{
    public Image[] maps; // Mảng chứa các ảnh map
    private int selectedMap = 0; // Map được chọn

    void Start()
    {
        UpdateMapSelection();
    }

    public void SelectMap(int mapIndex)
    {
        selectedMap = mapIndex;
        PlayerPrefs.SetInt("SelectedMap", mapIndex);
        UpdateMapSelection();
    }

    public void PlayGame()
    {
        string sceneName = "scene" + selectedMap;
        SceneManager.LoadScene(sceneName);
    }

    public void OnHoverEnter(int mapIndex)
    {
        if (mapIndex != selectedMap)
        {
            maps[mapIndex].transform.localScale = Vector3.one * 1.1f; // Phóng to nhẹ
        }
    }

    public void OnHoverExit(int mapIndex)
    {
        if (mapIndex != selectedMap)
        {
            maps[mapIndex].transform.localScale = Vector3.one; // Trở lại bình thường
        }
    }

    private void UpdateMapSelection()
    {
        for (int i = 0; i < maps.Length; i++)
        {
            if (i == selectedMap)
            {
                maps[i].transform.localScale = Vector3.one * 1.15f; // Phóng to hơn map đang chọn
                maps[i].color = new Color(1f, 1f, 1f, 1f); // Màu sáng hơn
            }
            else
            {
                maps[i].transform.localScale = Vector3.one;
                maps[i].color = new Color(0.8f, 0.8f, 0.8f, 1f); // Màu tối hơn để phân biệt
            }
        }
    }
}
