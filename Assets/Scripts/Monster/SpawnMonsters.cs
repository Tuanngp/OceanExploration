using UnityEngine;
using System.Collections.Generic;

public class SpawnMonsters : MonoBehaviour
{
    public List<GameObject> monsterPrefabs; // Danh sách quái vật
    public Transform submarine; // Tàu ngầm (mục tiêu)
    public float minY = -60f, maxY = 0f; // Giới hạn vị trí spawn theo trục Y
    public static int maxMonsters = 10; // Số lượng tối đa quái vật spawn
    public float fixedZ = 0f; // Giữ quái vật ở Z cố định

    public static List<MonsterMovement> ActiveMonsters = new List<MonsterMovement>();
    private List<GameObject> spawnedMonsters = new List<GameObject>(); // Danh sách quái vật đã spawn

    void Start()
    {
        SpawnRandomMonsters();
    }
    void SpawnRandomMonsters()
    {
        if (monsterPrefabs.Count == 0) return;

        while (spawnedMonsters.Count < maxMonsters) // Đảm bảo spawn đúng số lượng
        {
            SpawnMonster();
        }
    }

    void SpawnMonster()
    {
        if (monsterPrefabs.Count == 0) return;

        float minX = submarine.position.x;
        float maxX = submarine.position.x + 1000;
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector3 randomSpawnPosition = new Vector3(randomX, randomY, fixedZ);

        int randomIndex = Random.Range(0, monsterPrefabs.Count); // Chọn quái vật ngẫu nhiên
        GameObject selectedMonster = monsterPrefabs[randomIndex];

        GameObject spawnedMonster = Instantiate(selectedMonster, randomSpawnPosition, Quaternion.identity);
        spawnedMonsters.Add(spawnedMonster);

        MonsterMovement monsterMovement = spawnedMonster.GetComponent<MonsterMovement>();
        if (monsterMovement != null)
        {
            monsterMovement.target = submarine;
            ActiveMonsters.Add(monsterMovement);
        }
    }
}
