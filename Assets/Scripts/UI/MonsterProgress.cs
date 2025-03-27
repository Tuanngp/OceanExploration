using UnityEngine;
using UnityEngine.UI;

public class MonsterProgress : MonoBehaviour
{
    private GameObject victory;
    private GameObject BottomHUBPanel;
    private Slider KillProgressBar;
    void Start()
    {
        victory = GameObject.Find("UI/VictoryCanvas/Victory");
        BottomHUBPanel = GameObject.Find("Bottom HUB Panel");
        var killProgressObject = GameObject.Find("KillProgressBar");
        if (killProgressObject != null)
        {
            KillProgressBar = killProgressObject.GetComponent<Slider>();
        }
        if (victory != null)
        {
            victory.SetActive(false);
        }
    }
    public void UpdateProgress(int monstersKilled, int totalMonsters)
    {
        // Debug.Log("monstersKilled: " + monstersKilled);
        // Debug.Log("totalMonsters: " + totalMonsters);
        KillProgressBar.value = (float)monstersKilled / totalMonsters;
        if (monstersKilled >= totalMonsters)
        {
            // Debug.Log("Hoàn thành mục tiêu!");
            ShowVictoryUI();
        }
        
    }
    private void ShowVictoryUI()
    {
        if (victory != null)
        {
            victory.SetActive(true);
            // BottomHUBPanel.SetActive(false);
            Time.timeScale = 0f;
        }
    }
}
