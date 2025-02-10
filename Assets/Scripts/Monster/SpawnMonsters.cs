using UnityEngine;
using System.Collections.Generic;

public class SpawnMonsters : MonoBehaviour
{
    public List<GameObject> monsterPrefabs; // Danh sách Prefab quái vật
    public GameObject healthBarPrefab; // Prefab thanh máu
    public Vector3 spawnPosition; // Vị trí spawn ban đầu
    public Transform submarine; // Target của quái vật

    void Start()
    {
        SpawnAllMonsters();
    }

    void SpawnAllMonsters()
    {
        foreach (GameObject monsterPrefab in monsterPrefabs)
        {
            // Random vị trí Y, nhưng giữ nguyên X và Z
            float randomY = spawnPosition.y + Random.Range(230f, 250f);
            Vector3 newSpawnPosition = new Vector3(spawnPosition.x, randomY, spawnPosition.z);

            // Spawn quái vật
            GameObject spawnedMonster = Instantiate(monsterPrefab, newSpawnPosition, Quaternion.identity);

            // Spawn thanh máu
            GameObject healthBarInstance = Instantiate(healthBarPrefab, spawnedMonster.transform.position, Quaternion.identity);

            // Gán thanh máu follow quái vật
            HealthBarFollow healthBarFollow = healthBarInstance.GetComponent<HealthBarFollow>();
            if (healthBarFollow != null)
            {
                healthBarFollow.SetTarget(spawnedMonster.transform, new Vector3(0, 1.2f, 0)); // Điều chỉnh sát hơn
            }

            // Gán mục tiêu cho MonsterAI
            MonsterAI monsterAI = spawnedMonster.GetComponent<MonsterAI>();
            if (monsterAI != null)
            {
                monsterAI.target = submarine;
            }
        }
    }
}
