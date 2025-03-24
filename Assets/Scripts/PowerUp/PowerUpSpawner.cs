using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;  // Viên thuốc
    public float spawnInterval = 10f; // Bao lâu spawn 1 viên

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnPowerUp();
            timer = 0f;
        }
    }

    void SpawnPowerUp()
    {
        if (SpawnMonsters.ActiveMonsters.Count == 0)
        {
            return;
        }

        MonsterMovement randomMonster = null;
        int attempts = 0;
        while (attempts < 10)
        {
            randomMonster = SpawnMonsters.ActiveMonsters[Random.Range(0, SpawnMonsters.ActiveMonsters.Count)];
            if (randomMonster != null && randomMonster.gameObject != null) break;
            attempts++;
        }

        if (randomMonster == null || randomMonster.gameObject == null)
        {
            return;
        }

        Vector2 spawnPosition = (Vector2)randomMonster.transform.position + Random.insideUnitCircle * 25f;
        GameObject powerUp = Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);

        float randomScale = Random.Range(1.5f, 3f);
        powerUp.transform.localScale = new Vector3(randomScale, randomScale, 1f);
        powerUp.AddComponent<PowerUpRotation>();  // Tự xoay nhẹ
    }

}