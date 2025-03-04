using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI; // Tham chiếu đến Panel Pause Menu
    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false); // Đảm bảo menu ẩn khi bắt đầu
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Dừng game
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Tiếp tục game
        isPaused = false;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f; // Reset lại thời gian trước khi chuyển Scene
        SceneManager.LoadScene("MenuScene"); 
    }
}
