using UnityEngine;
using UnityEngine.UI;

public class MonsterProgress : MonoBehaviour
{
    public Slider progressBar;
    private GameObject victory;
    private GameObject BottomHUBPanel;
    private GameObject KillProgressBar;

    void Start()
    {
        victory = GameObject.Find("VictoryCanvas/Victory");
        BottomHUBPanel = GameObject.Find("Bottom HUB Panel");
        KillProgressBar = GameObject.Find("KillProgressBar");
        if (victory != null)
        {
            victory.SetActive(false);
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
        if (victory != null)
        {
            victory.SetActive(true);
            Debug.Log(victory);
            BottomHUBPanel.SetActive(false);
            KillProgressBar.SetActive(false);
            Time.timeScale = 0f;
        }
    }
}
