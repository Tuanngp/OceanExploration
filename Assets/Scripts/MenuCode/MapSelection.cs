using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MapSelection : MonoBehaviour
{
    public Image[] maps;
    private int selectedMap = 0;

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
            maps[mapIndex].transform.localScale = Vector3.one * 1.1f; 
        }
    }

    public void OnHoverExit(int mapIndex)
    {
        if (mapIndex != selectedMap)
        {
            maps[mapIndex].transform.localScale = Vector3.one; 
        }
    }

    private void UpdateMapSelection()
    {
        for (int i = 0; i < maps.Length; i++)
        {
            if (i == selectedMap)
            {
                maps[i].transform.localScale = Vector3.one * 1.15f; 
                maps[i].color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                maps[i].transform.localScale = Vector3.one;
                maps[i].color = new Color(0.8f, 0.8f, 0.8f, 1f); 
            }
        }
    }
}
