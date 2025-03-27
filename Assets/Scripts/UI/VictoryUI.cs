using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryUI : MonoBehaviour
{
    private GameObject victory;
    public MonsterProgress monsterProgress;
    private HealthBarController healthBarController;
    void Start()
    {
        var monsterProgressObject = GameObject.Find("UI/KillProgressBar");
        if (monsterProgressObject != null)
        {
            monsterProgress = monsterProgressObject.GetComponent<MonsterProgress>();
        }
        victory = GameObject.Find("UI/VictoryCanvas/Victory");
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
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
        var currentScene = SceneManager.GetActiveScene().name;

        // Tìm số ở cuối tên scene
        Match match = Regex.Match(currentScene, @"(\d+)$");

        if (match.Success)
        {
            int number = int.Parse(match.Value); // Lấy số cuối cùng
            int newNumber = number + 1; // Tăng lên 1
            string newSceneName = Regex.Replace(currentScene, @"\d+$", newNumber.ToString()); // Thay thế số cũ bằng số mới
            SceneManager.LoadScene(newSceneName); // Tải Scene tiếp theo
            if (newNumber == 4)
            {
                SceneManager.LoadScene("MapScene");
            }
        }
        else
        {
            SceneManager.LoadScene("MapScene"); // Thay bằng tên Scene của Menu chính
        }
    }
}
