using UnityEngine;
using UnityEngine.UI;

public class MonsterProgress : MonoBehaviour
{
    public Slider progressBar;
    public void UpdateProgress(int monstersKilled, int totalMonsters)
    {
        progressBar.value = (float)monstersKilled / totalMonsters;
        Debug.Log("progressBar.value: " + progressBar.value);
        if (monstersKilled >= totalMonsters)
        {
            Debug.Log("Hoàn thành mục tiêu! 🏆");
        }
    }
}
