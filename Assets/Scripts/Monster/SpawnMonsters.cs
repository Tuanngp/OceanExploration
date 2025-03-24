using UnityEngine;
using System.Collections.Generic;

public class SpawnMonsters : MonoBehaviour
{
    public List<GameObject> monsterPrefabs;
    public GameObject bossPrefab;
    public Transform submarine;
    public float minY = -60f, maxY = 0f;
    [SerializeField] public static int maxMonsters = 0;
    public float fixedZ = 0f;

    public static List<MonsterMovement> ActiveMonsters = new List<MonsterMovement>();
    private List<GameObject> spawnedMonsters = new List<GameObject>();

    private bool bossSpawned = false;

    void Start()
    {
        SpawnRandomMonsters();
    }

    void Update()
    {
        if (!bossSpawned && MonsterAI.GetKillCount() >= maxMonsters)
        {
            SpawnBoss();
            bossSpawned = true;
        }
    }

    void SpawnRandomMonsters()
    {
        if (monsterPrefabs.Count == 0) return;

        while (spawnedMonsters.Count < maxMonsters)
        {
            SpawnMonster();
        }
    }

    void SpawnMonster()
    {
        if (monsterPrefabs.Count == 0) return;

        float minX = submarine.position.x;
        float maxX = submarine.position.x + 300;
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector3 randomSpawnPosition = new Vector3(randomX, randomY, fixedZ);

        int randomIndex = Random.Range(0, monsterPrefabs.Count);
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

    void SpawnBoss()
    {
        if (bossPrefab == null) return;

        Vector3 bossSpawnPosition = new Vector3(submarine.position.x + 120, Random.Range(minY, maxY), fixedZ);
        GameObject spawnedBoss = Instantiate(bossPrefab, bossSpawnPosition, Quaternion.identity);
        MonsterMovement monsterMovement = spawnedBoss.GetComponent<MonsterMovement>();
        if (monsterMovement != null)
        {
            monsterMovement.target = submarine;
        }
    }
}
