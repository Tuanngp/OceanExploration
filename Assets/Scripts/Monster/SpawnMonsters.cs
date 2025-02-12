using UnityEngine;
using System.Collections.Generic;

public class SpawnMonsters : MonoBehaviour
{
    public List<GameObject> monsterPrefabs;
    public Transform submarine;

    public float minX = 555f, maxX = 1000f;
    public float minY = -60f, maxY = 0f;
    public float fixedZ = 0f;

    void Start()
    {
        SpawnAllMonsters();
    }

    void SpawnAllMonsters()
    {
        foreach (GameObject monsterPrefab in monsterPrefabs)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            Vector3 randomSpawnPosition = new Vector3(randomX, randomY, fixedZ);

            GameObject spawnedMonster = Instantiate(monsterPrefab, randomSpawnPosition, Quaternion.identity);

            MonsterAI monsterAI = spawnedMonster.GetComponent<MonsterAI>();
            if (monsterAI != null)
            {
                monsterAI.target = submarine;
            }
        }
    }
}
