using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton để truy cập từ mọi nơi
    public TMP_Text scoreText; // UI Text hiển thị điểm
    public int score = 0; // Biến lưu điểm

    void Awake()
    {
        instance = this; // Đảm bảo chỉ có 1 ScoreManager
    }

    public void AddScore(int amount)
    {
        score += amount; // Cộng điểm
        scoreText.text = "Score: " + score; // Cập nhật UI
    }
}
