using UnityEngine;
using UnityEngine.UI;

public class MonsterProgress : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void UpdateProgress(int monstersKilled, int totalMonsters)
    {
        slider.value = (float)monstersKilled / totalMonsters;

        if (monstersKilled >= totalMonsters)
        {
            Debug.Log("Hoàn thành mục tiêu! 🏆");
        }
    }
}
