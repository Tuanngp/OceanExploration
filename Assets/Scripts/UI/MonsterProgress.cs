using UnityEngine;
using UnityEngine.UI;

public class MonsterProgress : MonoBehaviour
{
    public Slider progressBar;
    private GameObject victoryCanvas;
    private GameObject BottomHUBPanel;
    private GameObject KillProgressBar;

    void Start()
    {
        victoryCanvas = GameObject.Find("VictoryCanvas");
        BottomHUBPanel = GameObject.Find("Bottom HUB Panel");
        KillProgressBar = GameObject.Find("KillProgressBar");
        if (victoryCanvas != null)
        {
            victoryCanvas.SetActive(false);
        }
    }
    public void UpdateProgress(int monstersKilled, int totalMonsters)
    {
        progressBar.value = (float)monstersKilled / totalMonsters;
        Debug.Log("progressBar.value: " + progressBar.value);
        if (monstersKilled >= totalMonsters)
        {
            Debug.Log("Hoàn thành mục tiêu!");
            ShowVictoryUI();
        }
        
    }
    private void ShowVictoryUI()
    {
        if (victoryCanvas != null)
        {
            victoryCanvas.SetActive(true);
            Debug.Log(victoryCanvas);
            BottomHUBPanel.SetActive(false);
            KillProgressBar.SetActive(false);
            Time.timeScale = 0f;
        }
    }
}
