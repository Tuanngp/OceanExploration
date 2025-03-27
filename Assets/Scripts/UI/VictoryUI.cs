using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryUI : MonoBehaviour
{
    private GameObject victory;
    public MonsterProgress monsterProgress;
    private HealthBarController healthBarController;
    void Start() {
        var monsterProgressObject = GameObject.Find("UI/KillProgressBar");
        if (monsterProgressObject != null)
        {
            monsterProgress = monsterProgressObject.GetComponent<MonsterProgress>();
        }
        victory = GameObject.Find("UI/VictoryCanvas/Victory");
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            healthBarController = player.GetComponent<HealthBarController>();
        }
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void RestartGame()
    {
        healthBarController?.ResetHealth();
        MonsterAI.killCount = 0;
        monsterProgress?.UpdateProgress(0, SpawnMonsters.maxMonsters + 1);
        victory.SetActive(false); // Tắt UI Victory
        Time.timeScale = 1f; // Khôi phục lại tốc độ game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Tải lại màn chơi hiện tại
    }

    public void MainMenu()
    {
        MonsterAI.killCount = 0;
        monsterProgress?.UpdateProgress(0, SpawnMonsters.maxMonsters + 1);
        victory.SetActive(false); // Tắt UI Victory
        Time.timeScale = 1f; // Khôi phục lại tốc độ game
        SceneManager.LoadScene("MapScene"); // Thay bằng tên Scene của Menu chính
    }
}
