using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpawnMonsters : MonoBehaviour
{
    public List<GameObject> monsterPrefabs;
    public Transform submarine;
    public float minY = -60f, maxY = 0f;
    public int maxMonsters = 5;
    public float fixedZ = 0f;
    private List<GameObject> spawnedMonsters = new List<GameObject>();
    public MonsterProgress monsterProgress;
    void Update()
    {
        if (spawnedMonsters.Count < maxMonsters)
        {
            SpawnMonster();
        }
        UpdateHealthBarPositions();
    }

    void SpawnMonster()
    {
        if (monsterPrefabs.Count == 0) return;

        float minX = submarine.position.x;
        float maxX = submarine.position.x + 1000;
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector3 randomSpawnPosition = new Vector3(randomX, randomY, fixedZ);

        int randomIndex = Random.Range(0, monsterPrefabs.Count);
        GameObject selectedMonster = monsterPrefabs[randomIndex];

        GameObject spawnedMonster = Instantiate(selectedMonster, randomSpawnPosition, Quaternion.identity);

        spawnedMonsters.Add(spawnedMonster);

        MonsterAI monsterAI = spawnedMonster.GetComponent<MonsterAI>();
        if (monsterAI != null)
        {
            monsterAI.target = submarine;
            monsterAI.monsterProgress = monsterProgress;
        }

    }

    void UpdateHealthBarPositions()
    {
        foreach (GameObject monster in spawnedMonsters)
        {
            if (monster == null) continue;

            Transform healthBar = monster.transform.Find("HealthBarMonster");
            if (healthBar != null)
            {
                healthBar.position = monster.transform.position + new Vector3(0f, -12f, 0f);
                healthBar.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
