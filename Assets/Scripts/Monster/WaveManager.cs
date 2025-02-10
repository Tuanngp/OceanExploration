using UnityEngine;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs; // Danh sách quái vật
    [SerializeField] private Transform spawnPoint; // Điểm spawn
    [SerializeField] private int enemiesPerWave = 2; // Số quái vật xuất hiện mỗi đợt

    private int currentWaveIndex = 0; // Chỉ mục của đợt hiện tại
    private int aliveCount = 0; // Số lượng quái vật còn sống

    void Start()
    {
        SpawnNextWave(); // Bắt đầu wave đầu tiên
    }

    // Hàm spawn quái vật
    void SpawnNextWave()
    {
        if (currentWaveIndex >= enemyPrefabs.Count) return;

        aliveCount = 0;

        for (int i = 0; i < enemiesPerWave && currentWaveIndex < enemyPrefabs.Count; i++)
        {
            Vector3 spawnPos = spawnPoint.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
            GameObject enemy = Instantiate(enemyPrefabs[currentWaveIndex], spawnPos, Quaternion.identity);
            enemy.GetComponent<JellyfishAI>().waveManager = this;
            aliveCount++;
            currentWaveIndex++;
        }
    }

    // Hàm giảm số lượng quái vật khi một con chết
    public void EnemyDied()
    {
        aliveCount--;

        if (aliveCount <= 0)
        {
            Invoke(nameof(SpawnNextWave), 2f); // Chờ 2s rồi spawn đợt tiếp theo
        }
    }
}
